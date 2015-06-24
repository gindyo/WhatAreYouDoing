using System;
using System.Timers;
using System.Windows;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Hardcodet.Wpf.TaskbarNotification;
using Quartz;
using Quartz.Impl;

namespace WhatAreYouDoing
{
    public partial class App
    {
        private TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            Action a = () => {
                                 var notifVm = container.Resolve<NotifyIconViewModel>();
                                 notifVm.Execute();
            };

            new Scheduler().Repeat(a, 5000);
            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
        }


        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
        public class Scheduler
        {
            private Timer _timer;
            public Scheduler()
            {
                _timer = new Timer();
            }

            public void Repeat(Action act, int i)
            {
                _timer.AutoReset = true;
                _timer.Interval = i;
                ElapsedEventHandler eh = (sender, args) => act();
                _timer.Elapsed += eh;
                _timer.Start();
            }
        }
  
}