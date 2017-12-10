using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;

namespace Laba_5
{
    internal class DeviceSearcher
    {
        public List<Device> GetDevices()
        {
            var deviceList = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
            var devices = new List<Device>();
            foreach (var entityObject in deviceList.Get())
            {
                var device = new Device
                {
                    Guid = entityObject["ClassGuid"]?.ToString(),
                    HardwareId = string.Join("/r/n", entityObject["HardwareId"] as string[] ?? new string[] { }),
                    Manufacturer = entityObject["Manufacturer"]?.ToString(),
                    DevicePath = entityObject["DeviceId"]?.ToString(),
                    DeviceDescription = entityObject["Description"]?.ToString(),
                    Status = entityObject["Status"].ToString().Contains("OK") ? true : false,
                };
                SearchDriver(device, entityObject["Service"]?.ToString());
                devices.Add(device);
            }
            return devices;
        }

        private static void SearchDriver(Device device, string service)
        {
            if (string.IsNullOrEmpty(service)) return;
            var searcher =
                new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_SystemDriver WHERE Name = '{Regex.Escape(service)}'");
            foreach (var driver in searcher.Get())
            {
                device.DriverDescription = driver["Description"]?.ToString();
                device.DriverPath = driver["PathName"]?.ToString();
            }
        }
    }
}