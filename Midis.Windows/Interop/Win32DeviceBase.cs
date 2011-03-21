// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Windows.Interop
{
    using System;

    public abstract class Win32DeviceBase : IDisposable
    {
        protected IntPtr Handle;

        public void Dispose()
        {
            this.Dispose(true);
        }

        ~Win32DeviceBase()
        {
            this.Dispose(false);
        }

        public void Close()
        {
            this.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // cleanup managed resources
                GC.SuppressFinalize(this);
            }

            if (this.Handle != IntPtr.Zero)
            {
                this.CloseDevice();
                this.Handle = IntPtr.Zero;
            }
        }

        protected abstract void CloseDevice();
    }
}