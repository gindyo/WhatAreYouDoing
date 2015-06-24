using System;
using System.Windows;
using System.Windows.Input;

namespace WhatAreYouDoing
{

    public class NotifyIconViewModel
    {

        public NotifyIconViewModel()
        {
            
        }
        public Func<MainWindow> MainWindowFactory { get; set; }
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsActive)
                            return;
                        var mainWindow = MainWindowFactory();
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.TextBox.Focus();
                        mainWindow.Title = "What are you doing?";
                        mainWindow.Show();
                        mainWindow.Activate();
                    }
                };
            }
        }

        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }

        public ICommand ExitApplicationCommand
        {
            get { return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()}; }
        }

        public void Execute()
        {
            Application.Current.Dispatcher.Invoke(delegate { ShowWindowCommand.Execute(null); });
        }
    }

    public class MainWindowFactory : IMainWindowFactory
    {
        public MainWindow Create()
        {
            return new MainWindow();
        }
    }
}