using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WhatAreYouDoingTests
{
    public class BaseViewModelTest
    {
        protected MockRepository _mockRepo;

        [TestInitialize]
        public void TestInit()
        {
            _mockRepo = new MockRepository(MockBehavior.Default);
        }
    }
}