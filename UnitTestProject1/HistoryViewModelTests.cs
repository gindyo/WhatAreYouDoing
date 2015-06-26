using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
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
        public void TestSettingDate()
        {
            var vmContext = VMContext;
            vmContext.Setup(c => c.GetEntriesForDate(DateTime.Now.Date.AddDays(-1))).Returns(new List<UIEntry>());
            var vm = new ViewModel {Context = vmContext.Object};
            vm.SelectedDate = DateTime.Now.Date.AddDays(-1);
            vmContext.VerifyAll();
        }

        [TestMethod]
        public void TestSettingContext()
        {
            var vmContext = VMContext;
            vmContext.Setup(c => c.GetEntriesForDate(DateTime.Now.Date.AddDays(-1))).Returns(new List<UIEntry>());
            var vm = new ViewModel {Context = vmContext.Object};
            Assert.AreEqual(DateTime.Now.Date, vm.SelectedDate.Date);
            
        }

        private static Mock<IViewModelContext> VMContext
        {
            get { return new Mock<IViewModelContext>(); }
        }
    }
}