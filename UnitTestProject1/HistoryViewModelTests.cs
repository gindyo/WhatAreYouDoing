using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatAreYouDoing.Display.History;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Persistance.Models;
using WhatAreYouDoing.UIModels;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class HistoryViewModelTests : BaseViewModelTest
    {
        [TestMethod]
        public void TestGettingYesterdaysEntries()
        {
            IEntry todaysEntry = new UIEntry(new Entry {Time = DateTime.Now}).ToInterface();
            IEntry yesterdaysEntry = new UIEntry(new Entry {Time = DateTime.Now.AddDays(-1)}).ToInterface();

            var entryList = new List<IEntry> {todaysEntry, yesterdaysEntry};
            var vmContext = new Mock<IViewModelContext>();
            vmContext.Setup(c => c.GetAllEntries()).Returns(entryList.AsQueryable());
            vmContext.Setup(c => c.NewUIEntry(It.IsAny<IEntry>())).Returns(new UIEntry(yesterdaysEntry));

            var vm = new ViewModel {Context = vmContext.Object};
            vm.SelectedDate = yesterdaysEntry.Time.Date;

            var expectedEntry = new UIEntry(yesterdaysEntry);
            IUIEntry actualEntry = vm.Entries.Single();

            Assert.AreEqual(expectedEntry, actualEntry);
        }
    }
}