// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Midis.Interop;

    public class MidiEnumerator
    {
        public IEnumerable<MidiInDescriptor> GetInputDevices()
        {
            return Enumerable.Range(0, NativeMethods.midiInGetNumDevs()).Select(GetInputDescriptor);
        }

        public IEnumerable<MidiOutDescriptor> GetOutputDevices()
        {
            return Enumerable.Range(0, NativeMethods.midiOutGetNumDevs()).Select(GetOutputDescriptor);
        }

        private static MidiInDescriptor GetInputDescriptor(int portId)
        {
            var caps = new tagMIDIINCAPSW();
            var result = NativeMethods.midiInGetDevCapsW(portId, ref caps, Marshal.SizeOf(caps));
            if (result != NativeConstants.MMSYSERR_NOERROR)
            {
                throw new Exception(String.Format("MIDI Error: {0}", result));
            }

            return new MidiInDescriptor(portId, caps.szPname);
        }

        private static MidiOutDescriptor GetOutputDescriptor(int portId)
        {
            var caps = new tagMIDIOUTCAPSW();
            var result = NativeMethods.midiOutGetDevCapsW(portId, ref caps, Marshal.SizeOf(caps));
            if(result != NativeConstants.MMSYSERR_NOERROR)
            {
                throw new Exception(String.Format("MIDI Error: {0}", result));
            }

            return new MidiOutDescriptor(portId, caps.szPname, (PortType)caps.wTechnology, new BitArray(BitConverter.GetBytes(caps.wChannelMask)));
        }
    }
}