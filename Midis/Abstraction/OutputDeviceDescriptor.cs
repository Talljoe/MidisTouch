// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Abstraction
{
    using System.Collections;

    public class OutputDeviceDescriptor
    {
        private readonly uint driverVersion;
        private readonly int id;
        private readonly int manufacturerId;
        private readonly string name;
        private readonly PortType portType;
        private readonly int productId;
        private readonly BitArray wChannelMask;

        public OutputDeviceDescriptor(int id, string name, PortType portType, BitArray wChannelMask, uint driverVersion,
                                      int manufacturerId, int productId)
        {
            this.id = id;
            this.name = name;
            this.portType = portType;
            this.wChannelMask = wChannelMask;
            this.driverVersion = driverVersion;
            this.manufacturerId = manufacturerId;
            this.productId = productId;
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

        public uint DriverVersion
        {
            get { return this.driverVersion; }
        }

        public int ManufacturerId
        {
            get { return this.manufacturerId; }
        }

        public int ProductId
        {
            get { return this.productId; }
        }
    }
}