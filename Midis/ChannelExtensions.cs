// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Midis.Utility;

    public static class ChannelExtensions
    {
        public static void ConnectTo(this IObservable<ChannelMessage> source, IEnumerable<OutputPort> ports)
        {
            ports.Connect(source);
        }

        public static void ConnectTo(this IObservable<ChannelMessage> source, params OutputPort[] ports)
        {
            ports.Connect(source);
        }

        public static void ConnectTo(this IObservable<ChannelMessage> source, OutputPort port)
        {
            port.Connect(source);
        }

        public static IObservable<ChannelMessage> ToChannel(this IObservable<ChannelMessage> source, int channel)
        {
            return source.ToChannels(channel);
        }

        public static IObservable<ChannelMessage> ToChannels(this IObservable<ChannelMessage> source, BitArray channels)
        {
            return source.ToChannels(Enumerable.Range(0, channels.Count).Where(i => channels[i]).ToArray());
        }

        public static IObservable<ChannelMessage> ToChannels(this IObservable<ChannelMessage> source,
                                                             params int[] channels)
        {
            return source.ToChannels((IEnumerable<int>) channels);
        }

        public static IObservable<ChannelMessage> ToChannels(this IObservable<ChannelMessage> source,
                                                             IEnumerable<int> channels)
        {
            return source.SelectMany(message => channels.ToObservable(),
                                     (message, channel) =>
                                         {
                                             message.Channel = channel;
                                             return message;
                                         });
        }

        public static IObservable<ChannelMessage> OnChannels(this IObservable<ChannelMessage> source, int channel)
        {
            return source.OnChannels(channel);
        }

        public static IObservable<ChannelMessage> OnChannels(this IObservable<ChannelMessage> source, BitArray channels)
        {
            return source.OnChannels(Enumerable.Range(0, channels.Count).Where(i => channels[i]).ToArray());
        }

        public static IObservable<ChannelMessage> OnChannels(this IObservable<ChannelMessage> source,
                                                             params int[] channels)
        {
            return source.OnChannels((IEnumerable<int>) channels);
        }

        public static IObservable<ChannelMessage> OnChannels(this IObservable<ChannelMessage> source,
                                                             IEnumerable<int> channels)
        {
            var set = new HashSet<int>(channels);
            return source.Where(message => set.Contains(message.Channel));
        }

        public static IObservable<ChannelMessage> OfMessageType(this IObservable<ChannelMessage> source,
                                                                ChannelMessageType type)
        {
            return source.Where(message => message.MessageType == type);
        }

        public static IObservable<ChannelMessage> OfMessageTypes(this IObservable<ChannelMessage> source,
                                                                 params ChannelMessageType[] types)
        {
            return source.OfMessageTypes((IEnumerable<ChannelMessageType>) types);
        }

        public static IObservable<ChannelMessage> OfMessageTypes(this IObservable<ChannelMessage> source,
                                                                 IEnumerable<ChannelMessageType> types)
        {
            var set = new HashSet<ChannelMessageType>(types);
            return source.Where(message => set.Contains(message.MessageType));
        }

        public static IObservable<bool> MomentaryButton(this IObservable<ChannelMessage> source, int controller)
        {
            return source.OfMessageType(ChannelMessageType.ControllerChange)
                .Where(message => message.Value1 == controller)
                .Select(message => message.Value2 >= 64)
                .DistinctUntilChanged();
        }

        public static IObservable<bool> ToggleButton(this IObservable<ChannelMessage> source, int controller)
        {
            return source.MomentaryButton(controller).Where(b => b).Scan((acc, _) => !acc);
        }

        public static IObservable<ChannelMessage> ToControllerMessage(this IObservable<bool> source, int controller,
                                                                      int channel = 1)
        {
            return source.Select(
                b => new ChannelMessage
                         {
                             MessageType = ChannelMessageType.ControllerChange,
                             Channel = channel,
                             Value1 = controller,
                             Value2 = b ? 127 : 0
                         });
        }

        public static Tuple<IObservable<T>, IObservable<T>> Split<T>(this IObservable<T> source, Func<T, bool> @switch)
        {
            source = source.Publish().RefCount();
            return new Tuple<IObservable<T>, IObservable<T>>(source.Where(@switch), source.Where(@switch.Not()));
        }

        public static IObservable<ChannelMessage> Splice(this IObservable<ChannelMessage> source,
                                                         Func<ChannelMessage, bool> @switch,
                                                         Func<ChannelMessage, ChannelMessage> selector1,
                                                         Func<ChannelMessage, ChannelMessage> selector2 = null)
        {
            Func<ChannelMessage, ChannelMessage> nullSelector = (x => x);
            selector1 = selector1 ?? nullSelector;
            selector2 = selector2 ?? nullSelector;
            return source.Select(message => @switch(message) ? selector1(message) : selector2(message));
        }

        public static IObservable<ChannelMessage> Splice(this IObservable<ChannelMessage> source,
                                                         ChannelMessageType messageType,
                                                         Func<ChannelMessage, ChannelMessage> selector1,
                                                         Func<ChannelMessage, ChannelMessage> selector2 = null)
        {
            return source.Splice(message => message.MessageType == messageType, selector1, selector2);
        }
    }
}