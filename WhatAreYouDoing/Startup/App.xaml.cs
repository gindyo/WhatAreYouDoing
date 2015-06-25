using System;
using System.Windows;
using Castle.Windsor;
using Castle.Windsor.Installer;
using WhatAreYouDoing.TaskbarIcon;
using WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.Startup
{
    public partial class App
    {
        private Hardcodet.Wpf.TaskbarNotification.TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            IWindsorContainer container = new WindsorContainer().Install(FromAssembly.This());

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (Hardcodet.Wpf.TaskbarNotification.TaskbarIcon) FindResource("NotifyIcon");

            //make sure the notifyIcon's context is resolved from the container
            notifyIcon.DataContext = container.Resolve<NotifyIconViewModel>();

            Action popTheWindow = () =>
            {
                var notifVm = container.Resolve<NotifyIconViewModel>();
                Dispatcher.Invoke(() => notifVm.ShowWindowCommand.Execute(null));
            };
            var scheduler = container.Resolve<Scheduler>();
            const int min15 = 900000;
            scheduler.Repeat(popTheWindow, min15);
            popTheWindow();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
}