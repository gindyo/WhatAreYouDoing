using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatAreYouDoing.History;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Startup;
using ViewModel = WhatAreYouDoing.Main.ViewModel;

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
            var mainWindow = container.Resolve<MainWindow>();
            Assert.IsNotNull(mainWindow.ViewModel);
        }

        [TestMethod]
        public void TestViewModelInitialization()
        {
            var mainWindowVm = container.Resolve<ViewModel>();
            Assert.IsNotNull(mainWindowVm.ViewModelContext);
        }

        [TestMethod]
        public void HistoryViewModelIsResolvedSuccessfuly()
        {
            var vm = container.Resolve<IHistoryViewModel>();
            Assert.IsNotNull(vm.Context);
        }
    }
}