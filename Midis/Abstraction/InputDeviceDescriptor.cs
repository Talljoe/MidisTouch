// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Abstraction
{
    public class InputDeviceDescriptor
    {
        private readonly uint driverVersion;
        private readonly int id;
        private readonly int manufacturerId;
        private readonly string name;
        private readonly int productId;

        public InputDeviceDescriptor(int id, string name, uint driverVersion, int manufacturerId, int productId)
        {
            this.id = id;
            this.name = name;
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