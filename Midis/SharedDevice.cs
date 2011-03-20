// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Disposables;
    using System.Threading;
    using Midis.Abstraction;

    internal abstract class SharedDevice<T> : IDisposable where T : class, IDevice
    {
        private readonly object gate = new object();
        private bool closed;
        private int count;

        public bool Closed
        {
            get { return this.closed; }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        ~SharedDevice()
        {
            this.Dispose(false);
        }

        public T Get()
        {
            lock (this.gate)
            {
                if (this.closed)
                {
                    return null;
                }
                Interlocked.Increment(ref this.count);
            }

            return this.CreateDevice(Disposable.Create(this.Release));
        }

        private void Release()
        {
            if (Interlocked.Decrement(ref this.count) == 0)
            {
                this.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var doDispose = false;
                lock (this.gate)
                {
                    if (!this.closed && this.count == 0)
                    {
                        doDispose = true;
                        this.closed = true;
                    }
                }
                if (doDispose)
                {
                    this.CloseDevice();
                    GC.SuppressFinalize(this);
                }
            }
        }

        protected abstract void CloseDevice();

        protected abstract T CreateDevice(IDisposable create);

        #region Nested type: DeviceWrapper

        protected class DeviceWrapper : IDisposable
        {
            private readonly IDisposable disposable;

            public DeviceWrapper(IDisposable disposable)
            {
                this.disposable = disposable;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~DeviceWrapper()
            {
                this.Dispose(false);
            }

            public void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.disposable.Dispose();
                }
            }
        }

        #endregion
    }
}