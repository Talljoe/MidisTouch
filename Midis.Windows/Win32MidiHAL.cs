// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Windows
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using Midis.Abstraction;
    using Midis.Windows.Interop;

    public class Win32MidiHAL : IMidiHAL
    {
        public int GetInputDeviceCount()
        {
            return NativeMethods.midiInGetNumDevs();
        }

        public int GetOutputDeviceCount()
        {
            return NativeMethods.midiOutGetNumDevs();
        }

        public InputDeviceDescriptor GetInputDescriptor(int portId)
        {
            var caps = new tagMIDIINCAPSW();
            var result = NativeMethods.midiInGetDevCapsW(portId, ref caps, Marshal.SizeOf(caps));
            if (result != NativeConstants.MMSYSERR_NOERROR)
            {
                throw new Exception(String.Format("MIDI Error: {0}", result));
            }

            return new InputDeviceDescriptor(portId, caps.szPname, caps.vDriverVersion, caps.wMid, caps.wPid);
        }

        public OutputDeviceDescriptor GetOutputDescriptor(int portId)
        {
            var caps = new tagMIDIOUTCAPSW();
            var result = NativeMethods.midiOutGetDevCapsW(portId, ref caps, Marshal.SizeOf(caps));
            if (result != NativeConstants.MMSYSERR_NOERROR)
            {
                throw new Exception(String.Format("MIDI Error: {0}", result));
            }

            return new OutputDeviceDescriptor(portId, caps.szPname, (PortType) caps.wTechnology,
                                              new BitArray(BitConverter.GetBytes(caps.wChannelMask)),
                                              caps.vDriverVersion, caps.wMid, caps.wPid);
        }

        public IInputDevice OpenInputDevice(int portId)
        {
            return new Win32InputDevice(portId);
        }

        public IOutputDevice OpenOutputDevice(int portId)
        {
            return new Win32OutputDevice(portId);
        }
    }
}