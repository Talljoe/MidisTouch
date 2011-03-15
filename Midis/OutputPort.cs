// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using Midis.Interop;

    public class OutputPort : IDisposable
    {
        private readonly IntPtr handle;
        private readonly int id;

        public OutputPort(int id, Func<NativeMethods.MidiOutProc, IntPtr> initFunction)
        {
            this.id = id;
            this.handle = initFunction(HandleMessage);
        }

        public int Id
        {
            get { return this.id; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OutputPort()
        {
            this.Dispose(false);
        }

        private static void HandleMessage(IntPtr intPtr, int message, int instance, int param1, int param2) {}

        private void Dispose(bool disposing)
        {
            if(disposing)
            {
                // cleanup managed resources
            }
            NativeMethods.midiOutClose(this.handle);
        }

        public void Send(int status, int val1, int val2)
        {
            NativeMethods.midiOutShortMsg(this.handle, BitConverter.ToInt32(new[] { (byte)status, (byte)val1, (byte)val2, (byte)0 }, 0));
        }

        public void SendChannel(int channel, ChannelMessage message, int val1, int val2)
        {
            if (channel < 0 || channel > 15)
                throw new ArgumentOutOfRangeException("channel", @"Invalid channel number");
            this.Send((int)message | channel, val1, val2);
        }

        public OutputChannel OpenChannels(params int[] channels)
        {
            return new OutputChannel(this, channels);
        }
    }
}