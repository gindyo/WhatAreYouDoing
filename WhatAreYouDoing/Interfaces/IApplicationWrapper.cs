namespace WhatAreYouDoing.Interfaces
{
    public interface IApplicationWrapper
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
