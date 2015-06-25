using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.History;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Startup;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class HistoryViewModelTests : BaseViewModelTest
    {
        [TestMethod]
        public void TestGettingYesterdaysEntries()
        {
            var todaysEntry = new UIEntry(new Entry{Time = DateTime.Now}).ToInterface();
            var yesterdaysEntry = new UIEntry(new Entry{Time = DateTime.Now.AddDays(-1)}).ToInterface();

            List<IEntry> entryList = new List<IEntry>{todaysEntry, yesterdaysEntry};

            RegisterFakeViewModelContext(entryList);
            _container.Register(
                Component.For<IHistoryViewModel>().ImplementedBy<WhatAreYouDoing.History.ViewModel>()
                    .DependsOn(Dependency.OnComponent(typeof (IViewModelContext), "FakeContext")).IsDefault());

            var vm = _container.Resolve<IHistoryViewModel>();

            vm.SelectedDate = yesterdaysEntry.Time.Date;

            var expectedEntry = new UIEntry(yesterdaysEntry);
            var actualEntry = vm.Entries.Single();

            Assert.IsTrue(vm.Entries.Any());
            Assert.AreEqual(expectedEntry, actualEntry);
        }

        private void RegisterFakeViewModelContext(List<IEntry> entryList)
        {
            var vmContext = new Mock<IViewModelContext>();
            vmContext.Setup(c => c.GetAllEntries()).Returns(entryList);
            _container.Register(Component.For<IViewModelContext>().Instance(vmContext.Object).Named("FakeContext").IsDefault());
        }
    }

  
}