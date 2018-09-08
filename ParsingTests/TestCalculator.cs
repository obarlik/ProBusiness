using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsing;

namespace ParsingTests
{
    [TestClass]
    public class TestCalculator
    {
        [TestMethod]
        public void TestCalculation()
        {
            var calc = new Calculator();

            var exp = "1";
            var r = calc.Calculate(exp);

            calc["x"] = 10;

            var tests = new[]
            {
                new object[] { "1", 1m },
                new object[] { "1+1", 2m },
                new object[] { "2*1+1", 3m },
                new object[] { "2*(1+1)", 4m },
                new object[] { "2*(1+1)+1", 5m },
                new object[] { "(2*(1+1)+1)*x", 50m },
                new object[] { "(2*(1+1)+1)*x^2^2", 50000m },
                new object[] { "(2*(1+1)+1)*x^2/2", 250m },
                new object[] { "x=10?10:0", 10m },
                new object[] { "x  !=   10   ?  10  :   0", 0m },
            };

            foreach (var t in tests)
            {
                Assert.AreEqual(r = calc.Calculate((string)t[0]), (decimal)t[1]);
            }
        }
    }
}
