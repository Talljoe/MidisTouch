// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Disposables;
    using System.Threading;
    using Midis.Abstraction;

    public class LoopbackDevice : IInputDevice, IOutputDevice
    {
        private readonly BooleanDisposable disposable = new BooleanDisposable();

        public void Dispose()
        {
            lock(disposable)
            {
                this.disposable.Dispose();
            }
        }

        public event EventHandler<ChannelMessageEventArgs> ChannelMessage;

        public void ShortMessage(int message)
        {
            var bytes = BitConverter.GetBytes(message);
            var status = bytes[0];
            if (status >= 0x80 && status <= 0xef)
            {
                ThreadPool.QueueUserWorkItem(_ => this.InvokeChannelMessage(new ChannelMessageEventArgs(status, bytes[1], bytes[2])));
            }
        }

        private void InvokeChannelMessage(ChannelMessageEventArgs e)
        {
            lock (this.disposable)
            {
                if (!this.disposable.IsDisposed)
                {
                    var handler = this.ChannelMessage;
                    if (handler != null) handler(this, e);
                }
            }
        }
    }
}