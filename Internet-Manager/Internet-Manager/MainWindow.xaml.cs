using SharpPcap;
using System.Linq;
using System.Windows;

namespace Internet_Manager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadInformation();
        }
        private void LoadInformation()
        {
            CaptureDeviceList deviceList = CaptureDeviceList.Instance;
            var list = deviceList.Select(device => new { Name = device.Name, Description = device.Description, Status = CheckDeviceStatus(device) }).ToList();
            var devices = list;
            DevicesTable.ItemsSource = devices;
        }
        private string CheckDeviceStatus(ICaptureDevice device)
        {
            try
            {
                device.Open(); 
                device.Close();
                return "Available";
            }
            catch
            {
                return "Unavailable";
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadInformation();
        }
    }
}