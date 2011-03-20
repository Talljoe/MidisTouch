// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using Midis.Abstraction;

    internal class SharedOutputDevice : SharedDevice<IOutputDevice>
    {
        private readonly IOutputDevice device;

        public SharedOutputDevice(IOutputDevice device)
        {
            this.device = device;
        }

        protected override void CloseDevice()
        {
            this.device.Dispose();
        }

        protected override IOutputDevice CreateDevice(IDisposable create)
        {
            return new OutputDeviceWrapper(create, this.device);
        }

        #region Nested type: OutputDeviceWrapper

        private class OutputDeviceWrapper : DeviceWrapper, IOutputDevice
        {
            private readonly IOutputDevice device;

            public OutputDeviceWrapper(IDisposable disposable, IOutputDevice device)
                : base(disposable)
            {
                this.device = device;
            }

            public void ShortMessage(int message)
            {
                this.device.ShortMessage(message);
            }
        }

        #endregion
    }
}