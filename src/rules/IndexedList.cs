﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class IndexedList<K,T> :  IEnumerable<T>
    {
        private Dictionary<K, List<T>> _dictionary = new Dictionary<K, List<T>>();

        public List<T> this[K key] { get => _dictionary[key]; set { _dictionary[key] = value; } }

        public ICollection<K> Keys => _dictionary.Keys;

        public ICollection<List<T>> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(K key, List<T> value) => _dictionary.Add(key, value);

        public void Add(K key, T value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, new List<T>());
            }
            _dictionary[key].Add(value);
        }

        public void Clear() => _dictionary.Clear();

        public bool Contains(KeyValuePair<K, List<T>> item) => _dictionary.Contains(item);

        public bool ContainsKey(K key) => _dictionary.ContainsKey(key);

        public bool Remove(K key) => _dictionary.Remove(key);

        public bool TryGetValue(K key, out List<T> value) => TryGetValue(key, out value);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var k in _dictionary.Keys)
                foreach (var v in _dictionary[k])
                {
                    yield return v;
                }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}