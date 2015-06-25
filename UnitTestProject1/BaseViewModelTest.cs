using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Startup;

namespace WhatAreYouDoingTests
{
    public class BaseViewModelTest
    {
        protected Mock<IApplicationWrapper> _appWrapper;
        protected WindsorContainer _container;
        protected MockRepository _mockRepo;

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
            Mock<IDataSourceFactory> fakeDsFactoryMock = _mockRepo.Create<IDataSourceFactory>();
            Mock<IWAYDDatasource> fakeDatasource = _mockRepo.Create<IWAYDDatasource>();
            fakeDatasource.Setup(ds => ds.GetAll()).Returns(entries.AsQueryable());
            fakeDsFactoryMock.Setup(f => f.GetCurrent()).Returns(fakeDatasource.Object);
            _container.Register(Component.For<IDataSourceFactory>().Instance(fakeDsFactoryMock.Object));
        }
    }
}