using NUnit.Framework;
using Assignment8.BL;

namespace Assignment8.Test
{
    [TestFixture]
    public class Tests
    {
        private StudentRepository _studentRepository;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _studentRepository = new StudentRepository();
        }

        [Test]
        public void TestForName()
        {
            // Act
            var result = _studentRepository.GetStudents();

            // Assert
            Assert.That(result, Has
                .Exactly(1)
                .Property("Name")
                .EqualTo("Heta")
            );
        }

        [Test]
        public void TestForCity()
        {
            // Act
            var result = _studentRepository.GetStudents();

            // Assert
            Assert.That(result, Has
                .Exactly(2)
                .Property("City")
                .EqualTo("Surat")
            );
        }

        [Test]
        public void TestForState()
        {
            // Act
            var result = _studentRepository.GetStudents();

            // Assert
            Assert.That(result, Has
                .Exactly(3)
                .Property("State")
                .EqualTo("Gujarat")
            );
        }

        [Test]
        public void TestForAge()
        {
            // Act
            var result = _studentRepository.GetStudents();

            // Assert
            Assert.That(result, Has
                .Exactly(1)
                .Property("Age")
                .LessThan(20)
            );
        }

        [Test]
        public void TestMultipleProperties()
        {
            // Act
            var result = _studentRepository.GetStudents();

            // Assert
            Assert.That(result, Has
                .Count.EqualTo(4)
                .And.Exactly(0).Property("Name").EqualTo("Manthan")
                .And.Exactly(1).Property("City").EqualTo("Pune")
                .And.Exactly(3).Property("State").EqualTo("Gujarat")
                .And.Exactly(2).Property("Gender").EqualTo("Female")
                .And.Exactly(4).Property("Age").GreaterThan(17)
            );
        }
    }
}