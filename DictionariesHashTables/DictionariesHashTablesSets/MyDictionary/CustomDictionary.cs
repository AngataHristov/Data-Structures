namespace MyDictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading;

    public class CustomDictionary<Tkey, TValue> : IEnumerable<KeyValue<Tkey, TValue>>
    {
        private const int DefaultCapacity = 16;
        private const float LoadFactor = 0.75f;

        private LinkedList<KeyValue<Tkey, TValue>>[] slots;

        public CustomDictionary(int capacity = DefaultCapacity)
        {
            this.slots = new LinkedList<KeyValue<Tkey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.slots.Length; }
        }

        public IEnumerable<Tkey> Keys
        {
            get { return this.Select(element => element.Key); }
        }

        public IEnumerable<TValue> Values
        {
            get { return this.Select(element => element.Value); }
        }

        public TValue this[Tkey key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public void Add(Tkey key, TValue value)
        {
            this.GrowIfNeeded();

            int slotNumber = this.FindSlotNumber(key);

            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<Tkey, TValue>>();
            }

            foreach (KeyValue<Tkey, TValue> element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException(string.Format("Key already exists:  {0}", key));
                }
            }

            var newElement = new KeyValue<Tkey, TValue>(key, value);

            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
        }

        public bool AddOrReplace(Tkey key, TValue value)
        {
            this.GrowIfNeeded();

            int slotNumber = this.FindSlotNumber(key);

            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<Tkey, TValue>>();
            }

            foreach (KeyValue<Tkey, TValue> element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;

                    return false;
                }
            }

            var newElement = new KeyValue<Tkey, TValue>(key, value);

            this.slots[slotNumber].AddLast(newElement);
            this.Count++;

            return true;
        }

        public TValue Get(Tkey key)
        {
            var element = this.Find(key);

            if (element == null)
            {
                throw new KeyNotFoundException("Key not found");
            }

            return element.Value;
        }

        public bool TryGetValue(Tkey key, out TValue value)
        {
            var element = this.Find(key);

            if (element == null)
            {
                value = default(TValue);
            }

            value = element.Value;

            return true;
        }

        public KeyValue<Tkey, TValue> Find(Tkey key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];
            if (elements != null)
            {
                foreach (KeyValue<Tkey, TValue> element in elements)
                {
                    if (element.Key.Equals(key))
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(Tkey key)
        {
            var element = this.Find(key);

            return element != null;
        }

        public bool Remove(Tkey key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];

            if (elements != null)
            {
                var currentElement = elements.First;
                while (currentElement != null)
                {
                    if (currentElement.Value.Key.Equals(key))
                    {
                        elements.Remove(currentElement);
                        this.Count--;

                        return true;
                    }

                    currentElement = currentElement.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<Tkey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerator<KeyValue<Tkey, TValue>> GetEnumerator()
        {
            foreach (LinkedList<KeyValue<Tkey, TValue>> elements in this.slots)
            {
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        yield return element;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Grow()
        {
            var newDict = new CustomDictionary<Tkey, TValue>(2 * this.Capacity);

            foreach (KeyValue<Tkey, TValue> element in this)
            {
                newDict.Add(element.Key, element.Value);
            }

            this.slots = newDict.slots;
            this.Count = newDict.Count;
        }

        private void GrowIfNeeded()
        {
            if (((float)this.Count - 1) / this.Capacity > LoadFactor)
            {
                this.Grow();
            }
        }

        private int FindSlotNumber(Tkey key)
        {
            var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;

            return slotNumber;
        }
    }
}
