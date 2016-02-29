namespace LinkedQueue.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedQueueTests
    {
        [TestMethod]
        public void EnqueueDequeue_ShouldWorkCorrectly()
        {
            var linkedQueue = new LinkedQueue<int>();
            var testElement = 17;

            Assert.AreEqual(0, linkedQueue.Count);
            linkedQueue.Enqueue(testElement);
            Assert.AreEqual(1, linkedQueue.Count);
            var popElement = linkedQueue.Dequeue();
            Assert.AreEqual(testElement, popElement);
            Assert.AreEqual(0, linkedQueue.Count);
        }

        [TestMethod]
        public void EnqueueDequeue_1000Elements_ShouldWorkCorrectly()
        {
            const int TestIterations = 1000;

            var linkedQueue = new LinkedQueue<string>();
            Assert.AreEqual(0, linkedQueue.Count);

            for (int i = 0; i < TestIterations; i++)
            {
                linkedQueue.Enqueue(i.ToString());
                Assert.AreEqual(i + 1, linkedQueue.Count);
            }

            for (int i = 0; i < TestIterations; i++)
            {
                var currentElement = linkedQueue.Dequeue();
                Assert.AreEqual(i.ToString(), currentElement);
                Assert.AreEqual(TestIterations - i - 1, linkedQueue.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_EmptylinkedQueue_ShouldThrow()
        {
            var linkedQueue = new LinkedQueue<int>();

            linkedQueue.Dequeue();
        }

        [TestMethod]
        public void ToArray_ShouldReturnCorrectArray()
        {
            var initialArr = new[] { 7, -2, 5, 3 };

            var linkedQueue = new LinkedQueue<int>();
            for (int i = initialArr.Length - 1; i >= 0; i--)
            {
                linkedQueue.Enqueue(initialArr[i]);
            }

            var convertedStack = linkedQueue.ToArray();
            CollectionAssert.AreEqual(initialArr, convertedStack);
        }

        [TestMethod]
        public void ToArray_EmptylinkedQueue_ShouldReturnEmptyArray()
        {
            LinkedQueue<int> linkedQueue = new LinkedQueue<int>();

            var convertedStack = linkedQueue.ToArray();
            CollectionAssert.AreEqual(new Queue<int>() { }, convertedStack);
        }
    }
}
