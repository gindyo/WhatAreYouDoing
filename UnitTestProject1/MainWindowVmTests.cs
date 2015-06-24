using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatAreYouDoing;
using WhatAreYouDoing.Contexts;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class MainWindowVmTests
    {
        [TestMethod]
        public void TestVmInitialization()
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());
            var mainWindowVm = container.Resolve<MainWindowViewModel>();
            Assert.IsNotNull(mainWindowVm.Context);

        }
    }
}
