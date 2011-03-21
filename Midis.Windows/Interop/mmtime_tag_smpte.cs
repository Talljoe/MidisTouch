// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Windows.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct mmtime_tag_smpte
    {
        /// BYTE->unsigned char
        public byte hour;

        /// BYTE->unsigned char
        public byte AnonymousMember1;

        /// BYTE->unsigned char
        public byte sec;

        /// BYTE->unsigned char
        public byte frame;

        /// BYTE->unsigned char
        public byte fps;

        /// BYTE->unsigned char
        public byte dummy;

        /// BYTE[2]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)] public byte[] pad;
    }
}