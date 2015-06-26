using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing.Display.Main;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Persistance.Models;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class MainWindowContextTests
    {
        private Mock<IDataSourceFactory> _datasourceFactory;
        private MockRepository _mockRepo;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new MockRepository(MockBehavior.Default);
            _datasourceFactory = _mockRepo.Create<IDataSourceFactory>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockRepo = null;
            _datasourceFactory = null;
        }

        [TestMethod]
        public void TestGetAllEntriesFromContext()
        {
            Mock<IWAYDDatasource> datasource = _mockRepo.Create<IWAYDDatasource>();
            _datasourceFactory.Setup(ds => ds.GetCurrent()).Returns(datasource.Object);
            var data = new List<IEntry> {new Entry()};
            datasource.Setup(ds => ds.GetAllEntries()).Returns(data.AsQueryable());
            var context = new EntriesViewModelContext(_datasourceFactory.Object, null);
            context.GetAllEntries();
            _mockRepo.VerifyAll();
        }

        [TestMethod]
        public void TestEntriesHaveDuration()
        {
            
        }

        [TestMethod]
        public void TestGetCurrentFromContext()
        {
            Mock<IWAYDDatasource> datasource = _mockRepo.Create<IWAYDDatasource>();
            _datasourceFactory.Setup(ds => ds.GetCurrent()).Returns(datasource.Object);
            datasource.Setup(ds => ds.GetEntry());
            var context = new EntriesViewModelContext(_datasourceFactory.Object,null);
            context.GetCurrentEntry();
            _mockRepo.VerifyAll();
        }
    }
}