namespace ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Interfaces;

    public class MyReversedList<T> : IReversedList<T>
    {
        private const int DefaultSize = 16;

        private T[] elements;

        public MyReversedList(int size = DefaultSize)
        {
            this.elements = new T[size];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return this.elements.Length;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.GetAtIndex(index);
            }

            set
            {
                this.SetAtIndex(index, value);
            }
        }

        public void Add(T element)
        {
            if (this.Count >= this.Capacity)
            {
                this.DoubleCapacity();
            }

            this.elements[this.Count] = element;

            this.Count++;
        }

        public void Clear()
        {
            this.elements = new T[DefaultSize];
        }

        public int IndexOf(T element)
        {
            for (int index = 0; index < this.Count; index++)
            {
                if (this.elements[index].Equals(element))
                {
                    return index;
                }
            }

            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < arrayIndex; i++)
            {
                array[i] = this.elements[i];
            }
        }

        public void Insert(int index, T item)
        {
            if (this.Count >= this.Capacity)
            {
                this.DoubleCapacity();
            }

            for (int i = this.Count; i >= index; i--)
            {
                this.elements[i] = this.elements[i - 1];
            }

            this.elements[index] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            bool exist = false;

            for (int i = 0; i < this.Count; i++)
            {
                if (this.elements[i].Equals(item))
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }

        public bool Remove(T item)
        {
            if (!this.Contains(item))
            {
                return false;
            }

            int index = this.IndexOf(item);

            for (int i = index; i < this.Count; i++)
            {
                this.elements[i] = this.elements[i + 1];
            }

            this.Count++;

            return true;
        }

        public void RemoveAt(int index)
        {
            this.CheckIfIndexIsInRange(index);

            index = this.ReverseIndex(index);

            for (int i = index; i < this.Count - 1; i++)
            {
                this.elements[i] = this.elements[i + 1];
            }

            this.elements[this.Count - 1] = default(T);
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int index = this.Count-1;
            while (index >= 0)
            {
                yield return this.elements[index];
                index--;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            var result = string.Format($"[{string.Join(", ", this.elements)}]");

            return result;
        }

        private void DoubleCapacity()
        {
            T[] dobledElements = new T[this.Capacity * 2];

            for (int i = dobledElements.Length - 1; i >= this.Count; i--)
            {
                dobledElements[i] = this.elements[i - this.elements.Length];
            }

            this.elements = dobledElements;
        }

        private T GetAtIndex(int index)
        {
            this.CheckIfIndexIsInRange(index);

            index = this.ReverseIndex(index);

            var element = this.elements[index];

            return element;
        }

        private int ReverseIndex(int index)
        {
            var reversedIndex = this.Count - 1 - index;

            return reversedIndex;
        }

        private void SetAtIndex(int index, T element)
        {
            this.CheckIfIndexIsInRange(index);

            this.ResizeIfNeeded();

            index = this.ReverseIndex(index) + 1;

            for (int i = this.Count; i >= index; i--)
            {
                this.elements[i] = this.elements[i - 1];
            }

            this.elements[index] = element;
            this.Count++;
        }

        private void CheckIfIndexIsInRange(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
        }

        private void ResizeIfNeeded()
        {
            if (this.Count >= this.Capacity - 1)
            {
                this.DoubleCapacity();
            }
        }
    }
}
