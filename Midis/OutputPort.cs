// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Disposables;
    using Midis.Abstraction;

    public class OutputPort : IDisposable
    {
        private readonly IOutputDevice device;
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        private readonly int id;

        public OutputPort(int id, IOutputDevice device)
        {
            this.id = id;
            this.device = device;
        }

        public int Id
        {
            get { return this.id; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OutputPort()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.device.Dispose();
                this.disposable.Dispose();
            }
        }

        public void Send(int status, int val1, int val2)
        {
            this.device.ShortMessage(BitConverter.ToInt32(new[] {(byte) status, (byte) val1, (byte) val2, (byte) 0}, 0));
        }

        public void SendChannel(int channel, ChannelMessageType messageType, int val1, int val2)
        {
            if (channel < 1 || channel > 16)
                throw new ArgumentOutOfRangeException("channel", @"Invalid channel number");
            this.Send((int) messageType | (channel - 1), val1, val2);
        }

        public void SendMessage(ChannelMessage message)
        {
            this.SendChannel(message.Channel, message.MessageType, message.Value1, message.Value2);
        }

        public OutputChannel OpenChannels(params int[] channels)
        {
            return new OutputChannel(this, channels);
        }

        public void Connect(IObservable<ChannelMessage> source)
        {
            this.disposable.Add(source.Subscribe(this.SendMessage));
        }
    }
}