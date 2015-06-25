using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatAreYouDoing;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Startup;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class MainWindowViewModelTests : BaseViewModelTest
    {
        [TestMethod]
        public void TestViewModelInitialization()
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());
            var mainWindowVm = container.Resolve<ViewModel>();
            Assert.IsNotNull(mainWindowVm.ViewModelContext);
        }
    }
}