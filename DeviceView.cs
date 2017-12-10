using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Laba_5;

namespace Laba_5
{
    public partial class DeviceView : Form
    {
        private static readonly DeviceSearcher Searcher = new DeviceSearcher();
        private List<Device> _deviceList;

        public DeviceView()
        {
            InitializeComponent();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            _deviceList = new List<Device>();
            CreateForm();
        }

        private void CreateForm()
        {
            devicesList.Items.Clear();
            _deviceList = Searcher.GetDevices();

            foreach (var device in _deviceList)
            {
                var deviceInfo = new ListViewItem(device.DeviceDescription);
                deviceInfo.SubItems.AddRange(new[]
                {
                    device.Status.ToString(),
                    device.Guid,
                    device.DevicePath,
                    device.HardwareId,
                    device.Manufacturer,
                    device.DriverDescription,
                    device.DriverPath
                });
                devicesList.Items.Add(deviceInfo);
            }
        }

        private void DisableEnableDevice(object sender, MouseEventArgs e)
        {
            var item = (sender as ListView).HitTest(e.Location).Item.SubItems;
            var device = _deviceList[devicesList.HitTest(e.Location).Item.Index];
            if (device.Status)
            {
                device.DisEnable("Disable");
                MessageBox.Show(@"Disable complete", @"Status", MessageBoxButtons.OK);
                device.Status = false;
                item[1].Text = false.ToString();
            }
            else
            {
                device.DisEnable("Enable");
                MessageBox.Show(@"Enable complete", @"Status", MessageBoxButtons.OK);
                device.Status = true;
                item[1].Text = true.ToString();
            }
        }

        private void devicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}