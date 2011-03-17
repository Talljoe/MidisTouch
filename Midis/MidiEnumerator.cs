// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Midis.Abstraction;

    public class MidiEnumerator
    {
        private readonly IMidiHAL hal;
        private readonly LoopbackDevice lo = new LoopbackDevice();

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
            return new InputPort(portId, portId < this.hal.GetInputDeviceCount()
                                             ? this.hal.OpenInputDevice(portId)
                                             : this.lo);
        }

        public OutputPort OpenMidiOut(int portId)
        {
            return new OutputPort(portId, portId < this.hal.GetOutputDeviceCount()
                                              ? this.hal.OpenOutputDevice(portId)
                                              : this.lo);
        }
    }
}