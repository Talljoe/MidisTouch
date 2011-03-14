// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct mmtime_tag
    {
        /// UINT->unsigned int
        public uint wType;

        /// mmtime_tag_union
        public mmtime_tag_union u;
    }
}