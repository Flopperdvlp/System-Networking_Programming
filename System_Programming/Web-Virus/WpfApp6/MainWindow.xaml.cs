using System.Diagnostics;
using System.Windows;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenWebsites_Click(object sender, RoutedEventArgs e)
        {
            string[] urls =
            {
                "https://www.google.com",
                "https://www.youtube.com",
                "https://www.facebook.com",
                "https://www.twitter.com"
            };

            foreach (var url in urls)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true 
                });
            }
        }
    }
}
