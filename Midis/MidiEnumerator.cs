// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Midis.Abstraction;

    public class MidiEnumerator
    {
        private readonly IMidiHAL hal;

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
            return Enumerable.Range(0, this.hal.GetInputDeviceCount())
                .Select(this.hal.GetInputDescriptor)
                .Select(d => new MidiInDescriptor(d.Id, d.Name));
        }

        public IEnumerable<MidiOutDescriptor> GetOutputDevices()
        {
            return Enumerable.Range(0, this.hal.GetOutputDeviceCount())
                .Select(this.hal.GetOutputDescriptor)
                .Select(d => new MidiOutDescriptor(d.Id, d.Name, d.PortType, d.WChannelMask));
        }

        public InputPort OpenMidiIn(int portId)
        {
            return new InputPort(portId, this.hal.OpenInputDevice(portId));
        }

        public OutputPort OpenMidiOut(int portId)
        {
            return new OutputPort(portId, this.hal.OpenOutputDevice(portId));
        }
    }
}