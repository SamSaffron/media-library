using System;
using System.Text;
using System.Collections.Generic;

namespace MediaLibrary.Caching {
    class LruCache<TKey, TValue> : IDictionary<TKey, TValue> {

        public LruCache(int maxSize) {
            // we may have one more entry temporarily
            data = new Dictionary<TKey, TValue>(maxSize+1);
            dataAsCollection = data;
            this.maxSize = maxSize;
        }

        object sync = new object();
        Dictionary<TKey, TValue> data;
        IndexedLinkedList<TKey> lruList = new IndexedLinkedList<TKey>();
        ICollection<KeyValuePair<TKey, TValue>> dataAsCollection;
        int maxSize;


        public void Add(TKey key, TValue value) {
            if (!ContainsKey(key)) {
                this[key] = value;
            } else {
                throw new ArgumentException("An attempt was made to insert a duplicate key in the cache.");
            }
        }

        public bool ContainsKey(TKey key) {
            return data.ContainsKey(key);
        }

        public ICollection<TKey> Keys {
            get {
                return data.Keys;
            }
        }

        public bool Remove(TKey key) {
            bool existed = data.Remove(key);
            lruList.Remove(key);
            return existed;
        }

        public bool TryGetValue(TKey key, out TValue value) {
            return data.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values {
            get { return data.Values; }
        }

        public TValue this[TKey key] {
            get {
                var value = data[key];
                lruList.Remove(key);
                lruList.Add(key);
                return value;
            }
            set {
                data[key] = value;
                lruList.Remove(key);
                lruList.Add(key);

                if (data.Count > maxSize) {
                    Remove(lruList.First);
                    lruList.RemoveFirst();
                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) {
            Add(item.Key, item.Value);
        }

        public void Clear() {
            data.Clear();
            lruList.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) {
            return dataAsCollection.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            dataAsCollection.CopyTo(array, arrayIndex);
        }

        public int Count {
            get { return data.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) {

            bool removed = dataAsCollection.Remove(item);
            if (removed) {
                lruList.Remove(item.Key);
            }
            return removed;
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return dataAsCollection.GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return ((System.Collections.IEnumerable)data).GetEnumerator();
        }

    }
}