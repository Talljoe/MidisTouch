// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Devices
{
    using System;
    using Midis.Abstraction;

    internal class SharedInputDevice : SharedDevice<IInputDevice>
    {
        private readonly IInputDevice device;

        public SharedInputDevice(IInputDevice device)
        {
            this.device = device;
        }

        protected override void CloseDevice()
        {
            this.device.Dispose();
        }

        protected override IInputDevice CreateDevice(IDisposable create)
        {
            return new InputDeviceWrapper(create, this.device);
        }

        #region Nested type: InputDeviceWrapper

        private class InputDeviceWrapper : DeviceWrapper, IInputDevice
        {
            private readonly IInputDevice device;

            public InputDeviceWrapper(IDisposable disposable, IInputDevice device)
                : base(disposable)
            {
                this.device = device;
            }

            public event EventHandler<ChannelMessageEventArgs> ChannelMessage
            {
                add { this.device.ChannelMessage += value; }
                remove { this.device.ChannelMessage -= value; }
            }
        }

        #endregion
    }
}