// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tagMIDIOUTCAPSW
    {
        /// WORD->unsigned short
        public ushort wMid;

        /// WORD->unsigned short
        public ushort wPid;

        /// MMVERSION->UINT->unsigned int
        public uint vDriverVersion;

        /// WCHAR[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szPname;

        /// WORD->unsigned short
        public ushort wTechnology;

        /// WORD->unsigned short
        public ushort wVoices;

        /// WORD->unsigned short
        public ushort wNotes;

        /// WORD->unsigned short
        public ushort wChannelMask;

        /// DWORD->unsigned int
        public uint dwSupport;
    }
}