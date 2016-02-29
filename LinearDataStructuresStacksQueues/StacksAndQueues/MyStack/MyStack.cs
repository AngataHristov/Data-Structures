namespace MyStack
{
    using System;

    public class MyStack<T>
    {
        private const int DefaultCapacity = 16;

        private T[] items;
        private int capacity;

        public MyStack(int capacity = DefaultCapacity)
        {
            this.Capacity = capacity;
            this.items = new T[this.Capacity];
            this.Count = 0;
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Capacity.");
                }

                this.capacity = value;
            }
        }

        public int Count { get; private set; }

        public void Push(T element)
        {
            if (this.Count == this.Capacity)
            {
                this.Resize();
            }

            this.items[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            this.ValidateNotEmpty();

            T element = this.items[this.Count - 1];
            this.items[this.Count - 1] = default(T);
            this.Count--;

            return element;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();

            T element = this.items[this.Count - 1];

            return element;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];

            Array.Copy(this.items, array, this.Count);

            return array;
        }

        private void ValidateNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
        }

        private void Resize()
        {
            var newArray = new T[2 * this.Capacity];
            Array.Copy(this.items, newArray, this.items.Length);
            this.items = newArray;
            this.Capacity *= 2;
        }
    }
}
