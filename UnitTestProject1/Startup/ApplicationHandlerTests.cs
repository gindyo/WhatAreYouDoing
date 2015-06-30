using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WhatAreYouDoing.Display.Main;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Startup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.Startup.Tests
{
    [TestClass()]
    public class ApplicationHandlerTests
    {
        private Mock<IApplicationWrapper> _applicationMock;

        [TestInitialize]
        public void Init()
        {
            _applicationMock = new Mock<IApplicationWrapper>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _applicationMock = null;
        }

        public ApplicationHandler Cut
        {
            get { return new ApplicationHandler(_applicationMock.Object); }
        }

        [TestMethod()]
        public void ShutdownTest()
        {
            _applicationMock.Setup(ah => ah.Shutdown());
            Cut.Shutdown();
        }

        [TestMethod()]
        public void WindowIsOpenTest()
        {
            _applicationMock.SetupGet(a => a.MainWindow).Returns(new MainWindow());
            _applicationMock.SetupGet(a => a.IsMainWindowLoaded).Returns(false);
            var result = Cut.WindowIsOpen();
            Assert.AreEqual(false, result);

            _applicationMock.SetupGet(a => a.IsMainWindowLoaded).Returns(true);
            result = Cut.WindowIsOpen();
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void CloseCurrentWindowTest()
        {
            _applicationMock.Setup(a => a.Shutdown());
            Cut.Shutdown();
            _applicationMock.VerifyAll();
            
        }

        [TestMethod()]
        public void PopWindowTest()
        {
            var mainWindow = new MainWindow();
            _applicationMock.Setup(a => a.IsMainWindowLoaded).Returns(false);
            _applicationMock.Setup(a => a.MainWindow).Returns(mainWindow);
            _applicationMock.Setup(a => a.ShowMainWindow());
            _applicationMock.Setup(a => a.ActivateMainWindow());
            _applicationMock.Setup(a => a.PlaySound());
            Cut.PopWindow(mainWindow);
            _applicationMock.VerifyAll();
            Assert.AreEqual("What are you doing?", mainWindow.Title);
            Assert.IsTrue(mainWindow.TextBox.IsFocused);

        }
    }
}
