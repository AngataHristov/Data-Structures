namespace LinkedStack.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedStackTests
    {
        [TestMethod]
        public void PushPop_ShouldWorkCorrectly()
        {
            var stack = new LinkedStack<int>();
            var testElement = 17;

            Assert.AreEqual(0, stack.Count);
            stack.Push(testElement);
            Assert.AreEqual(1, stack.Count);
            var popElement = stack.Pop();
            Assert.AreEqual(testElement, popElement);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void PushPop_1000Elements_ShouldWorkCorrectly()
        {
            const int TestIterations = 1000;

            var stack = new LinkedStack<string>();
            Assert.AreEqual(0, stack.Count);

            for (int i = 0; i < TestIterations; i++)
            {
                stack.Push(i.ToString());
                Assert.AreEqual(i + 1, stack.Count);
            }

            for (int i = TestIterations - 1; i >= 0; i--)
            {
                var currentElement = stack.Pop();
                Assert.AreEqual(i.ToString(), currentElement);
                Assert.AreEqual(i, stack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ShouldThrow()
        {
            var stack = new LinkedStack<int>();

            stack.Pop();
        }

        [TestMethod]
        public void ToArray_ShouldReturnCorrectArray()
        {
            var initialArr = new[] { 7, -2, 5, 3 };

            var stack = new LinkedStack<int>();
            for (int i = initialArr.Length - 1; i >= 0; i--)
            {
                stack.Push(initialArr[i]);
            }

            var convertedStack = stack.ToArray();
            CollectionAssert.AreEqual(initialArr, convertedStack);
        }

        [TestMethod]
        public void ToArray_EmptyStack_ShouldReturnEmptyArray()
        {
            LinkedStack<int> stack = new LinkedStack<int>();

            var convertedStack = stack.ToArray();
            CollectionAssert.AreEqual(new Stack<int>() { }, convertedStack);
        }

        [TestMethod]
        public void Peek_ShouldReturnCorrectElement()
        {
            var stack = new LinkedStack<int>();
            var testElement = 1;

            stack.Push(testElement);

            var peekEelement = stack.Peek();
            Assert.AreEqual(testElement, peekEelement);
        }
    }
}
