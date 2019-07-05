using System.Diagnostics;
using System.Windows;

namespace AttachToChrome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchBrowser_Click(object sender, RoutedEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            proc.StartInfo.Arguments = "https://www.intellitect.com/blog/ --new-window --remote-debugging-port=9222 --user-data-dir=C:\\Temp";
            proc.Start();
        }
    }
}
