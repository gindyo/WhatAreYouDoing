using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
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
        private IWindsorContainer _container;
        const string OpenWindowCommand = "openWidowCommand";
        const string PipeName = "pipeServerName";

        protected override void OnStartup(StartupEventArgs e)
        {
            ExitIfAlreadyRunning();
            StartServer();

            _container = new WindsorContainer().Install(FromAssembly.This());

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (Hardcodet.Wpf.TaskbarNotification.TaskbarIcon) FindResource("NotifyIcon");

            //make sure the notifyIcon's context is resolved from the container
            notifyIcon.DataContext = _container.Resolve<NotifyIconViewModel>();


            PopTheWindow();
            var scheduler = _container.Resolve<Scheduler>();
            const int min15 = 900000;
            scheduler.Repeat(PopTheWindow, min15);
            PopTheWindow();
        }

        private void PopTheWindow()
        {
            var notifVm = _container.Resolve<NotifyIconViewModel>();
            Dispatcher.Invoke(() => notifVm.ShowWindowCommand.Execute(null));
        }

        private void ExitIfAlreadyRunning()
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            string[] processes = Process.GetProcesses().Select(p=>p.ProcessName).ToArray();
            if (processes.Count(p => p.Contains("WhatAreYouDoing")) < 2)
                return;

            var client = new NamedPipeClientStream(PipeName);
            client.Connect();
            StreamWriter writer = new StreamWriter(client);
            writer.WriteLine(OpenWindowCommand);
            writer.Flush();
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
        void StartServer()
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream(PipeName);
                StreamReader reader = new StreamReader(server);
                while (true)
                {
                    server.WaitForConnection();
                    var line = reader.ReadLine();
                    if (null != line && line == OpenWindowCommand)
                    {
                        Dispatcher.Invoke(PopTheWindow);
                    }
                    server.Disconnect();
                }
            });
        }
    }
}