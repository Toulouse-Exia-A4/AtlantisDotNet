using System;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 2);
        }
    }
}
