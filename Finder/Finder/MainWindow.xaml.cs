using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.DirectoryServices;
namespace Finder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThisPCButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrivesPanel.Visibility == Visibility.Collapsed)
            {
                DrivesPanel.Visibility = Visibility.Visible;
                DrivesPanel.Children.Clear();

                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        var driveButton = new Button
                        {
                            Content = $"{drive.Name} ({drive.VolumeLabel})",
                            Margin = new Thickness(5, 2, 0, 2)
                        };

                        driveButton.Click += DriveButton_Click;
                        DrivesPanel.Children.Add(driveButton);
                    }
                }
            }
            else
            {
                DrivesPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string driveName = button.Content.ToString().Split(' ')[0]; 
                DisplayDirectoryTree(driveName);
            }
        }

        private void DisplayDirectoryTree(string path)
        {
            DirectoryTree.Items.Clear();

            try
            {
                var rootItem = new TreeViewItem
                {
                    Header = path,
                    Tag = path
                };
                rootItem.Expanded += Folder_Expanded;
                DirectoryTree.Items.Add(rootItem);

                AddDummyNode(rootItem);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error accessing {path}: {ex.Message}");
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count == 1 && item.Items[0] is string dummy && dummy == "Loading...")
            {
                item.Items.Clear();
                foreach (var directory in Directory.GetDirectories(item.Tag.ToString()))
                {
                    var subItem = new TreeViewItem
                    {
                        Header = System.IO.Path.GetFileName(directory),
                        Tag = directory
                    };
                    subItem.Expanded += Folder_Expanded;
                    AddDummyNode(subItem);
                    item.Items.Add(subItem);
                }
                foreach (var file in Directory.GetFiles(item.Tag.ToString()))
                {
                    var fileItem = new TreeViewItem
                    {
                        Header = System.IO.Path.GetFileName(file),
                        Tag = file
                    };
                    item.Items.Add(fileItem);
                }
                
            }
        }
        private void AddDummyNode(TreeViewItem item)
        {
            item.Items.Add("Loading...");
        }
    }
}