﻿// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Abstraction
{
    public interface IOutputDevice : IDevice
    {
        void ShortMessage(int message);
    }
}