// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Linq;
    using Midis.Abstraction;

    public class InputPort : IDisposable
    {
        private readonly IObservable<ChannelMessage> channelMessages;
        private readonly IInputDevice device;
        private readonly int id;

        public InputPort(int id, IInputDevice device)
        {
            this.id = id;
            this.device = device;
            this.channelMessages = Observable.FromEvent<ChannelMessageEventArgs>(h => this.device.ChannelMessage += h,
                                                                                 h => this.device.ChannelMessage -= h)
                .Select(e => e.EventArgs)
                .Select(e => new ChannelMessage
                                 {
                                     MessageType = (ChannelMessageType) e.Status,
                                     Channel = e.Channel,
                                     Value1 = e.Value1,
                                     Value2 = e.Value2,
                                 });
        }

        public int Id
        {
            get { return this.id; }
        }

        public IObservable<ChannelMessage> ChannelMessages
        {
            get { return this.channelMessages; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~InputPort()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.device.Dispose();
            }
        }
    }
}