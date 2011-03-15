// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis
{
    using System;
    using System.Collections;

    public class MidiOutDescriptor {
        private readonly int id;
        private readonly string name;
        private readonly PortType portType;
        private readonly BitArray wChannelMask;

        public MidiOutDescriptor(int id, string name, PortType portType, BitArray wChannelMask)
        {
            this.id = id;
            this.name = name;
            this.portType = portType;
            this.wChannelMask = wChannelMask;
        }

        public int Id
        {
            get { return this.id; }
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