// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    public class MidiInDescriptor
    {
        private readonly string name;
        private readonly int portId;

        public MidiInDescriptor(int portId, string name)
        {
            this.portId = portId;
            this.name = name;
        }

        public int PortId
        {
            get { return this.portId; }
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}