namespace BiDictionaryImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class BiDictionary<K1, K2, T>
    {
        private Dictionary<K1, IList<T>> valuesByFirstKey;
        private Dictionary<K2, IList<T>> valuesBySecondKey;
        private Dictionary<Tuple<K1, K2>, IList<T>> valuesByBothKeys;

        public BiDictionary()
        {
            this.valuesByFirstKey = new Dictionary<K1, IList<T>>();
            this.valuesBySecondKey = new Dictionary<K2, IList<T>>();
            this.valuesByBothKeys = new Dictionary<Tuple<K1, K2>, IList<T>>();
        }

        public void Add(K1 key1, K2 key2, T value)
        {
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                this.valuesByFirstKey[key1] = new List<T>();
            }

            this.valuesByFirstKey[key1].Add(value);

            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                this.valuesBySecondKey[key2] = new List<T>();
            }

            this.valuesBySecondKey[key2].Add(value);

            Tuple<K1, K2> bothKeys = new Tuple<K1, K2>(key1, key2);
            if (!this.valuesByBothKeys.Keys.Any(k => k.Item1.Equals(key1) && k.Item2.Equals(key2)))
            {
                this.valuesByBothKeys[bothKeys] = new List<T>();
            }

            this.valuesByBothKeys[bothKeys].Add(value);
        }

        public IEnumerable<T> Find(K1 key1, K2 key2)
        {
            var result = this.valuesByBothKeys.Keys
                .FirstOrDefault(k => k.Item1.Equals(key1) && k.Item2.Equals(key2));

            if (result == null)
            {
                return new List<T>();
            }

            return this.valuesByBothKeys[result];

        }

        public IEnumerable<T> FindByKey1(K1 key1)
        {
            var result = this.valuesByFirstKey.Keys
                .FirstOrDefault(k => k.Equals(key1));

            if (result == null)
            {
                return new List<T>();
            }

            return this.valuesByFirstKey[key1];
        }

        public IEnumerable<T> FindByKey2(K2 key2)
        {
            var result = this.valuesBySecondKey.Keys
                .FirstOrDefault(k => k.Equals(key2));

            if (result == null)
            {
                return new List<T>();
            }

            return this.valuesBySecondKey[key2];
        }

        public bool Remove(K1 key1, K2 key2)
        {
            Tuple<K1, K2> bothKeys = this.valuesByBothKeys.Keys
                .FirstOrDefault(k => k.Item1.Equals(key1) && k.Item2.Equals(key2));

            IList<T> values = this.valuesByBothKeys[bothKeys];

            foreach (T value in values)
            {
                this.valuesByFirstKey[key1].Remove(value);
                this.valuesBySecondKey[key2].Remove(value);
            }

            bool areBothKeysRemoved = this.valuesByBothKeys.Remove(bothKeys);

            if (areBothKeysRemoved)
            {
                return true;
            }

            return false;
        }
    }
}