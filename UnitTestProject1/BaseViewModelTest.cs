using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Startup;

namespace WhatAreYouDoingTests
{
    public class BaseViewModelTest
    {
        protected MockRepository _mockRepo;
        protected WindsorContainer _container;
        protected Mock<IApplicationWrapper> _appWrapper;

        [TestInitialize]
        public void TestInit()
        {
            _mockRepo = new MockRepository(MockBehavior.Default);
            _container = new WindsorContainer();
            _container.Install(new WindsorInstaller());
            _appWrapper = _mockRepo.Create<IApplicationWrapper>();
        }

        protected void SetupFakeDatasource(List<IEntry> entries)
        {
            var fakeDsFactoryMock = _mockRepo.Create<IDataSourceFactory>();
            var fakeDatasource = _mockRepo.Create<IWAYDDatasource>();
            fakeDatasource.Setup(ds => ds.GetAll()).Returns(entries.AsQueryable());
            fakeDsFactoryMock.Setup(f => f.GetCurrent()).Returns(fakeDatasource.Object);
            _container.Register(Component.For<IDataSourceFactory>().Instance(fakeDsFactoryMock.Object));
        }

    }
}