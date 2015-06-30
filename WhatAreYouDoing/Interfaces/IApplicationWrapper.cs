using System.Windows;
using System.Windows.Threading;

ace IApplicationWrapper
    {
        System.Windows.Threading.Dispatcher Dispatcher { get; }
        System.Windows.Window MainWindow { get; set; }
        bool IsMainWindowLoaded { get; }
        void Shutdown();
        void ShowMainWindow();
        void ActivateMainWindow();
        void PlaySound();
    }
}
