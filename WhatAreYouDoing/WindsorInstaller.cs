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
        const string Mainwindowcontext = "MainWindowContext";
        const bool InMemory = false;
        const string Datasourcefactory = "DatasourceFactory";
        const string Mainwindowfactory = "MainWindowFactory";
        const string Mainwindowviewmodel = "MainWindowViewModel";
        const string Mainwindow = "MainWindow";
        const string Scheduler = "Scheduler";
        const string Notifyiconviewmodel = "NotifyIconViewModel";

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
                    .DependsOn(Dependency.OnComponent(typeof (IMainWindowContext), Mainwindowcontext))
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
                Component.For<IMainWindowContext>()
                    .ImplementedBy<MainWindowVMContext>()
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
                    .DependsOn(Dependency.OnComponent(typeof(Application), "application"))
                    .Named(Notifyiconviewmodel)
            );
        }
    }
}