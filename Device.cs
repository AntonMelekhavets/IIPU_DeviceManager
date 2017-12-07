using System.Linq;
using System.Management;


namespace DeviceManager
{
    class Device
    {
        public string DevicePath { get; set; }

        public string DeviceDescription { get; set; }

        public string Guid { get; set; }

        public string HardwareId { get; set; }

        public string Manufacturer { get; set; }

        public string DriverDescription { get; set; }

        public string DriverPath { get; set; }

        public bool Status { get; set; }
        
    }
}
