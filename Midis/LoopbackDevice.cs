// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using Midis.Abstraction;

    public class LoopbackDevice : IInputDevice, IOutputDevice
    {
        public void Dispose() {}

        public event EventHandler<ChannelMessageEventArgs> ChannelMessage;

        public void ShortMessage(int message)
        {
            var bytes = BitConverter.GetBytes(message);
            var status = bytes[0];
            if (status >= 0x80 && status <= 0xef)
            {
                this.InvokeChannelMessage(new ChannelMessageEventArgs(
                                              status & 0xf0, bytes[0] & 0x0f, bytes[1], bytes[2]));
            }
        }

        private void InvokeChannelMessage(ChannelMessageEventArgs e)
        {
            var handler = this.ChannelMessage;
            if (handler != null) handler(this, e);
        }
    }
}