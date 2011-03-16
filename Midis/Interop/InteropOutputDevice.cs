// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System;
    using Midis.Abstraction;

    public class InteropOutputDevice : IOutputDevice
    {
        private readonly IntPtr handle;

        public InteropOutputDevice(int portId)
        {
            NativeMethods.midiOutOpen(ref this.handle, portId, MidiProc, 0, NativeConstants.CALLBACK_FUNCTION);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void ShortMessage(int message)
        {
            NativeMethods.midiOutShortMsg(this.handle, message);
        }

        private static void MidiProc(IntPtr intPtr, int message, int instance, int param1, int param2) {}

        ~InteropOutputDevice()
        {
            this.Dispose(false);
        }

        public void Close() {}

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // cleanup managed resources
                GC.SuppressFinalize(this);
            }

            NativeMethods.midiOutClose(this.handle);
        }
    }
}