using System.Diagnostics;
using System.Linq;
using System.Windows;
namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();
        }
        private void LoadProcesses()
        {
            var processes = Process.GetProcesses().Where(p => { try { _ = p.MainModule; return true; } catch { return false; }}).Select(p => new{p.Id,p.ProcessName}).ToList();
            ProcessesGrid.ItemsSource = processes;
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }
        private void KillButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedProcess = ProcessesGrid.SelectedItem;
            if (selectedProcess != null)
            {
                var process = Process.GetProcessById((int)selectedProcess.Id);
                process.Kill();
                LoadProcesses();
            }
        }
        private bool isSortedAscending = false;
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var processes = Process.GetProcesses().Where(p => { try {_ = p.MainModule; return true; } catch { return false; }}).Select(p => new { p.Id, p.ProcessName }).ToList();

            if (isSortedAscending)
                processes = processes.OrderByDescending(p => p.ProcessName).ToList();
            else
                processes = processes.OrderBy(p => p.ProcessName).ToList();

            ProcessesGrid.ItemsSource = processes;
            isSortedAscending = !isSortedAscending;
        }
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedProcess = ProcessesGrid.SelectedItem;
            if (selectedProcess != null)
            {
                var process = Process.GetProcessById((int)selectedProcess.Id);
                MessageBox.Show($"ID: {process.Id}\n" +
                                $"Имя: {process.ProcessName}\n" +
                                $"Память: {process.PrivateMemorySize64 / 1024 / 1024}\n" +
                                $"Запущен: {process.StartTime}\n" +
                                $"Путь: {process.MainModule?.FileName}");
            }
        }
    }
}