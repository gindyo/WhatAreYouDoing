using System.Media;
using System.Windows;
using System.Windows.Threading;
using WhatAreYouDoing.Display.Main;
using WhatAreYouDoing.Interfaces;

namespace WhatAreYouDoing.Startup
{
    public class ApplicationHandler : IApplicationHandler
    {
        private readonly IApplicationWrapper _app;

        public ApplicationHandler(IApplicationWrapper app)
        {
            _app = app;
        }

        public virtual Dispatcher Dispatcher
        {
            get { return _app.Dispatcher; }
        }

        public virtual void Shutdown()
        {
            _app.Shutdown();
        }

        public bool WindowIsOpen()
        {
            return _app.MainWindow != null && _app.IsMainWindowLoaded;
        }

        public void CloseCurrentWindow()
        {
            _app.MainWindow.Close();
            _app.MainWindow = null;
        }

        public void PopWindow(MainWindow mWindow)
        {
            if (WindowIsOpen())
                return;
            MainWindow mainWindow = mWindow;
            mainWindow.TextBox.Focus();
            mainWindow.Title = "What are you doing?";
            _app.MainWindow = mainWindow;
            _app.ShowMainWindow();
            _app.ActivateMainWindow();
            _app.PlaySound();
        }
    }
}