// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Devices
{
    using System.Collections;
    using Midis.Abstraction;

    public class NullHal : IMidiHAL
    {
        private readonly NullDevice device = new NullDevice();

        public int GetInputDeviceCount()
        {
            return 0;
        }

        public int GetOutputDeviceCount()
        {
            return 0;
        }

        public IOutputDevice OpenOutputDevice(int portId)
        {
            return this.device;
        }

        public IInputDevice OpenInputDevice(int portId)
        {
            return this.device;
        }

        public InputDeviceDescriptor GetInputDescriptor(int portId)
        {
            return new InputDeviceDescriptor(portId, "[NULL]", 0, 0, 0);
        }

        public OutputDeviceDescriptor GetOutputDescriptor(int portId)
        {
            return new OutputDeviceDescriptor(portId, "[NULL]", PortType.Port, new BitArray(16, false), 0, 0, 0);
        }
    }
}