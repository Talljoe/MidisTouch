// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OutputChannel
    {
        private readonly OutputPort port;
        private readonly IEnumerable<int> channels;

        public OutputChannel(OutputPort port, IEnumerable<int> channels)
        {
            if(port == null)
                throw new ArgumentNullException("port");
            if(channels.Any(c => c < 0 || c > 15)) 
                throw new ArgumentOutOfRangeException("channels", @"Invalid channel number");

            this.port = port;
            this.channels = channels;
        }

        public void NoteOn(int note, int velocity = 127)
        {
            Send(ChannelMessage.NoteOn, note, velocity);
        }

        public void NoteOff(int note, int velocity = 0)
        {
            Send(ChannelMessage.NoteOff, note, velocity);
        }

        private void Send(ChannelMessage message, int value1, int value2)
        {
            this.channels.Run(channel => this.port.SendChannel(channel, message, value1, value2));
        }
    }
}