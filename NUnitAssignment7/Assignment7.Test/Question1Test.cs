using System;
using System.Collections.Generic;
using System.Text;
using Assignment7.BL;
using NUnit.Framework;

namespace Assignment7.Test
{
    [TestFixture]
    public class Question1Test
    {
        /// <summary>
        /// Test for "if-else"
        /// </summary>
        /// <param name="age"></param>
        /// <param name="expectedAgeGroup"></param>
        [TestCase(12, "Child")]
        [TestCase(45, "Adult")]
        [TestCase(66, "Senior Citizen")]
        [TestCase(-5, "Invalid age!!!")]
        public void TestFindAgeGroup(int age, string expectedAgeGroup)
        {
            Question1BL question1BL = new Question1BL();

            // Act
            string result = question1BL.FindAgeGroup(age);

            // Assert
            Assert.That(result == expectedAgeGroup);
        }

        /// <summary>
        /// Test for "switch-case"
        /// </summary>
        /// <param name="day"></param>
        /// <param name="expectedDay"></param>
        [TestCase(1, "Sunday")]
        [TestCase(2, "Monday")]
        [TestCase(3, "Tuesday")]
        [TestCase(4, "Wednesday")]
        [TestCase(5, "Thursday")]
        [TestCase(6, "Friday")]
        [TestCase(7, "Saturday")]
        [TestCase(0, "Invalid day!!!")]
        [TestCase(55, "Invalid day!!!")]
        [TestCase(-10, "Invalid day!!!")]
        public void TestFindDayofWeek(int day, string expectedDay)
        {
            Question1BL question1BL = new Question1BL();

            // Act
            string result = question1BL.FindDayofWeek(day);

            // Assert
            Assert.AreEqual(result, expectedDay);
        }

        /// <summary>
        /// Test for "for-loop"
        /// </summary>
        /// <param name="number"></param>
        /// <param name="factorial"></param>
        [TestCase(5, 120)]
        [TestCase(3, 6)]
        [TestCase(0, 1)]
        [TestCase(-5, 0)]
        public void TestFindFactorial(int number, long factorial)
        {
            Question1BL question1BL = new Question1BL();

            // Act
            long result = question1BL.FindFactorial(number);

            // Assert
            Assert.AreEqual(result, factorial);
        }

        /// <summary>
        /// Test for "foreach-loop"
        /// </summary>
        /// <param name="numberList"></param>
        /// <param name="sum"></param>
        [TestCase(new int[] { 1, 2, 3 }, 2)]
        [TestCase(new int[] { 11,22,44,20,54,55,33,101,0}, 140)]
        [TestCase(new int[] { 12, -22, 30,55,77,20,-10 }, 30)]
        [TestCase(new int[] { }, 0)]
        public void TestFindSumofEven(int[] numberList, long sum)
        {
            Question1BL question1BL = new Question1BL();

            // Act
            long result = question1BL.FindSumofEven(numberList);

            // Assert
            Assert.AreEqual(result, sum);
        }

        /// <summary>
        /// Test for "while-loop"
        /// </summary>
        /// <param name="number"></param>
        /// <param name="answer"></param>
        [TestCase(121,true)]
        [TestCase(507,false)]
        public void TestCheckPalindrome(int number,bool answer)
        {
            Question1BL question1BL = new Question1BL();

            // Act
            bool result = question1BL.CheckPalindrome(number);

            // Assert
            Assert.That(result==answer);
        }
    }
}
