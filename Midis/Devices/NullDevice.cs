﻿// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Devices
{
    using System;
    using Midis.Abstraction;

    public class NullDevice : IInputDevice, IOutputDevice
    {
        private static readonly IMidiHAL hal = new NullHal();
        public static IMidiHAL Hal { get { return hal; } }

        public void Dispose() {}

        public event EventHandler<ChannelMessageEventArgs> ChannelMessage { add { } remove { } }

        public void ShortMessage(int message) {}
    }
}