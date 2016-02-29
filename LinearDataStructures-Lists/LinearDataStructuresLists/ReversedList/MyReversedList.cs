namespace ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MyReversedList<T> : IList<T>
    {
        private const int DefaultSize = 16;

        private T[] elements;

        public MyReversedList(int size = DefaultSize)
        {
            this.elements = new T[size];
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

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
                if (index < 0 || index > this.elements.Length)
                {
                    throw new IndexOutOfRangeException("Invalid index: " + index);
                }

                return this.elements[index];
            }

            set
            {
                if (index < 0 || index > this.elements.Length)
                {
                    throw new IndexOutOfRangeException("Invalid index: " + index);
                }

                this.elements[this.Count - index - 1] = value;
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
            if (index > this.Count)
            {
                throw new IndexOutOfRangeException(String.Format("No element with index: {0}", index));
            }

            for (int i = index; i < this.Count; i++)
            {
                this.elements[i] = this.elements[i + 1];
            }

            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int index = 0;
            while (index < this.Count)
            {
                yield return this.elements[index];
                index++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void DoubleCapacity()
        {
            T[] dobledElements = new T[this.Capacity * 2];

            Array.Copy(this.elements, dobledElements, this.Capacity);

            this.elements = dobledElements;
        }
    }
}
