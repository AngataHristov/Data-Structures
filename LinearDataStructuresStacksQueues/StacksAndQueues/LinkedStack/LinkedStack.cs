namespace LinkedStack
{
    using System;

    public class LinkedStack<T>
    {
        private Node<T> firstNode;

        public int Count { get; private set; }

        public LinkedStack()
        {
            this.Count = 0;
        }

        public void Push(T element)
        {
            Node<T> newNode = new Node<T>(element, this.firstNode);

            this.firstNode = newNode;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            Node<T> nodeToPop = this.firstNode;
            this.firstNode = this.firstNode.NextNode;

            this.Count--;

            return nodeToPop.Element;
        }

        public T Peek()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            Node<T> nodeToPop = this.firstNode;

            return nodeToPop.Element;
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            Node<T> currentNode = this.firstNode;

            int arrIndex = 0;

            while (currentNode != null)
            {
                array[arrIndex] = currentNode.Element;
                currentNode = currentNode.NextNode;

                arrIndex++;
            }

            return array;
        }
    }
}
