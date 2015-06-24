using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WhatAreYouDoing
{
    public class NotifyIconViewModel
    {
        #region Windsor injected

        public IApplicationWrapper CurrentApp { get; set; }
        public Func<MainWindow> MainWindowFactory { get; set; }

        #endregion

        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => !CurrentApp.WindowIsOpen(),
                    CommandAction = () =>
                    {
                        Debug.Assert(MainWindowFactory != null, "MainWindowFactory != null");
                        CurrentApp.PopWindow(MainWindowFactory());
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
                    CommandAction = () => CurrentApp.CloseCurrentWindow(),
                    CanExecuteFunc = () => CurrentApp.WindowIsOpen()
                };
            }
        }

        public ICommand ExitApplicationCommand
        {
            get { return new DelegateCommand {CommandAction = () => CurrentApp.Shutdown()}; }
        }
    }
}