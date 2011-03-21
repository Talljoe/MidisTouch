// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Windows.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class NativeMethods
    {
        #region Delegates

        public delegate void MidiInProc(IntPtr handle, int message, int instance, int param1, int param2);

        public delegate void MidiOutProc(IntPtr handle, int message, int instance, int param1, int param2);

        #endregion

        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///uPatch: UINT->unsigned int
        ///pwkya: LPWORD->WORD*
        ///fuCache: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutCacheDrumPatches")]
        public static extern uint midiOutCacheDrumPatches(IntPtr hmo, uint uPatch, ref ushort pwkya, uint fuCache);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutUnprepareHeader")]
        public static extern uint midiOutUnprepareHeader(IntPtr hmo, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInUnprepareHeader")]
        public static extern uint midiInUnprepareHeader(IntPtr hmi, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutPrepareHeader")]
        public static extern uint midiOutPrepareHeader(IntPtr hmo, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///mmrError: MMRESULT->UINT->unsigned int
        ///pszText: LPWSTR->WCHAR*
        ///cchText: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutGetErrorTextW")]
        public static extern uint midiOutGetErrorTextW(uint mmrError,
                                                       [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszText,
                                                       uint cchText);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///mmrError: MMRESULT->UINT->unsigned int
        ///pszText: LPSTR->CHAR*
        ///cchText: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutGetErrorTextA")]
        public static extern uint midiOutGetErrorTextA(uint mmrError,
                                                       [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszText,
                                                       uint cchText);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///uBank: UINT->unsigned int
        ///pwpa: LPWORD->WORD*
        ///fuCache: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutCachePatches")]
        public static extern uint midiOutCachePatches(IntPtr hmo, uint uBank, ref ushort pwpa, uint fuCache);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInPrepareHeader")]
        public static extern uint midiInPrepareHeader(IntPtr hmi, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///mmrError: MMRESULT->UINT->unsigned int
        ///pszText: LPWSTR->WCHAR*
        ///cchText: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInGetErrorTextW")]
        public static extern uint midiInGetErrorTextW(uint mmrError,
                                                      [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszText,
                                                      uint cchText);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///mmrError: MMRESULT->UINT->unsigned int
        ///pszText: LPSTR->CHAR*
        ///cchText: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInGetErrorTextA")]
        public static extern uint midiInGetErrorTextA(uint mmrError,
                                                      [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszText,
                                                      uint cchText);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        ///lppropdata: LPBYTE->BYTE*
        ///dwProperty: DWORD->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiStreamProperty")]
        public static extern uint midiStreamProperty(IntPtr hms, ref byte lppropdata, uint dwProperty);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        ///lpmmt: LPMMTIME->mmtime_tag*
        ///cbmmt: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiStreamPosition")]
        public static extern uint midiStreamPosition(IntPtr hms, ref mmtime_tag lpmmt, uint cbmmt);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///uDeviceID: UINT_PTR->unsigned int
        ///pmoc: LPMIDIOUTCAPSW->tagMIDIOUTCAPSW*
        ///cbmoc: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutGetDevCapsW")]
        public static extern uint midiOutGetDevCapsW(int uDeviceID, ref tagMIDIOUTCAPSW pmoc, int cbmoc);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///uDeviceID: UINT_PTR->unsigned int
        ///pmoc: LPMIDIOUTCAPSA->tagMIDIOUTCAPSA*
        ///cbmoc: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutGetDevCapsA")]
        public static extern uint midiOutGetDevCapsA([MarshalAs(UnmanagedType.SysUInt)] uint uDeviceID,
                                                     ref tagMIDIOUTCAPSA pmoc, uint cbmoc);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        [DllImport("winmm.dll", EntryPoint = "midiStreamRestart")]
        public static extern uint midiStreamRestart(IntPtr hms);


        /// Return Type: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutGetNumDevs")]
        public static extern int midiOutGetNumDevs();


        ///Return Type: MMRESULT->UINT->unsigned int
        ///uDeviceID: UINT_PTR->unsigned int
        ///pmic: LPMIDIINCAPSW->tagMIDIINCAPSW*
        ///cbmic: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInGetDevCapsW")]
        public static extern uint midiInGetDevCapsW(int uDeviceID, ref tagMIDIINCAPSW pmic, int cbmic);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///uDeviceID: UINT_PTR->unsigned int
        ///pmic: LPMIDIINCAPSA->tagMIDIINCAPSA*
        ///cbmic: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInGetDevCapsA")]
        public static extern uint midiInGetDevCapsA([MarshalAs(UnmanagedType.SysUInt)] uint uDeviceID,
                                                    ref tagMIDIINCAPSA pmic, uint cbmic);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///dwVolume: DWORD->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutSetVolume")]
        public static extern uint midiOutSetVolume(IntPtr hmo, uint dwVolume);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///pdwVolume: LPDWORD->DWORD*
        [DllImport("winmm.dll", EntryPoint = "midiOutGetVolume")]
        public static extern uint midiOutGetVolume(IntPtr hmo, ref uint pdwVolume);


        /// Return Type: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInGetNumDevs")]
        public static extern int midiInGetNumDevs();


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        [DllImport("winmm.dll", EntryPoint = "midiStreamPause")]
        public static extern uint midiStreamPause(IntPtr hms);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        [DllImport("winmm.dll", EntryPoint = "midiStreamClose")]
        public static extern uint midiStreamClose(IntPtr hms);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///dwMsg: DWORD->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutShortMsg")]
        public static extern int midiOutShortMsg(IntPtr hmo, int dwMsg);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInAddBuffer")]
        public static extern uint midiInAddBuffer(IntPtr hmi, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        [DllImport("winmm.dll", EntryPoint = "midiStreamStop")]
        public static extern uint midiStreamStop(IntPtr hms);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///phms: LPHMIDISTRM->HMIDISTRM*
        ///puDeviceID: LPUINT->UINT*
        ///cMidi: DWORD->unsigned int
        ///dwCallback: DWORD_PTR->ULONG_PTR->unsigned int
        ///dwInstance: DWORD_PTR->ULONG_PTR->unsigned int
        ///fdwOpen: DWORD->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiStreamOpen")]
        public static extern uint midiStreamOpen(ref IntPtr phms, ref uint puDeviceID, uint cMidi, uint dwCallback,
                                                 uint dwInstance, uint fdwOpen);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///uMsg: UINT->unsigned int
        ///dw1: DWORD_PTR->ULONG_PTR->unsigned int
        ///dw2: DWORD_PTR->ULONG_PTR->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutMessage")]
        public static extern uint midiOutMessage(IntPtr hmo, uint uMsg, uint dw1, uint dw2);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutLongMsg")]
        public static extern uint midiOutLongMsg(IntPtr hmo, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDI->HMIDI__*
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///pReserved: LPVOID->void*
        [DllImport("winmm.dll", EntryPoint = "midiDisconnect")]
        public static extern uint midiDisconnect(IntPtr hmi, IntPtr hmo, IntPtr pReserved);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hms: HMIDISTRM->HMIDISTRM__*
        ///pmh: LPMIDIHDR->midihdr_tag*
        ///cbmh: UINT->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiStreamOut")]
        public static extern uint midiStreamOut(IntPtr hms, ref midihdr_tag pmh, uint cbmh);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        ///uMsg: UINT->unsigned int
        ///dw1: DWORD_PTR->ULONG_PTR->unsigned int
        ///dw2: DWORD_PTR->ULONG_PTR->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInMessage")]
        public static extern uint midiInMessage(IntPtr hmi, uint uMsg, uint dw1, uint dw2);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        [DllImport("winmm.dll", EntryPoint = "midiOutReset")]
        public static extern uint midiOutReset(IntPtr hmo);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///puDeviceID: LPUINT->UINT*
        [DllImport("winmm.dll", EntryPoint = "midiOutGetID")]
        public static extern uint midiOutGetID(IntPtr hmo, ref uint puDeviceID);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmo: HMIDIOUT->HMIDIOUT__*
        [DllImport("winmm.dll", EntryPoint = "midiOutClose")]
        public static extern uint midiOutClose(IntPtr hmo);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///phmo: LPHMIDIOUT->HMIDIOUT*
        ///uDeviceID: UINT->unsigned int
        ///dwCallback: DWORD_PTR->ULONG_PTR->unsigned int
        ///dwInstance: DWORD_PTR->ULONG_PTR->unsigned int
        ///fdwOpen: DWORD->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiOutOpen")]
        public static extern uint midiOutOpen(out IntPtr phmo, int uDeviceID, MidiOutProc dwCallback, uint dwInstance,
                                              uint fdwOpen);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        [DllImport("winmm.dll", EntryPoint = "midiInStart")]
        public static extern uint midiInStart(IntPtr hmi);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        [DllImport("winmm.dll", EntryPoint = "midiInReset")]
        public static extern uint midiInReset(IntPtr hmi);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        ///puDeviceID: LPUINT->UINT*
        [DllImport("winmm.dll", EntryPoint = "midiInGetID")]
        public static extern uint midiInGetID(IntPtr hmi, ref uint puDeviceID);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        [DllImport("winmm.dll", EntryPoint = "midiInClose")]
        public static extern uint midiInClose(IntPtr hmi);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDI->HMIDI__*
        ///hmo: HMIDIOUT->HMIDIOUT__*
        ///pReserved: LPVOID->void*
        [DllImport("winmm.dll", EntryPoint = "midiConnect")]
        public static extern uint midiConnect(IntPtr hmi, IntPtr hmo, IntPtr pReserved);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///hmi: HMIDIIN->HMIDIIN__*
        [DllImport("winmm.dll", EntryPoint = "midiInStop")]
        public static extern uint midiInStop(IntPtr hmi);


        ///Return Type: MMRESULT->UINT->unsigned int
        ///phmi: LPHMIDIIN->HMIDIIN*
        ///uDeviceID: UINT->unsigned int
        ///dwCallback: DWORD_PTR->ULONG_PTR->unsigned int
        ///dwInstance: DWORD_PTR->ULONG_PTR->unsigned int
        ///fdwOpen: DWORD->unsigned int
        [DllImport("winmm.dll", EntryPoint = "midiInOpen")]
        public static extern uint midiInOpen(out IntPtr phmi, int uDeviceID, MidiInProc dwCallback, uint dwInstance,
                                             uint fdwOpen);
    }
}