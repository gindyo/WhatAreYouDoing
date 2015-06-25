using System.Configuration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Startup;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class ComponentsInitializationTests
    {
        private static WindsorContainer container;

        [TestInitialize]
        public void Init()
        {
            container = new WindsorContainer();
            container.Install(new WindsorInstaller());
        }

        [TestMethod]
        public void TestViewModelContextInitialization()
        {
            var context = container.Resolve<IViewModelContext>();
            Assert.IsNotNull(context);
        }


        [TestMethod]
        public void TestMainWindowInitialization()
        {
            var mainWindow = container.Resolve<WhatAreYouDoing.Display.Main.MainWindow>();
            Assert.IsNotNull(mainWindow.ViewModel);
        }

        [TestMethod]
        public void TestViewModelInitialization()
        {
            var mainWindowVm = container.Resolve<WhatAreYouDoing.Display.Main.ViewModel>();
            Assert.IsNotNull(mainWindowVm.ViewModelContext);
        }

        [TestMethod]
        public void HistoryViewModelIsResolvedSuccessfuly()
        {
            var vm = container.Resolve<WhatAreYouDoing.Display.History.ViewModel>();
            Assert.IsNotNull(vm.Context);
        }
        [TestMethod]
        public void SettingsViewModelIsResolvedSuccessfuly()
        {
            var vm = container.Resolve<WhatAreYouDoing.Display.Settings.ViewModel>();
            Assert.IsNotNull(vm.Context);
        }
    }
}