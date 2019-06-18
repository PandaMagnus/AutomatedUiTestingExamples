using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.Windows.Input;

namespace AttachToChrome.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            LaunchBrowser = new RelayCommand(OnLaunchBrowser);
        }

        public ICommand LaunchBrowser { get; }

        private void OnLaunchBrowser()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            proc.StartInfo.Arguments = "https://www.intellitect.com/blog/ --new-window --remote-debugging-port=9222 --user-data-dir=C:\\Temp";
            proc.Start();
        }
    }
}