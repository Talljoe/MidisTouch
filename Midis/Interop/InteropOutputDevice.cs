// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System;
    using Midis.Abstraction;

    public class InteropOutputDevice : InteropDeviceBase, IOutputDevice
    {
        private readonly NativeMethods.MidiOutProc proc = MidiProc;

        public InteropOutputDevice(int portId)
        {
            NativeMethods.midiOutOpen(out this.Handle, portId, this.proc, 0, NativeConstants.CALLBACK_FUNCTION);
        }

        public void ShortMessage(int message)
        {
            NativeMethods.midiOutShortMsg(this.Handle, message);
        }

        private static void MidiProc(IntPtr intPtr, int message, int instance, int param1, int param2) {}

        protected override void CloseDevice()
        {
            NativeMethods.midiOutClose(this.Handle);
        }
    }
}