// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using Midis.Abstraction;

    public class InputPort : IDisposable
    {
        private readonly IInputDevice device;
        private readonly int id;

        public InputPort(int id, IInputDevice device)
        {
            this.id = id;
            this.device = device;
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

        ~InputPort()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.device.Dispose();
            }
        }
    }
}