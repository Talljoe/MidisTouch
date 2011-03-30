// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Linq;

    public static class ControllerChangeExtensions
    {
        public static IObservable<ChannelMessage> ToRange(this IObservable<ChannelMessage> source, int floor, int ceiling)
        {
            if (floor < 0 || floor > 127)
                throw new ArgumentOutOfRangeException("floor", "Value must be between 0 and 127.");
            if(ceiling < 0 || ceiling > 127)
                throw new ArgumentOutOfRangeException("ceiling", "Value must be between 0 and 127.");
            var multiplier = (decimal) ((Math.Max(ceiling, floor) - Math.Min(ceiling,floor)) + 1)/128;
            var adder = Math.Min(ceiling, floor);
            return source.Select(cm =>
                                     {
                                         cm.Value2 = (int) (cm.Value2*multiplier) + adder;
                                         return cm;
                                     })
                         .DistinctUntilChanged(cm => cm.Value2);
        }

        public static IObservable<ChannelMessage> Clamp(this IObservable<ChannelMessage> source, int floor, int ceiling)
        {
            if (floor < 0 || floor > 127)
                throw new ArgumentOutOfRangeException("floor", "Value must be between 0 and 127.");
            if(ceiling < 0 || ceiling > 127)
                throw new ArgumentOutOfRangeException("ceiling", "Value must be between 0 and 127.");

            if(floor > ceiling)
            {
                var temp = floor;
                floor = ceiling;
                ceiling = temp;
            }

            return source.Select(cm =>
                                     {
                                         cm.Value2 = Math.Min(ceiling, Math.Max(floor, cm.Value2));
                                         return cm;
                                     })
                          .DistinctUntilChanged(cm => cm.Value2);
        }
    }
}