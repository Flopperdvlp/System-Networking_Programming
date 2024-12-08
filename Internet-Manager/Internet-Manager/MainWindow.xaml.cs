using SharpPcap;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Internet_Manager
{
    public partial class MainWindow : Window
    {
        private ICaptureDevice selectedDevice;

        public MainWindow()
        {
            InitializeComponent();
            LoadDevices();
        }

        private void LoadDevices()
        {
            var devices = CaptureDeviceList.Instance;
            DeviceList.ItemsSource = devices.Select(d => d.Description).ToList();
            DeviceList.SelectedIndex = 0;
        }

        private void StartCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            var devices = CaptureDeviceList.Instance;
            selectedDevice = devices[DeviceList.SelectedIndex];
            selectedDevice.OnPacketArrival += Device_OnPacketArrival;
            selectedDevice.Open();
            selectedDevice.StartCapture();
        }

        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            var rawPacket = e.GetPacket();
            StringBuilder packetDetails = new StringBuilder();
            byte[] packetData = rawPacket.Data;
            packetDetails.AppendLine("Первые 100 байт пакета:");
            for (int i = 0; i < Math.Min(packetData.Length, 100); i++)
            {
                packetDetails.AppendFormat("{0:X2} ", packetData[i]);
            }
            Dispatcher.Invoke(() =>
            {
                PacketDetails.Text += packetDetails.ToString() + "\n";
            });
        }
    }
}