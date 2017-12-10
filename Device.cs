using System.Linq;
using System.Management;


namespace Laba_5
{
    internal class Device
    {
        public string DevicePath { get; set; }

        public string DeviceDescription { get; set; }

        public string Guid { get; set; }

        public string HardwareId { get; set; }

        public string Manufacturer { get; set; }

        public string DriverDescription { get; set; }

        public string DriverPath { get; set; }

        public bool Status { get; set; }

        public void DisEnable(string operationType)
        {
            var devices = new ManagementObjectSearcher("SELECT * FROM Win32_PNPEntity");
            var device = devices.Get()
                .OfType<ManagementObject>()
                .FirstOrDefault(x => x["DeviceID"].ToString().Contains(DevicePath));
            device?.InvokeMethod(operationType, new object[] {false});
        }
    }
}