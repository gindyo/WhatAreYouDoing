using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using Hardcodet.Wpf.TaskbarNotification;
using Quartz;
using Quartz.Impl;
using Volante;
using WhatAreYouDoing;

namespace WhatAreYouDoing
{

   
    /// <summary>
    /// Simple application. Check the XAML for comments.
    /// </summary>
    public partial class App
    {
        private TaskbarIcon notifyIcon;
        private IScheduler _scheduler;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // Grab the Scheduler instance from the Factory 
                _scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                _scheduler.Start();
                // define the job and tie it to our HelloJob class

                IJobDetail job = JobBuilder.Create<NotifyIconViewModel>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(1800)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                _scheduler.ScheduleJob(job, trigger);
           
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
        }

        

        protected override void OnExit(ExitEventArgs e)
        {

            _scheduler.Shutdown();
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
       
    }
}
