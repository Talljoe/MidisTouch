// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Windows.Interop
{
    using System;
    using Midis.Abstraction;

    public class Win32InputDevice : Win32DeviceBase, IInputDevice
    {
        private readonly NativeMethods.MidiInProc proc;

        public Win32InputDevice(int portId)
        {
            this.proc = this.MidiProc;
            NativeMethods.midiInOpen(out this.Handle, portId, this.proc, 0, NativeConstants.CALLBACK_FUNCTION);
            NativeMethods.midiInStart(this.Handle);
        }

        public event EventHandler<ChannelMessageEventArgs> ChannelMessage;

        private void MidiProc(IntPtr intPtr, int message, int instance, int param1, int param2)
        {
            if ((MidiInMessage) message == MidiInMessage.Data)
            {
                var bytes = BitConverter.GetBytes(param1);
                int status = bytes[0];
                if (status >= 0x80 && status <= 0xef)
                {
                    this.InvokeChannelMessage(new ChannelMessageEventArgs(status, bytes[1], bytes[2]));
                }
            }
        }

        protected override void CloseDevice()
        {
            NativeMethods.midiInStop(this.Handle);
            NativeMethods.midiInClose(this.Handle);
        }

        private void InvokeChannelMessage(ChannelMessageEventArgs e)
        {
            var handler = this.ChannelMessage;
            if (handler != null) handler(this, e);
        }
    }
}