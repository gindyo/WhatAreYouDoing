using System;
using System.Windows;
using System.Windows.Navigation;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using WhatAreYouDoing.Contexts;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IEntryFactory>()
                    .ImplementedBy<EntryFactory>()
                );

            const string mainwindowcontext = "MainWindowContext";
            container.Register(
                Component.For<MainWindowViewModel>()
                    .ImplementedBy<MainWindowViewModel>()
                    .DependsOn(Dependency.OnComponent(typeof (IMainWindowContext),mainwindowcontext))
                    .LifestyleTransient());

            const bool inMemory = false;
            container.Register(
                Component.For<IDataSourceFactory>()
                    .ImplementedBy<DatasourceFactory>()
                    .DependsOn(Dependency.OnValue<bool>(inMemory))
                );
            container.Register(
                Component.For<IMainWindowContext>()
                    .ImplementedBy<MainWindowVMContext>().Named(mainwindowcontext)
                    .LifestyleTransient()
                );
            container.Register(
                Component.For<Func<MainWindow>>()
                .AsFactory()
                );

            container.Register(
                Component.For<MainWindow>()
                .LifestyleTransient()
                );

            container.Register(
                Component.For<NotifyIconViewModel>()
                    .DependsOn(Dependency.OnComponent<IMainWindowFactory,MainWindowFactory>())
                );
        }
     
    }

    public interface IMainWindowFactory
    {
        MainWindow Create();
    }
}