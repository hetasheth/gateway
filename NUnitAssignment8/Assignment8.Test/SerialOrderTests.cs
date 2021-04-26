using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assignment8.BL;
using Assignment8.Test.SequentialTestingHelpers;

namespace Assignment8.Test
{
    [TestFixture]
    public class SerialOrderTests
    {
        private static readonly IntegerNumber _integerNumber = new IntegerNumber();

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange
            _integerNumber.Number = 0;
        }

        [TestCaseSource(sourceName: "TestSource")]
        public void MyTest(TestStructure test)
        {
            test.Test();
        }

        public static IEnumerable<TestCaseData> TestSource
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                Dictionary<int, List<MethodInfo>> methods = assembly
                    .GetTypes()
                    .SelectMany(x => x.GetMethods())
                    .Where(y => y.GetCustomAttributes().OfType<TestSequenceAttribute>().Any())
                    .GroupBy(z => z.GetCustomAttribute<TestSequenceAttribute>().Sequence)
                    .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList());

                foreach (var order in methods.Keys.OrderBy(x => x))
                {
                    foreach (var methodInfo in methods[order])
                    {
                        MethodInfo info = methodInfo;
                        yield return new TestCaseData(
                            new TestStructure
                            {
                                Test = () =>
                                {
                                    object classInstance = Activator.CreateInstance(info.DeclaringType, null);
                                    info.Invoke(classInstance, null);
                                }
                            }).SetName(methodInfo.Name);
                    }
                }

            }
        }


        [TestSequence(1)]
        public void Test1()
        {
            // Act
            _integerNumber.Number += 5;
            var result = _integerNumber.Number;

            // Assert
            Assert.AreEqual(result, 5);
        }

        [TestSequence(2)]
        public void Test2()
        {
            // Act
            _integerNumber.Number -= 1;
            var result = _integerNumber.Number;

            // Assert
            Assert.AreEqual(result, 4);
        }

        [TestSequence(3)]
        public void Test3()
        {
            // Act
            _integerNumber.Number *= 6;
            var result = _integerNumber.Number;

            // Assert
            Assert.AreEqual(result, 24);
        }

        [TestSequence(4)]
        public void Test4()
        {
            // Act
            _integerNumber.Number /= 2;
            var result = _integerNumber.Number;

            // Assert
            Assert.AreEqual(result, 12);
        }

        [TestSequence(5)]
        public void Test5()
        {
            // Act
            _integerNumber.Number %= 7;
            var result = _integerNumber.Number;

            // Assert
            Assert.AreEqual(result, 5);
        }
    }
}
