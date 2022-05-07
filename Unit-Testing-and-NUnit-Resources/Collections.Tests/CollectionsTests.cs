using NUnit.Framework;


namespace Collections.Tests
{
    public class CollectionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collections_ToStringSingle()
        {
            // Arrange
            var nums = new Collection<int>(10);

            // Act
            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[10]"));
        }

        [Test]
        public void Test_Collections_ToStringMultiple()
        {
            // Arrange
            var nums = new Collection<int>(10, 20, 30, 100, 2000000);

            // Act
            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 100, 2000000]"));
        }
    }
}