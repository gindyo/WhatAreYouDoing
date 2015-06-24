using System;
using System.Windows;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WhatAreYouDoing.Contexts;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing
{
    public class WindsorInstaller : IWindsorInstaller
    {
        private const string Mainwindowcontext = "MainWindowContext";
        private const bool InMemory = false;
        private const string Datasourcefactory = "DatasourceFactory";
        private const string Mainwindowfactory = "MainWindowFactory";
        private const string Mainwindowviewmodel = "MainWindowViewModel";
        private const string Mainwindow = "MainWindow";
        private const string Scheduler = "Scheduler";
        private const string Notifyiconviewmodel = "NotifyIconViewModel";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IEntryFactory>()
                    .ImplementedBy<EntryFactory>()
                );

            container.Register(
                Component.For<IApplicationWrapper>()
                    .ImplementedBy<ApplicationWrapper>()
                    .Named("application")
                );

            container.Register(
                Component.For<MainWindowViewModel>()
                    .ImplementedBy<MainWindowViewModel>()
                    .DependsOn(Dependency.OnComponent(typeof (IMainWindowViewModelContext), Mainwindowcontext))
                    .LifestyleTransient()
                    .Named(Mainwindowviewmodel)
                );

            container.Register(
                Component.For<IDataSourceFactory>()
                    .ImplementedBy<DatasourceFactory>()
                    .DependsOn(Dependency.OnValue<bool>(InMemory))
                    .Named(Datasourcefactory)
                );

            container.Register(
                Component.For<IMainWindowViewModelContext>()
                    .ImplementedBy<MainWindowViewModelContext>()
                    .LifestyleTransient()
                    .Named(Mainwindowcontext)
                );

            container.Register(
                Component.For<Scheduler>()
                    .Named(Scheduler)
                );

            container.Register(
                Component.For<Func<MainWindow>>()
                    .AsFactory()
                    .Named(Mainwindowfactory)
                );


            container.Register(
                Component.For<MainWindow>()
                    .LifestyleTransient()
                    .Named(Mainwindow)
                );

            container.Register(
                Component.For<NotifyIconViewModel>()
                    .DependsOn(Dependency.OnComponent(typeof (Application), "application"))
                    .Named(Notifyiconviewmodel)
                );
        }
    }
}