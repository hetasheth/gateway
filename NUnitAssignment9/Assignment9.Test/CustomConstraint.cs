using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment9.Test
{
    public class CustomConstraint : Constraint
    {
        public readonly string _expectedString;

        public CustomConstraint(string expectedString)
        {
            _expectedString = expectedString.ToUpper();
            Description = $"Expected string is {expectedString}";
        }
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            if (typeof(TActual) != typeof(string))
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            if (_expectedString == actual.ToString())
                return new ConstraintResult(this, actual, ConstraintStatus.Success);
            else
                return new ConstraintResult(this, actual, ConstraintStatus.Failure);
        }
    }

    public class Is : NUnit.Framework.Is
    {
        public static CustomConstraint ConvertToUppercase(string expectedString)
        {
            return new CustomConstraint(expectedString);
        }

    }
}
