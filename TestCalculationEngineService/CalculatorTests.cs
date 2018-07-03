using System;
using System.Collections.Generic;
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
        List<T> CreateList<T>(params T[] values)
        {
            return new List<T>(values);
        }

        [Test]
        public void ShouldReturnCorrectAverage()
        {
            List<int> values = CreateList(1, 2, 5, 6, 7);
            List<int> values2 = CreateList(1, 8, 4, 6, 1);
            List<int> values3 = CreateList(1, 2, -8, 6);
            List<int> values4 = CreateList(-1, -2, -5, -6, -7);

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
            List<int> values = CreateList(1, 2, 5, 6, 7);
            List<int> values2 = CreateList(1, 8, 4, 6, 1);
            List<int> values3 = CreateList(1, 2, -8, 6);
            List<int> values4 = CreateList(-1, -2, -5, -6, -7);

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();            
            Assert.AreEqual(5, calc.calcMedianValue(values));
            Assert.AreEqual(4, calc.calcMedianValue(values2));
            Assert.AreEqual(1.5, calc.calcMedianValue(values3));
            Assert.AreEqual(-5, calc.calcMedianValue(values4));
        }

        [Test]
        public void ShouldReturnCorrectHighestValue()
        {
            List<int> values = CreateList(1, 2, 5, 6, 7);
            List<int> values2 = CreateList(1, 8, 4, 6, 1);
            List<int> values3 = CreateList(1, 2, -8, 6);
            List<int> values4 = CreateList(-1, -2, -5, -6, -7);

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.AreEqual(7, calc.calcHighestValue(values));
            Assert.AreEqual(8, calc.calcHighestValue(values2));
            Assert.AreEqual(6, calc.calcHighestValue(values3));
            Assert.AreEqual(-1, calc.calcHighestValue(values4));
        }

        [Test]
        public void ShouldReturnCorrectLowestValue()
        {
            List<int> values = CreateList(1, 2, 5, 6, 7);
            List<int> values2 = CreateList(1, 8, 4, 6, 1);
            List<int> values3 = CreateList(1, 2, -8, 6);
            List<int> values4 = CreateList(-1, -2, -5, -6, -7);

            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.AreEqual(1, calc.calcLowestValue(values));
            Assert.AreEqual(1, calc.calcLowestValue(values2));
            Assert.AreEqual(-8, calc.calcLowestValue(values3));
            Assert.AreEqual(-7, calc.calcLowestValue(values4));
        }
    }
}
