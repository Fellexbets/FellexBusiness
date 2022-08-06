using System;
using Xunit;
using CalculatorLib;

namespace CalculatorUnitTest
{
    public class CalculatorUnitTest
    {
        [Fact]
        public void TestAdding2And2()
        {
            // arrange 
            double a = 2;
            double b = 2;
            double expected = 4;
            Calculator calc = new Calculator();

            // act
            double actual = calc.Add(a, b);

            // assert 
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAdding2And3()
        {
            // arrange 
            double a = 2;
            double b = 3;
            double expected = 5;
            Calculator calc = new Calculator();

            // act
            double actual = calc.Add(a, b);

            // assert 
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Subtract1And1()
        {
            // arrange
            double a = 1;
            double b = 1;
            double expected = 0;
            Calculator calc = new Calculator();

            // act
            double actual = calc.Subtract(a, b);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
