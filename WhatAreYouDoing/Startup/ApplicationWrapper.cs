using System.Media;
using System.Windows;
using System.Windows.Threading;
using WhatAreYouDoing.Main;

namespace WhatAreYouDoing.Startup
{
    public class ApplicationWrapper : IApplicationWrapper
    {
        private readonly Application _app;

        public ApplicationWrapper()
        {
            _app = Application.Current;
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
            return _app.MainWindow != null && _app.MainWindow.IsLoaded;
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
            mainWindow.Show();
            mainWindow.Activate();
            SystemSounds.Beep.Play();
        }

        public virtual void SetMainWindow(MainWindow mWindow)
        {
            _app.MainWindow = mWindow;
        }

        public virtual MainWindow GetMainWindow()
        {
            return _app.MainWindow as MainWindow;
        }
    }
}