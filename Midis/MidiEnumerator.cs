// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Midis.Abstraction;

    public class MidiEnumerator : IDisposable
    {
        private readonly IEnumerable<IMidiHAL> hals;
        private readonly ConcurrentDictionary<int, Lazy<SharedInputDevice>> openInputDevices = new ConcurrentDictionary<int, Lazy<SharedInputDevice>>();
        private readonly ConcurrentDictionary<int, Lazy<SharedOutputDevice>> openOutputDevices = new ConcurrentDictionary<int, Lazy<SharedOutputDevice>>();
        private readonly List<DeviceInfo> inputDevices;
        private readonly List<DeviceInfo> outputDevices;

        public MidiEnumerator(IEnumerable<IMidiHAL> hals)
        {
            this.hals = (hals ?? Enumerable.Empty<IMidiHAL>()).MemoizeAll();
            this.inputDevices = this.GetDevices(hal => hal.GetInputDeviceCount());
            this.outputDevices = this.GetDevices(hal => hal.GetOutputDeviceCount());
        }

        private List<DeviceInfo> GetDevices(Func<IMidiHAL, int> deviceCountSelector)
        {
            return this.hals.SelectMany(hal => Enumerable.Range(0, deviceCountSelector(hal)), (hal, portId) => new { hal, portId })
                            .Select((info, portId) => new DeviceInfo { GlobalPortId = portId, PortId = info.portId, Hal = info.hal })
                            .ToList();
        }

        public IEnumerable<MidiInDescriptor> GetInputDevices()
        {
            return inputDevices.Select(di => di.Hal.GetInputDescriptor(di.PortId))
                               .Select((d, port) => new MidiInDescriptor(port, d.Name));
        }

        public IEnumerable<MidiOutDescriptor> GetOutputDevices()
        {
            return outputDevices.Select(di => di.Hal.GetOutputDescriptor(di.PortId))
                                .Select((d, port) => new MidiOutDescriptor(port, d.Name, d.PortType, d.WChannelMask));
        }

        public InputPort OpenMidiIn(int portId)
        {
            var device = this.OpenMidi<SharedInputDevice, IInputDevice>(portId, openInputDevices, this.GetInputDevice);
            return new InputPort(portId, device.Get());
        }

        public OutputPort OpenMidiOut(int portId)
        {
            var device = this.OpenMidi<SharedOutputDevice, IOutputDevice>(portId, openOutputDevices, this.GetOutputDevice);
            return new OutputPort(portId, device.Get());
        }

        public bool Rescan()
        {
            var hasNewInputDevices = this.DoRescan(this.inputDevices, hal => hal.GetInputDeviceCount());
            var hasNewOutputDevices = this.DoRescan(this.outputDevices, hal => hal.GetOutputDeviceCount());
            return hasNewInputDevices || hasNewOutputDevices;
        }

        private class LocalDeviceComparer : IEqualityComparer<DeviceInfo>
        {
            public bool Equals(DeviceInfo x, DeviceInfo y)
            {
                return x.PortId == y.PortId && ReferenceEquals(x.Hal, y.Hal);
            }

            public int GetHashCode(DeviceInfo obj)
            {
                return (obj.PortId.GetHashCode() ^ 397);
            }
        }

        private bool DoRescan(List<DeviceInfo> deviceInfos, Func<IMidiHAL, int> deviceCountSelector)
        {
            var comparer = new LocalDeviceComparer();
            var initialCount = deviceInfos.Count;
            var newDevices = this.GetDevices(deviceCountSelector);
            deviceInfos.AddRange(newDevices.Except(deviceInfos, comparer));
            deviceInfos.Except(newDevices, comparer).Run(di => deviceInfos[di.GlobalPortId].Hal = NullDevice.Hal);
            var finalCount = deviceInfos.Count;
            return initialCount != finalCount;
        }

        private T OpenMidi<T, TDevice>(int portId, ConcurrentDictionary<int, Lazy<T>> devices, Func<int, T> getDeviceFunc)
            where TDevice : class, IDevice where T : SharedDevice<TDevice>
        {
            var device = getDeviceFunc(portId);
            if (device.Closed)
            {
                lock (this.openInputDevices)
                {
                    device = getDeviceFunc(portId);
                    if (device.Closed)
                    {
                        Lazy<T> sharedDevice;
                        devices.TryRemove(portId, out sharedDevice);
                    }
                }
                device = getDeviceFunc(portId);
            }
            return device;
        }

        private SharedInputDevice GetInputDevice(int portId)
        {
            var deviceInfo = this.inputDevices[portId];
            return this.openInputDevices.GetOrAdd(portId, new Lazy<SharedInputDevice>(
                    () => new SharedInputDevice(deviceInfo.Hal.OpenInputDevice(deviceInfo.PortId)))).Value;
        }

        private SharedOutputDevice GetOutputDevice(int portId)
        {
            var deviceInfo = this.outputDevices[portId];
            return this.openOutputDevices.GetOrAdd(portId, new Lazy<SharedOutputDevice>(
                    () => new SharedOutputDevice(deviceInfo.Hal.OpenOutputDevice(deviceInfo.PortId)))).Value;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                this.openInputDevices.Cast<KeyValuePair<int, Lazy<IDisposable>>>()
                                     .Concat(this.openOutputDevices.Cast<KeyValuePair<int, Lazy<IDisposable>>>())
                                     .Select(kvp => kvp.Value)
                                     .Where(lazy => lazy.IsValueCreated)
                                     .Do(lazy => lazy.Value.Dispose());
            }
        }

        private class DeviceInfo
        {
            public int GlobalPortId { get; set; }
            public int PortId { get; set; }
            public IMidiHAL Hal { get; set; }
        }
    }
}