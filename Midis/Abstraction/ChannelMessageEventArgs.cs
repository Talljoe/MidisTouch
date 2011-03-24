// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Abstraction
{
    using System;

    public class ChannelMessageEventArgs : EventArgs
    {
        public ChannelMessageEventArgs(int message, int value1, int value2)
        {
            this.Message = message;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public int Message { get; private set; }
        public int Value1 { get; private set; }
        public int Value2 { get; private set; }
    }

    public class SystemMessageEventArgs : EventArgs
    {
        public SystemMessageEventArgs(int status, int channel, int value1, int value2)
        {
            this.Status = status;
            this.Channel = channel;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public int Status { get; private set; }
        public int Channel { get; private set; }
        public int Value1 { get; private set; }
        public int Value2 { get; private set; }
    }
}