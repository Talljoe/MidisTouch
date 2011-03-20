// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Midis.Abstraction;

    public class MidiEnumerator : IDisposable
    {
        private readonly IMidiHAL hal;
        private readonly LoopbackDevice lo = new LoopbackDevice();
        private readonly ConcurrentDictionary<int, Lazy<SharedInputDevice>> openInputDevices = new ConcurrentDictionary<int, Lazy<SharedInputDevice>>();
        private readonly ConcurrentDictionary<int, Lazy<SharedOutputDevice>> openOutputDevices = new ConcurrentDictionary<int, Lazy<SharedOutputDevice>>();

        public MidiEnumerator(IMidiHAL hal)
        {
            if (hal == null)
            {
                throw new ArgumentNullException("hal");
            }
            this.hal = hal;
        }

        public IEnumerable<MidiInDescriptor> GetInputDevices()
        {
            var count = this.hal.GetInputDeviceCount();
            return Enumerable.Range(0, count)
                .Select(this.hal.GetInputDescriptor)
                .Select(d => new MidiInDescriptor(d.Id, d.Name))
                .Concat(new[] {new MidiInDescriptor(count, "Loopback")});
        }

        public IEnumerable<MidiOutDescriptor> GetOutputDevices()
        {
            var count = this.hal.GetOutputDeviceCount();
            return Enumerable.Range(0, count)
                .Select(this.hal.GetOutputDescriptor)
                .Select(d => new MidiOutDescriptor(d.Id, d.Name, d.PortType, d.WChannelMask))
                .Concat(new[]
                            {
                                new MidiOutDescriptor(count, "Loopback", PortType.Port,
                                                      new BitArray(new byte[] {0xff, 0xff}))
                            });
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
            return this.openInputDevices.GetOrAdd(portId, new Lazy<SharedInputDevice>(
                    () => new SharedInputDevice(portId < this.hal.GetInputDeviceCount()
                                                        ? this.hal.OpenInputDevice(portId)
                                                        : this.lo))).Value;
        }

        private SharedOutputDevice GetOutputDevice(int portId)
        {
            return this.openOutputDevices.GetOrAdd(portId, new Lazy<SharedOutputDevice>(
                    () => new SharedOutputDevice(portId < this.hal.GetOutputDeviceCount()
                                                        ? this.hal.OpenOutputDevice(portId)
                                                        : this.lo))).Value;
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
    }
}