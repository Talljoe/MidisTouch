// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Components
{
    using System;

    public static class ComponentExtensions
    {
        public static BreakoutBox BreakOut(this IObservable<ChannelMessage> source)
        {
            return new BreakoutBox(source);
        }
    }
}