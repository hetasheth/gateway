using Assignment7.BL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Test
{
    [TestFixture]
    public class Question2Test
    {
        /// <summary>
        /// Business logic for divide by zero exception
        /// </summary>
        [Test]
        public void TestDivisionException()
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = Assert.ThrowsAsync<DivideByZeroException>(async() => await question2BL.Division(10, 0)).Message;

            // Assert
            Assert.AreEqual("Attempted to divide by zero.", result);
        }

        /// <summary>
        /// Test for asyn method for division
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(10,2,5)]
        public async Task TestDivision(int num1, int num2, double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            double result = await question2BL.Division(num1, num2);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Test for index out of range exception
        /// </summary>
        [Test]
        public void TestGetValuebyIndexException()
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = Assert.Throws<IndexOutOfRangeException>(() => question2BL.GetValuebyIndex(new int[] { 1,2,3}, 4)).Message;

            // Assert
            Assert.AreEqual("Index was outside the bounds of the array.", result);
        }

        /// <summary>
        /// Test for getting value at particular index in array
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="index"></param>
        /// <param name="expectedResult"></param>
        [TestCase(new int[] { 1, 2, 3 }, 2,3)]
        public void TestGetValuebyIndex(int[] intArray, int index, int expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            int result = question2BL.GetValuebyIndex(intArray, index);

            // Assert
            Assert.That(result== expectedResult);
        }


        /// <summary>
        /// Test for true value of area of circle
        /// </summary>
        /// <param name="r"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(10,314)]
        [TestCase(0,0)]
        public async Task TestAreaofCircleTrue(int r,double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = await question2BL.AreaofCircle(r);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Test for false value of area of circle
        /// </summary>
        /// <param name="r"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(11, 314)]
        public async Task TestAreaofCircleFalse(int r, double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = await question2BL.AreaofCircle(r);

            // Assert
            Assert.AreNotEqual(result, expectedResult);
        }

        /// <summary>
        /// Test for true value of area of square
        /// </summary>
        /// <param name="l"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(10, 100)]
        public async Task TestAreaofSquareTrue(int l, double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = await question2BL.AreaofSquare(l);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Test for false value of area of square
        /// </summary>
        /// <param name="l"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(11, 120)]
        public async Task TestAreaofSquareFalse(int l, double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = await question2BL.AreaofSquare(l);

            // Assert
            Assert.AreNotEqual(result, expectedResult);
        }

        /// <summary>
        /// Test for true value of perimeter of square
        /// </summary>
        /// <param name="l"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(10, 40)]
        public async Task TestPerimeterofSquareTrue(int l, double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = await question2BL.PerimeterofSquare(l);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Test for false value of perimeter of square
        /// </summary>
        /// <param name="l"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [TestCase(11, 50)]
        public async Task TestPerimeterofSquareFalse(int l, double expectedResult)
        {
            Question2BL question2BL = new Question2BL();

            // Act
            var result = await question2BL.PerimeterofSquare(l);

            // Assert
            Assert.AreNotEqual(result, expectedResult);
        }
    }
}
