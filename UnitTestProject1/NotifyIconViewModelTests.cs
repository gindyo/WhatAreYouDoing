using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class NotifyIconViewModelTests : BaseViewModelTest
    {
        [TestMethod]
        public void TestOpenWindow()
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());
            Mock<IApplicationWrapper> appWrapper = _mockRepo.Create<IApplicationWrapper>();
            appWrapper.Setup(w => w.PopWindow(It.IsAny<MainWindow>()));
            container.Register(Component.For<IApplicationWrapper>()
                .Instance(appWrapper.Object)
                .Named("FakeWrapper").IsDefault());
            var notifyWindowVm = container.Resolve<NotifyIconViewModel>();
            if (notifyWindowVm.ShowWindowCommand.CanExecute(null))
                notifyWindowVm.ShowWindowCommand.Execute(null);
            _mockRepo.VerifyAll();
        }

        [TestMethod]
        public void TestOpenOpenedWindow()
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());
            Mock<IApplicationWrapper> appWrapper = _mockRepo.Create<IApplicationWrapper>();
            appWrapper.Verify(w => w.PopWindow(It.IsAny<MainWindow>()), Times.Never);
            appWrapper.Setup(w => w.WindowIsOpen()).Returns(true);
            container.Register(Component.For<IApplicationWrapper>()
                .Instance(appWrapper.Object)
                .Named("FakeWrapper").IsDefault());
            var notifyWindowVm = container.Resolve<NotifyIconViewModel>();
            if (notifyWindowVm.ShowWindowCommand.CanExecute(null))
                notifyWindowVm.ShowWindowCommand.Execute(null);
            _mockRepo.VerifyAll();
        }
    }
}