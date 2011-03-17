// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    public class ChannelMessage
    {
        public ChannelMessageType MessageType { get; set; }
        public int Channel { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}