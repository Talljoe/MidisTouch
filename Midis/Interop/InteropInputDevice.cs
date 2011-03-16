// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System;
    using Midis.Abstraction;

    public class InteropInputDevice : InteropDeviceBase, IInputDevice
    {
        public InteropInputDevice(int portId)
        {
            NativeMethods.midiInOpen(ref this.Handle, portId, MidiProc, 0, NativeConstants.CALLBACK_FUNCTION);
        }

        private static void MidiProc(IntPtr intPtr, int message, int instance, int param1, int param2) {}

        protected override void CloseDevice()
        {
            NativeMethods.midiInClose(this.Handle);
        }
    }
}