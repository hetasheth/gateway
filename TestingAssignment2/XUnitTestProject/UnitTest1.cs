using System;
using Xunit;
using TestingAssignment2;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ConvertCase()
        {
            string Name = "hETA";
            string result = Name.ConvertCase();
        }

        [Fact]
        public void Test_ConvertToTitleCase()
        {
            string Name = "heta Sheth";
            string result = Name.ConvertToTitleCase();
        }

        [Fact]
        public void Test_CheckLowerCase()
        {
            string Name = "heta Sheth";
            bool result = Name.CheckLowerCase();
        }

        [Fact]
        public void Test_ConvertToCapitalCase()
        {
            string Name = "heta Sheth";
            string result = Name.ConvertToCapitalCase();
        }

        [Fact]
        public void Test_CheckUpperCase()
        {
            string Name = "heta Sheth";
            bool result = Name.CheckUpperCase();
        }

        [Fact]
        public void Test_IsValidNumeric()
        {
            string Name = "heta Sheth";
            bool result = Name.IsValidNumeric();
        }

        [Fact]
        public void Test_RemoveLastCharacter()
        {
            string Name = "heta Sheth";
            string result = Name.RemoveLastCharacter();
        }

        [Fact]
        public void Test_WordCount()
        {
            string Name = "heta Sheth";
            int result = Name.WordCount();
        }

        [Fact]
        public void Test_ConvertToInt()
        {
            string Name = "heta Sheth";
            int result = Name.ConvertToInt();
        }
    }
}
