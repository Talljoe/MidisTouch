// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PortExtensions
    {
        public static void SendMessage(this IEnumerable<OutputPort> ports, ChannelMessage message)
        {
            ports.Run(port => port.SendMessage(message));
        }

        public static void Connect(this IEnumerable<OutputPort> ports, IObservable<ChannelMessage> source)
        {
            ports.Run(port => port.Connect(source));
        }

        public static IObservable<ChannelMessage> ChannelMessages(this IEnumerable<InputPort> ports)
        {
            return ports.Select(port => port.ChannelMessages).Merge();
        }
    }
}