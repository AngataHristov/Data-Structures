
namespace ReversedList.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReversedListTests
    {
        [TestMethod]
        public void Add_EmptyList_ShouldAdd()
        {
            var emptyReversedList = new MyReversedList<int>();
            emptyReversedList.Add(5);
            Assert.AreEqual(1, emptyReversedList.Count);
        }

        [TestMethod]
        public void Remove_ListOfElements_ShouldRemove()
        {
            var reversedList = new MyReversedList<int> { 1, 2, 3 };

            reversedList.RemoveAt(1);

            var expectedAtIndexZero = 3;
            var expectedAtIndexOne = 1;

            Assert.AreEqual(expectedAtIndexZero, reversedList[0]);
            Assert.AreEqual(expectedAtIndexOne, reversedList[1]);
        }

        [TestMethod]
        public void Remove_UsingForLoop_ShouldRemoveCorrectly()
        {
            var reversedList = new MyReversedList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 5; i++)
            {
                reversedList.RemoveAt(i);
            }

            var expected = new List<int>() { 8, 6, 4, 2 };
            var result = reversedList.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Remove_EmptyList_ShouldThrow()
        {
            var reversedList = new MyReversedList<int>();

            reversedList.RemoveAt(0);
        }

        [TestMethod]
        public void Count_AddElement_ShouldIncreaseCount()
        {
            var reversedList = new MyReversedList<int>();
            reversedList.Add(5);

            Assert.AreEqual(1, reversedList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Capacity_InvalidCapacity_ShouldThrow()
        {
            var reversedList = new MyReversedList<int>(-2);
        }

        [TestMethod]
        public void CapacityResize_AddElements_ShouldResizeDouble()
        {
            var reversedList = new MyReversedList<int>(2);
            reversedList.Add(5);
            reversedList.Add(5);
            reversedList.Add(5);
            reversedList.Add(5);
            reversedList.Add(5);

            Assert.AreEqual(8, reversedList.Capacity);
        }

        [TestMethod]
        public void IndexatorGet_FullList_ValidIndex_ShouldReturnElement()
        {
            var reversedList = new MyReversedList<int>();
            reversedList.Add(1);
            reversedList.Add(2);
            reversedList.Add(3);
            reversedList.Add(4);
            reversedList.Add(5);

            var expectedElement = reversedList[0];
            Assert.AreEqual(5, expectedElement);
        }

        [TestMethod]
        public void IndexatorSet_ValidIndex_ShouldAddElement()
        {
            var reversedList = new MyReversedList<int>();
            reversedList.Add(1);
            reversedList.Add(2);
            reversedList.Add(3);
            reversedList.Add(4);
            reversedList.Add(5);

            reversedList[0] = 69;

            var expectedElement = reversedList[0];
            Assert.AreEqual(69, expectedElement);
        }
    }
}
