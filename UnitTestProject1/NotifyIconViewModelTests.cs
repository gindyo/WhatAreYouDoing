using Castle.MicroKernel.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Startup;
using WhatAreYouDoing.TaskbarIcon;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class NotifyIconViewModelTests : BaseViewModelTest
    {
        [TestMethod]
        public void TestOpenWindow()
        {
            _appWrapper.Setup(w => w.PopWindow(It.IsAny<MainWindow>()));
            _container.Register(Component.For<IApplicationWrapper>()
                .Instance(_appWrapper.Object)
                .Named("FakeWrapper")
                .IsDefault());
            var notifyWindowVm = _container.Resolve<NotifyIconViewModel>();
            if (notifyWindowVm.ShowWindowCommand.CanExecute(null))
                notifyWindowVm.ShowWindowCommand.Execute(null);
            _mockRepo.VerifyAll();
        }

        [TestMethod]
        public void TestOpenOpenedWindow()
        {
            _appWrapper.Verify(w => w.PopWindow(It.IsAny<MainWindow>()), Times.Never);
            _appWrapper.Setup(w => w.WindowIsOpen()).Returns(true);
            _container.Register(Component.For<IApplicationWrapper>()
                .Instance(_appWrapper.Object)
                .Named("FakeWrapper")
                .IsDefault());
            var notifyWindowVm = _container.Resolve<NotifyIconViewModel>();
            if (notifyWindowVm.ShowWindowCommand.CanExecute(null))
                notifyWindowVm.ShowWindowCommand.Execute(null);
            _mockRepo.VerifyAll();
        }
    }
}