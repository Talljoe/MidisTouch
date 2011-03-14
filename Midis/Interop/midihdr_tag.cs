// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct midihdr_tag
    {
        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)] public string lpData;

        /// DWORD->unsigned int
        public uint dwBufferLength;

        /// DWORD->unsigned int
        public uint dwBytesRecorded;

        /// DWORD_PTR->ULONG_PTR->unsigned int
        public uint dwUser;

        /// DWORD->unsigned int
        public uint dwFlags;

        /// midihdr_tag*
        public IntPtr lpNext;

        /// DWORD_PTR->ULONG_PTR->unsigned int
        public uint reserved;

        /// DWORD->unsigned int
        public uint dwOffset;

        /// DWORD_PTR[8]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)] public uint[] dwReserved;
    }
}