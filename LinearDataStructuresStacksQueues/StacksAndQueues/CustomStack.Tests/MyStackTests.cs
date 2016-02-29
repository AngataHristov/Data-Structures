namespace CustomStack.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyStack;

    [TestClass]
    public class MyStackTests
    {
        private MyStack<int> stack;

        [TestInitialize]
        public void TestInitialize()
        {
            this.stack = new MyStack<int>();
        }

        [TestMethod]
        public void Push_ShouldIncrementCount()
        {
            this.stack.Push(5);
            Assert.AreEqual(1, this.stack.Count);
        }

        [TestMethod]
        public void PushPop_ShouldNonIncreaceCount()
        {
            this.stack.Push(5);
            var item = this.stack.Pop();
            Assert.AreEqual(0, this.stack.Count);
        }

        [TestMethod]
        public void PushOverCapacity_ShouldResizeCorrectly()
        {
            MyStack<int> fixedStack = new MyStack<int>(1);

            for (int i = 0; i < 100; i++)
            {
                fixedStack.Push(i);
            }

            Assert.AreEqual(100, fixedStack.Count);
        }

        [TestMethod]
        public void Pushed_MultipleElements_ShouldBePoppedInReverseOrder()
        {
            int[] numbers = { 3, 5, 10, 6, 2, 3, 1 };

            for (int i = 0; i < numbers.Length; i++)
            {
                this.stack.Push(numbers[i]);
            }

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                var currentNum = this.stack.Pop();
                Assert.AreEqual(numbers[i], currentNum);
            }
        }

        [TestMethod]
        public void Peek_NonEmptyStack_ShouldReturnsLastPushedElement()
        {
            const int Element = 5;
            this.stack.Push(2);
            this.stack.Push(3);
            this.stack.Push(Element);

            var lastElement = this.stack.Peek();

            Assert.AreEqual(Element, lastElement);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_EmptyStack_ShouldThrow()
        {
            this.stack.Peek();
        }

        [TestMethod]
        public void Peek_ShouldReturnsLastPushedElement_WithoutRemovingIt()
        {
            int count = this.stack.Count;
            this.stack.Push(5);
            this.stack.Peek();
            Assert.AreEqual(count + 1, this.stack.Count);
        }

        [TestMethod]
        public void Pop_NonEmptyStack_ShouldReturnsLastPushedElement_AndRemoveIt()
        {
            const int LastItem = 10;
            for (int currentItem = 0; currentItem <= LastItem; currentItem++)
            {
                this.stack.Push(currentItem);
            }

            var count = this.stack.Count;
            var poppedItem = this.stack.Pop();
            Assert.AreEqual(LastItem, poppedItem);
            Assert.AreEqual(count - 1, this.stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ShouldThrow()
        {
            this.stack.Pop();
        }

        [TestMethod]
        public void ToArray_EmptyStack_ShouldReturnEmptyArray()
        {
            var arr = this.stack.ToArray();

            CollectionAssert.AreEqual(arr, new Stack<int>() { });
        }

        [TestMethod]
        public void ToArray_NonEmptyStack_ShouldReturnArray()
        {
            this.stack.Push(1);
            this.stack.Push(2);
            this.stack.Push(3);


            var arr = this.stack.ToArray();

            CollectionAssert.AreEqual(arr, new int[] { 1, 2, 3 });
        }
    }
}
