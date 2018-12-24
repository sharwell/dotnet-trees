// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace TunnelVisionLabs.Collections.Trees.Immutable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal sealed partial class ImmutableSortedTreeList<T>
    {
        public sealed class Builder : IList<T>, IReadOnlyList<T>, IList
        {
            private ImmutableSortedTreeList<T> _immutableList;
            private readonly ImmutableTreeList<T>.Builder _treeList;

            internal Builder(ImmutableSortedTreeList<T> immutableList)
            {
                _immutableList = immutableList;
                _treeList = immutableList._treeList.ToBuilder();
            }

            public IComparer<T> Comparer => _immutableList.Comparer;

            public int Count => _treeList.Count;

            bool ICollection<T>.IsReadOnly => false;

            bool IList.IsFixedSize => false;

            bool IList.IsReadOnly => false;

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => this;

            public T this[int index]
            {
                get => _treeList[index];
            }

            T IList<T>.this[int index]
            {
                get => this[index];
                set => throw new NotSupportedException();
            }

            object IList.this[int index]
            {
                get => this[index];
                set => throw new NotSupportedException();
            }

            public void Add(T item) => throw null;

            public void AddRange(IEnumerable<T> items) => throw null;

            public int BinarySearch(T item) => throw null;

            public int BinarySearch(int index, int count, T item) => throw null;

            public void Clear() => throw null;

            public bool Contains(T item) => BinarySearch(item) >= 0;

            public void CopyTo(T[] array) => CopyTo(0, array, 0, Count);

            public void CopyTo(T[] array, int arrayIndex) => CopyTo(0, array, arrayIndex, Count);

            public void CopyTo(int index, T[] array, int arrayIndex, int count) => throw null;

            public bool Exists(Predicate<T> match) => FindIndex(match) >= 0;

            public T Find(Predicate<T> match) => throw null;

            public ImmutableSortedTreeList<T> FindAll(Predicate<T> match) => throw null;

            public int FindIndex(Predicate<T> match) => FindIndex(0, Count, match);

            public int FindIndex(int startIndex, Predicate<T> match) => FindIndex(startIndex, Count - startIndex, match);

            public int FindIndex(int startIndex, int count, Predicate<T> match) => throw null;

            public T FindLast(Predicate<T> match) => throw null;

            public int FindLastIndex(Predicate<T> match) => FindLastIndex(Count - 1, Count, match);

            public int FindLastIndex(int startIndex, Predicate<T> match) => FindLastIndex(startIndex, startIndex + 1, match);

            public int FindLastIndex(int startIndex, int count, Predicate<T> match) => throw null;

            public void ForEach(Action<T> action) => _treeList.ForEach(action);

            public Enumerator GetEnumerator() => new Enumerator(_treeList.GetEnumerator());

            public ImmutableSortedTreeList<T> GetRange(int index, int count) => ToImmutable().GetRange(index, count);

            public int IndexOf(T item) => IndexOf(item, 0, Count, equalityComparer: null);

            public int IndexOf(T item, int index) => IndexOf(item, index, Count - index, equalityComparer: null);

            public int IndexOf(T item, int index, int count) => IndexOf(item, index, count, equalityComparer: null);

            public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer) => throw null;

            public int LastIndexOf(T item) => LastIndexOf(item, Count - 1, Count, equalityComparer: null);

            public int LastIndexOf(T item, int startIndex) => LastIndexOf(item, startIndex, startIndex + 1, equalityComparer: null);

            public int LastIndexOf(T item, int startIndex, int count) => LastIndexOf(item, startIndex, count, equalityComparer: null);

            public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer) => throw null;

            public bool Remove(T item)
            {
                int index = IndexOf(item);
                if (index >= 0)
                {
                    RemoveAt(index);
                    return true;
                }

                return false;
            }

            public int RemoveAll(Predicate<T> match) => throw null;

            public void RemoveAt(int index) => throw null;

            public ImmutableSortedTreeList<T> ToImmutable() => throw null;

            public bool TrueForAll(Predicate<T> match) => _treeList.TrueForAll(match);

            IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            int IList.Add(object value) => throw null;

            bool IList.Contains(object value) => throw null;

            int IList.IndexOf(object value)
            {
                if (value == null)
                {
                    if (default(T) == null)
                        return IndexOf(default);
                }
                else if (value is T)
                {
                    return IndexOf((T)value);
                }

                return -1;
            }

            void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

            void IList.Insert(int index, object value) => throw new NotSupportedException();

            void IList.Remove(object value)
            {
                int index = ((IList)this).IndexOf(value);
                if (index >= 0)
                    RemoveAt(index);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ICollection treeList = _treeList;
                treeList.CopyTo(array, index);
            }

            internal void TrimExcess() => throw null;

            internal void Validate(ValidationRules validationRules)
            {
                Debug.Assert(_immutableList != null, $"Assertion failed: _immutableList != null");
                Debug.Assert(_treeList != null, $"Assertion failed: _treeList != null");

                _treeList.Validate(validationRules);

                for (int i = 0; i < Count - 1; i++)
                {
                    Debug.Assert(Comparer.Compare(this[i], this[i + 1]) <= 0, "Assertion failed: Comparer.Compare(this[i], this[i + 1]) <= 0");
                    Debug.Assert(Comparer.Compare(this[i + 1], this[i]) >= 0, "Assertion failed: Comparer.Compare(this[i + 1], this[i]) >= 0");
                }
            }
        }
    }
}
