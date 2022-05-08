using NUnit.Framework;
using System;
using System.Linq;

namespace Collections.Tests
{
    public class CollectionsTests
    {
        private object expectedValue;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collections_ToStringEmpty()
        {
            // Arrange
            var nums = new Collection<int>();

            // Act
            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
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

        [Test]
        public void Test_Collections_ToStringCollektionOfCollections()
        {
            // Arrange
            var nums = new Collection<Collection<string>>();
            var item1 = new Collection<string>("I", "You", "He");
            var item2 = new Collection<string>("We", "You", "They");
            var item3 = new Collection<string>("She", "It");
            var item4 = new Collection<string>();

            // Act
            var items = new Collection<Collection<string>>(item1, item2, item3, item4);
            string itemsToString = items.ToString();
            var expectedResult = "[[I, You, He], [We, You, They], [She, It], []]";

            // Assert
            Assert.That(itemsToString, Is.EqualTo(expectedResult));
        }

        [TestCase("", "[]")]
        [TestCase("I", "[I]")]
        [TestCase("I,You", "[I,You]")]
        [TestCase("I,You,He", "[I,You,He]")]
        public void Test_Collections_ToStringEmptySingleOrMultiple(string data, string expectedResult)
        {
            // Arrange
            var items = new Collection<string>(data);
           

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult));
        }

        [TestCase("[]", null, "[]")]
        [TestCase("I", 0, "I")]
        [TestCase("I,You,He", 0, "I")]
        [TestCase("I,You,He", 1, "You")]
        [TestCase("I,You,He", 2, "He")]
        public void Test_Collections_GetByValidIndex(string data, int index, string expectedResult)
        {
            // Arrange
            var items = new Collection<string>(data.Split(","));
            var currentIndex = items[index];

            // Assert
            Assert.That(currentIndex, Is.EqualTo(expectedResult));
        }

        [TestCase("", 0)]
        [TestCase("I", -1)]
        [TestCase("I", 1)]
        [TestCase("I,You,He", -3)]
        [TestCase("I,You,He", 3)]
        [TestCase("I,You,He", 100)]
        public void Test_Collections_GetByInvalidIndex(string data, int index)
        {
            // Arrange
            var items = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));            

            // Assert
            Assert.That(() => items[index], Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_AddElements()
        {
            // Arrange
            var items = new Collection<int>();
            var expectedResult = "[7, 8, 9]";

            //Act            
            items.Add(7);
            items.Add(8);
            items.Add(9);

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            // Arrange
            var items = new Collection<int>();            
            var newItems = Enumerable.Range(100, 2000).ToArray();

            //Act            
            items.AddRange(newItems);
            string expectedResult = "[" + string.Join(", ", newItems) + "]";

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult)); 
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            // Arrange
            var items = new Collection<int>();
            int oldCapaciti = items.Capacity;
            var newItems = Enumerable.Range(100, 2000).ToArray();

            //Act            
            items.AddRange(newItems);
            string expectedResult = "[" + string.Join(", ", newItems) + "]";

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult));
            Assert.That(items.Capacity, Is.GreaterThanOrEqualTo(oldCapaciti));
            Assert.That(items.Capacity, Is.GreaterThanOrEqualTo(items.Count));            
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            // Arrange
            const int itemsCount = 1000000;
            var nums = new Collection<int>();

            //Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            // Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        [Timeout(1001)]
        public void Test_Collection_1MillionItemsAdd()
        {
            // Arrange
            const int itemsCount = 1000001;
            var nums = new Collection<int>();

            //Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            // Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);     
        }

        
        [TestCase("I,You,He", 0, "[You, He]")]
        [TestCase("I,You,He", 2, "[I, You]")]
        [TestCase("I,You,He", 1, "[I, He]")]
        public void Test_Collections_RemoweAt(string data, int index, string expectedResult)
        {
            // Arrange
            var items = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));

            //Act
            items.RemoveAt(index);

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            // Arrange
            var items = new Collection<int>(new int[] {1,2,3,4,5 });            

            //Act            
            items.Clear();
            
            var expectedResult = "[]";

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult));
        }

       
        [TestCase("I", -1)]
        [TestCase("I", 1)]
        [TestCase("I,You,He", -3)]
        [TestCase("I,You,He", 3)]
        [TestCase("I,You,He", 100)]
        public void Test_Collections_RemoveAtInvalidIndex(string data, int index)
        {
            // Arrange
            var items = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));

            // Act         
            // Assert
            Assert.That(() => items.RemoveAt(index), Throws.InstanceOf<ArgumentOutOfRangeException>());            
        }

        [TestCase("You,He", "I", 0, "[I, You, He]")]
        [TestCase("I,You", "He", 2, "[I, You, He]")]
        [TestCase("I,He", "You", 1, "[I, You, He]")]
        public void Test_Collections_Insert(string data, string element, int index, string expectedResult)
        {
            // Arrange
            var items = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));

            //Act           
            items.InsertAt(index, element);
           

            // Assert
            Assert.That(items.ToString(), Is.EqualTo(expectedResult));
        }

        [TestCase("You,He", "I", -1)]
        [TestCase("You,He", "I", 5)]
        [TestCase("You,He", "I", 100)]
        [TestCase("You,He", "I", -3)]

        public void Test_Collections_InsertAtInvalidIndex(string data, string element, int index)
        {
            // Arrange
            var items = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));

            // Act  
            // Assert
            Assert.That(() => items.InsertAt(index, element), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}