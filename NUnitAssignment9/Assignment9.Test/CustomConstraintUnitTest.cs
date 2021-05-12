using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment9.Test
{
    public class CustomConstraintUnitTest
    {
        [Test]
        public void ConvertToUppercaseTestPositive()
        {
            // Act
            string name = "heta";

            // Assert
            Assert.That("HETA", Is.ConvertToUppercase(name));
        }

        [Test]
        public void ConvertToUppercaseTestNegative()
        {
            // Act
            string name = "heta";

            // Assert
            Assert.AreNotEqual("Heta", Is.ConvertToUppercase(name));
        }
    }
}
