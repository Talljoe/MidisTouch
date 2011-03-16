// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Abstraction
{
    public interface IMidiHAL
    {
        int GetInputDeviceCount();
        int GetOutputDeviceCount();
        IOutputDevice OpenOutputDevice(int portId);
        InputDeviceDescriptor GetInputDescriptor(int portId);
        OutputDeviceDescriptor GetOutputDescriptor(int portId);
    }
}