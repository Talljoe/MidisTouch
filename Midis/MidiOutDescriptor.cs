// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;

    public class MidiOutDescriptor {
        private readonly int portId;
        private readonly string name;
        private readonly PortType portType;
        private readonly BitArray wChannelMask;

        public MidiOutDescriptor(int portId, string name, PortType portType, BitArray wChannelMask)
        {
            this.portId = portId;
            this.name = name;
            this.portType = portType;
            this.wChannelMask = wChannelMask;
        }

        public int PortId
        {
            get { return this.portId; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public PortType PortType
        {
            get { return this.portType; }
        }

        public BitArray WChannelMask
        {
            get { return this.wChannelMask; }
        }
    }
}