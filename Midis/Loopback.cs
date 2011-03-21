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

    public class Loopback : IMidiHAL
    {
        private readonly List<LoopbackDevice> devices;

        private readonly ConcurrentDictionary<int, InputDeviceDescriptor> inputDescriptors =
            new ConcurrentDictionary<int, InputDeviceDescriptor>();

        private readonly ConcurrentDictionary<int, OutputDeviceDescriptor> outputDescriptors =
            new ConcurrentDictionary<int, OutputDeviceDescriptor>();

        public Loopback(int count)
        {
            this.devices = Enumerable.Range(0, count)
                .Select(_ => new LoopbackDevice())
                .ToList();
        }

        public Loopback() : this(1) {}

        protected int Count
        {
            get { return this.devices.Count; }
        }

        public int GetInputDeviceCount()
        {
            return this.Count;
        }

        public int GetOutputDeviceCount()
        {
            return this.Count;
        }

        public IOutputDevice OpenOutputDevice(int portId)
        {
            return this.devices[portId];
        }

        public IInputDevice OpenInputDevice(int portId)
        {
            return this.devices[portId];
        }

        public InputDeviceDescriptor GetInputDescriptor(int portId)
        {
            return this.inputDescriptors.GetOrAdd(portId, this.CreateInputDescriptor);
        }

        public OutputDeviceDescriptor GetOutputDescriptor(int portId)
        {
            return this.outputDescriptors.GetOrAdd(portId, this.CreateOutputDescriptor);
        }

        public InputDeviceDescriptor CreateInputDescriptor(int portId)
        {
            return new InputDeviceDescriptor(portId, GetDeviceName(portId), 0, 0, 0);
        }

        public OutputDeviceDescriptor CreateOutputDescriptor(int portId)
        {
            return new OutputDeviceDescriptor(portId, GetDeviceName(portId), PortType.Port,
                                              new BitArray(16, true), 0, 0, 0);
        }

        private static string GetDeviceName(int portId)
        {
            return String.Format("Loopback {0}", portId + 1);
        }
    }
}