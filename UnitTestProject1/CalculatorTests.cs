using System;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void ShouldReturnCorrectAverage()
        {
            int[] values = { 1, 2, 5, 6, 7 };
            int[] values2 = { 1, 8, 4, 6, 1 };
            int[] values3 = { 1, 2, -8, 6 };
            int[] values4 = { -1, -2, -5, -6, -7 };

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(4.2, calc.calcAverageValue(values));
                Assert.AreEqual(4, calc.calcAverageValue(values2));
                Assert.AreEqual(0.25, calc.calcAverageValue(values3));
                Assert.AreEqual(-4.2, calc.calcAverageValue(values4));
            });
        }

        [Test]
        public void ShouldReturnCorrectMedian()
        {
            int[] values = { 1, 2, 5, 6, 7 };
            int[] values2 = { 1, 8, 4, 6, 1 };
            int[] values3 = { 1, 2, -8, 6 };
            int[] values4 = { -1, -2, -5, -6, -7 };

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();            
            Assert.AreEqual(5, calc.calcMedianValue(values));
            Assert.AreEqual(4, calc.calcMedianValue(values2));
            Assert.AreEqual(1.5, calc.calcMedianValue(values3));
            Assert.AreEqual(-5, calc.calcMedianValue(values4));
        }

        [Test]
        public void ShouldReturnCorrectHighestValue()
        {
            int[] values = { 1, 2, 5, 6, 7 };
            int[] values2 = { 1, 8, 4, 6, 1 };
            int[] values3 = { 1, 2, -8, 6 };
            int[] values4 = { -1, -2, -5, -6, -7 };

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.AreEqual(7, calc.calcHighestValue(values));
            Assert.AreEqual(8, calc.calcHighestValue(values2));
            Assert.AreEqual(6, calc.calcHighestValue(values3));
            Assert.AreEqual(-1, calc.calcHighestValue(values4));
        }

        [Test]
        public void ShouldReturnCorrectLowestValue()
        {
            int[] values = { 1, 2, 5, 6, 7 };
            int[] values2 = { 1, 8, 4, 6, 1 };
            int[] values3 = { 1, 2, -8, 6 };
            int[] values4 = { -1, -2, -5, -6, -7 };

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.AreEqual(1, calc.calcLowestValue(values));
            Assert.AreEqual(1, calc.calcLowestValue(values2));
            Assert.AreEqual(-8, calc.calcLowestValue(values3));
            Assert.AreEqual(-7, calc.calcLowestValue(values4));
        }
    }
}
