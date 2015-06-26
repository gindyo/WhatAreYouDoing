using System.Media;
using System.Windows;
using System.Windows.Threading;
using WhatAreYouDoing.Interfaces;

namespace WhatAreYouDoing.ThirdPartyWrappers
{
    class ApplicationWrapper : IApplicationWrapper
    {
        private readonly Application _app;
        private bool _isMainWindowLoaded;

        public Window MainWindow
        {
            get { return _app.MainWindow; }
            set { _app.MainWindow = value; }
        }

        public bool IsMainWindowLoaded
        {
            get { return MainWindow.IsLoaded; }
        }

        public void Shutdown()
        {
            _app.Shutdown();
        }

        public void ShowMainWindow()
        {
            MainWindow.Show();
        }

        public void ActivateMainWindow()
        {
            MainWindow.Activate();
        }

        public void PlaySound()
        {
            SystemSounds.Beep.Play();
        }

        public Dispatcher Dispatcher
        {
            get { return _app.Dispatcher; }
        }

        public ApplicationWrapper()
        {
            _app = Application.Current;
        }
    }
}