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
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void TestCalculatorCalcAverageValue()
        {
            int[] values = { 1, 2, 5, 6, 7 };
            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.AreEqual(calc.calcAverageValue(values), 4.2);
        }

        [Test]
        public void TestCalculatorCalcMedianValue()
        {
            int[] values = { 1, 2, 5, 6, 7 };
            CalcEngineService.Calculator calc = new CalcEngineService.Calculator();
            Assert.AreEqual(calc.calcMedianValue(values), 5);
        }
    }
}
