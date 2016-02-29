namespace Circular_Queue
{
    using System;

    public class CircularQueue<T>
    {
        private const int DefaultCapacity = 16;

        private T[] array;
        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = DefaultCapacity)
        {
            this.array = new T[capacity];
            this.Count = 0;
            this.startIndex = 0;
            this.endIndex = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.array.Length; }
        }

        public void Enqueue(T element)
        {
            if (this.Count >= this.Capacity)
            {
                this.Grow();
            }

            this.array[this.endIndex] = element;
            this.endIndex = (this.endIndex + 1) % this.Capacity;
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T element = this.array[this.startIndex];
            this.array[this.startIndex] = default(T);
            this.startIndex = (this.startIndex + 1) % this.Capacity;
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];

            this.CopyAllElementTo(result);

            return result;
        }

        private void Grow()
        {
            T[] newArray = new T[this.Capacity * 2];
            this.CopyAllElementTo(newArray);
            this.array = newArray;
            this.startIndex = 0;
            this.endIndex = this.Count;
        }

        private void CopyAllElementTo(T[] resultArr)
        {
            int sourceIndex = this.startIndex;
            for (int destinationIndex = 0; destinationIndex < this.Count; destinationIndex++)
            {
                resultArr[destinationIndex] = this.array[sourceIndex];
                sourceIndex = (sourceIndex + 1) % this.Capacity;
            }
        }
    }
}