// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Windows.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct mmtime_tag_midi
    {
        /// DWORD->unsigned int
        public uint songptrpos;
    }
}