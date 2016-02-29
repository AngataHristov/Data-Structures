namespace CustomLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MyLinkedList<T> : IEnumerable<T>
    {
        private ListNode<T> head;
        private ListNode<T> tail;
        private int count;

        public MyLinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public int Count
        {
            get { return this.count; }
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);

                ListNode<T> currentNode = this.head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }

                return currentNode.Element;
            }

            set
            {
                CheckIndex(index);

                ListNode<T> currentNode = this.head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }

                currentNode.Element = value;
            }
        }

        public void Add(T item)
        {
            if (this.head == null)
            {
                this.head = new ListNode<T>(item);
                this.tail = this.head;
            }
            else
            {
                ListNode<T> newNode = new ListNode<T>(item, this.tail);
                this.tail = newNode;
            }

            this.count++;
        }

        public T RemoveAt(int index)
        {
            CheckIndex(index);

            int currentIndex = 0;
            ListNode<T> currentNode = this.head;
            ListNode<T> prevNode = null;

            while (currentIndex < index)
            {
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
                currentIndex++;
            }

            this.RemoveListNode(currentNode, prevNode);

            return currentNode.Element;
        }

        public int Remove(T item)
        {
            int currentIndex = 0;
            ListNode<T> currentNode = this.head;
            ListNode<T> prevNode = null;

            while (currentNode != null)
            {
                if (object.Equals(currentNode.Element, item))
                {
                    break;
                }

                prevNode = currentNode;
                currentNode = currentNode.NextNode;
                currentIndex++;
            }

            if (currentNode != null)
            {
                this.RemoveListNode(currentNode, prevNode);
                return currentIndex;
            }

            return -1;
        }

        public int IndexOf(T item)
        {
            int index = 0;
            ListNode<T> currentNode = this.head;

            while (currentNode != null)
            {
                if (object.Equals(currentNode.Element, item))
                {
                    return index;
                }

                currentNode = currentNode.NextNode;
                index++;
            }

            return -1;
        }

        public bool Contains(T item)
        {
            int index = this.IndexOf(item);
            bool found = index != -1;

            return found;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Element;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            int index = 0;
            var currentNode = this.head;
            while (currentNode != null)
            {
                array[index] = currentNode.Element;
                currentNode = currentNode.NextNode;
                index++;
            }

            return array;
        }

        private void RemoveListNode(ListNode<T> node, ListNode<T> prevNode)
        {
            this.count--;
            if (this.count == 0)
            {
                this.head = null;
                this.tail = null;
            }
            else if (prevNode == null)
            {
                this.head = node.NextNode;
            }
            else
            {
                prevNode.NextNode = node.NextNode;
            }

            if (object.ReferenceEquals(this.tail, node))
            {
                this.tail = prevNode;
            }
        }

        private void CheckIndex(int index)
        {
            if (index >= this.count || index < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid index: " + index);
            }
        }
    }
}
