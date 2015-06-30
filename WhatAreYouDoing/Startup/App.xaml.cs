using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Castle.Windsor;
using Castle.Windsor.Installer;
using WhatAreYouDoing.Display.Settings;
using WhatAreYouDoing.TaskbarIcon;
using WhatAreYouDoing.Utilities;

 public partial class App
    {
        private const string OpenWindowCommand = "openWidowCommand";
        private const string PipeName = "pipeServerName";
        private IWindsorContainer _container;
        private Hardcodet.Wpf.TaskbarNotification.TaskbarHardcodet.Wpf.TaskbarNotification.TaskbarIcone void OnStartup(StartupEventArgs e)
        {
            ExitIfAlreadyRunning();
           // StartServer();

            _container = new WindsorContainer().Install(FromAssembly.This());

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (Hardcodet.Wpf.TaskbarNotification.TaskbarIHardcodet.Wpf.TaskbarNotification.TaskbarIcon   //make sure the notifyIcon's context is resolved from the container
            notifyIcon.DataContext = _container.Resolve<NotifyIconViewModel>();


            PopTheWindow();
            var scheduler = _container.Resolve<Scheduler>();
            var interval = _container.Resolve<Display.Settings.Context>().GetInterval().Value;
            scheduler.Repeat(PopTheWindow, Convert.ToDouble( interval));
            PopTheWindow();
        }

        private void PopTheWindow()
        {
            var notifVm = _container.Resolve<NotifyIconViewModel>();
            Dispatcher.Invoke(() => notifVm.ShowWindowCommand.Execute(null));
        }

        private void ExitIfAlreadyRunning()
        {
            string currentProcessName = Process.GetCurrentProcess().ProcessName;
            string[] processes = Process.GetProcesses().Select(p => p.ProcessName).ToArray();
            if (processes.Count(p=>p == currentProcessName ) < 2)
                return;

            //StartClient();
            MessageBox.Show("Already running. Exiting..");
            Shutdown();
        }

        private static void StartClient()
        {
            var client = new NamedPipeClientStream(PipeName);
            client.Connect();
            var writer = new StreamWriter(client);
            writer.WriteLine(OpenWindowCommand);
            writer.Flush();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }

        private void StartServer()
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream(PipeName);
                var reader = new StreamReader(server);
                while (true)
                {
                    server.WaitForConnection();
                    string line = reader.ReadLine();
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