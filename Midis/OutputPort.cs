// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using Midis.Abstraction;

    public class OutputPort : IDisposable
    {
        private readonly IOutputDevice device;
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
            }
        }

        public void Send(int status, int val1, int val2)
        {
            this.device.ShortMessage(BitConverter.ToInt32(new[] {(byte) status, (byte) val1, (byte) val2, (byte) 0}, 0));
        }

        public void SendChannel(int channel, ChannelMessageType messageType, int val1, int val2)
        {
            if (channel < 0 || channel > 15)
                throw new ArgumentOutOfRangeException("channel", @"Invalid channel number");
            this.Send((int) messageType | channel, val1, val2);
        }

        public OutputChannel OpenChannels(params int[] channels)
        {
            return new OutputChannel(this, channels);
        }
    }
}