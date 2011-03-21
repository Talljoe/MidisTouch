// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class ChannelExtensions
    {
        public static IObservable<ChannelMessage> ToChannel(this IObservable<ChannelMessage> source, int channel)
        {
            return source.ToChannels(channel);
        }

        public static IObservable<ChannelMessage> ToChannels(this IObservable<ChannelMessage> source, BitArray channels)
        {
            return source.ToChannels(Enumerable.Range(0, channels.Count).Where(i => channels[i]).ToArray());
        }

        public static IObservable<ChannelMessage> ToChannels(this IObservable<ChannelMessage> source, params int[] channels)
        {
            return source.ToChannels((IEnumerable<int>) channels);
        }

        public static IObservable<ChannelMessage> ToChannels(this IObservable<ChannelMessage> source, IEnumerable<int> channels)
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

        public static IObservable<ChannelMessage> OnChannels(this IObservable<ChannelMessage> source, params int[] channels)
        {
            return source.OnChannels((IEnumerable<int>)channels);
        }

        public static IObservable<ChannelMessage> OnChannels(this IObservable<ChannelMessage> source, IEnumerable<int> channels)
        {
            var set = new HashSet<int>(channels);
            return source.Where(message => set.Contains(message.Channel));
        }

        public static IObservable<ChannelMessage> OfMessageType(this IObservable<ChannelMessage> source, ChannelMessageType type)
        {
            return source.Where(message => message.MessageType == type);
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
    }
}