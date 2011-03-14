// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct mmtime_tag_union
    {
        /// DWORD->unsigned int
        [FieldOffset(0)] public uint ms;

        /// DWORD->unsigned int
        [FieldOffset(0)] public uint sample;

        /// DWORD->unsigned int
        [FieldOffset(0)] public uint cb;

        /// DWORD->unsigned int
        [FieldOffset(0)] public uint ticks;

        /// mmtime_tag_smpte
        [FieldOffset(0)] public mmtime_tag_smpte smpte;

        /// mmtime_tag_midi
        [FieldOffset(0)] public mmtime_tag_midi midi;
    }
}