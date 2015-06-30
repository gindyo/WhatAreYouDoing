using System;
using System.Diagnostics;
using System.Windows.Input;
using WhatAreYouDoing.Display.Main;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Utilities;

g WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.TaskbarIcon
{
    public class NotifyIconViewModel
    {
        public IApplicationHandler CurrentApp { get; set; }
        public Func<MainWindow> MainWindowFactory { get; set; }


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