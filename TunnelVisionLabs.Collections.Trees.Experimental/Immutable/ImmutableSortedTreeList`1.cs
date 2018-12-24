// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace TunnelVisionLabs.Collections.Trees.Immutable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal sealed partial class ImmutableSortedTreeList<T> : IImmutableList<T>, IReadOnlyList<T>, IList<T>, IList
    {
        public static readonly ImmutableSortedTreeList<T> Empty = new ImmutableSortedTreeList<T>(ImmutableTreeList<T>.Empty, Comparer<T>.Default);

        private readonly IComparer<T> _comparer;
        private readonly ImmutableTreeList<T> _treeList;

        private ImmutableSortedTreeList(ImmutableTreeList<T> treeList, IComparer<T> comparer)
        {
            _treeList = treeList;
            _comparer = comparer;
        }

        public IComparer<T> Comparer => _comparer;

        public int Count => _treeList.Count;

        public bool IsEmpty => _treeList.IsEmpty;

        public T Max => throw null;

        public T Min => throw null;

        bool ICollection<T>.IsReadOnly => true;

        bool IList.IsFixedSize => true;

        bool IList.IsReadOnly => true;

        bool ICollection.IsSynchronized => true;

        object ICollection.SyncRoot => this;

        public T this[int index] => _treeList[index];

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

        public ImmutableSortedTreeList<T> Add(T value) => Add(value, addIfPresent: true);

        internal ImmutableSortedTreeList<T> Add(T item, bool addIfPresent)
        {
            var comparer = addIfPresent ? new CoercingComparer(_comparer, -1) : _comparer;
            int result = _treeList.BinarySearch(item, comparer);
            if (result >= 0)
                return this;

            var treeList = _treeList.Insert(~result, item);
            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public ImmutableSortedTreeList<T> AddRange(IEnumerable<T> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var builder = ToBuilder();
            builder.AddRange(items);
            return builder.ToImmutable();
        }

        public int BinarySearch(T item) => _treeList.BinarySearch(item, _comparer);

        public int BinarySearch(int index, int count, T item) => _treeList.BinarySearch(index, count, item, _comparer);

        public ImmutableSortedTreeList<T> Clear()
        {
            var treeList = _treeList.Clear();
            if (treeList == _treeList)
            {
                return this;
            }

            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public bool Contains(T value)
            => _treeList.BinarySearch(value, _comparer) >= 0;

        public void CopyTo(T[] array)
            => _treeList.CopyTo(array);

        public void CopyTo(T[] array, int arrayIndex)
            => _treeList.CopyTo(array, arrayIndex);

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
            => _treeList.CopyTo(index, array, arrayIndex, count);

        public bool Exists(Predicate<T> match)
            => _treeList.Exists(match);

        public T Find(Predicate<T> match)
            => _treeList.Find(match);

        public ImmutableSortedTreeList<T> FindAll(Predicate<T> match)
        {
            var treeList = _treeList.FindAll(match);
            if (treeList == _treeList)
            {
                return this;
            }

            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public int FindIndex(Predicate<T> match)
            => _treeList.FindIndex(match);

        public int FindIndex(int startIndex, Predicate<T> match)
            => _treeList.FindIndex(startIndex, match);

        public int FindIndex(int startIndex, int count, Predicate<T> match)
            => _treeList.FindIndex(startIndex, count, match);

        public T FindLast(Predicate<T> match)
            => _treeList.FindLast(match);

        public int FindLastIndex(Predicate<T> match)
            => _treeList.FindLastIndex(match);

        public int FindLastIndex(int startIndex, Predicate<T> match)
            => _treeList.FindLastIndex(startIndex, match);

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
            => _treeList.FindLastIndex(startIndex, count, match);

        public void ForEach(Action<T> action)
            => _treeList.ForEach(action);

        public Enumerator GetEnumerator()
            => new Enumerator(_treeList.GetEnumerator());

        public ImmutableSortedTreeList<T> GetRange(int index, int count)
        {
            var treeList = _treeList.GetRange(index, count);
            if (treeList == _treeList)
            {
                return this;
            }

            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public int IndexOf(T value)
            => IndexOf(value, 0, Count, equalityComparer: null);

        public int IndexOf(T value, int index)
            => IndexOf(value, index, Count - index, equalityComparer: null);

        public int IndexOf(T value, int index, int count)
            => IndexOf(value, index, count, equalityComparer: null);

        public int IndexOf(T value, int index, int count, IEqualityComparer<T> equalityComparer) => throw null;

        public int LastIndexOf(T item)
        {
            if (IsEmpty)
                return -1;

            return LastIndexOf(item, Count - 1, Count, equalityComparer: null);
        }

        public int LastIndexOf(T item, int index)
            => LastIndexOf(item, index, index + 1, equalityComparer: null);

        public int LastIndexOf(T item, int index, int count)
            => LastIndexOf(item, index, count, equalityComparer: null);

        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer) => throw null;

        public ImmutableSortedTreeList<T> Remove(T value)
            => Remove(value, equalityComparer: null);

        public ImmutableSortedTreeList<T> Remove(T value, IEqualityComparer<T> equalityComparer)
        {
            int index = IndexOf(value, 0, Count, equalityComparer);
            if (index < 0)
                return this;

            return RemoveAt(index);
        }

        public ImmutableSortedTreeList<T> RemoveAll(Predicate<T> match)
        {
            var treeList = _treeList.RemoveAll(match);
            if (treeList == _treeList)
            {
                return this;
            }

            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public ImmutableSortedTreeList<T> RemoveAt(int index)
        {
            var treeList = _treeList.RemoveAt(index);
            if (treeList == _treeList)
            {
                return this;
            }

            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public ImmutableSortedTreeList<T> RemoveRange(IEnumerable<T> items)
            => RemoveRange(items, equalityComparer: null);

        public ImmutableSortedTreeList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer) => throw null;

        public ImmutableSortedTreeList<T> RemoveRange(int index, int count)
        {
            var treeList = _treeList.RemoveRange(index, count);
            if (treeList == _treeList)
                return this;

            return new ImmutableSortedTreeList<T>(treeList, _comparer);
        }

        public ImmutableSortedTreeList<T> Replace(T oldValue, T newValue)
            => Replace(oldValue, newValue, equalityComparer: null);

        public ImmutableSortedTreeList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer) => throw null;

        public IEnumerable<T> Reverse()
            => _treeList.Reverse();

        public ImmutableSortedTreeList<T> WithComparer(IComparer<T> comparer)
        {
            comparer = comparer ?? Comparer<T>.Default;
            if (comparer == _comparer)
                return this;

            return new ImmutableSortedTreeList<T>(_treeList.Sort(comparer), _comparer);
        }

        public Builder ToBuilder() => new Builder(this);

        public bool TrueForAll(Predicate<T> match) => _treeList.TrueForAll(match);

        IImmutableList<T> IImmutableList<T>.Clear() => Clear();

        IImmutableList<T> IImmutableList<T>.Add(T value) => Add(value);

        IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items) => AddRange(items);

        IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer) => Remove(value, equalityComparer);

        IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match) => RemoveAll(match);

        IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer) => RemoveRange(items, equalityComparer);

        IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count) => RemoveRange(index, count);

        IImmutableList<T> IImmutableList<T>.RemoveAt(int index) => RemoveAt(index);

        IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer) => Replace(oldValue, newValue, equalityComparer);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IImmutableList<T> IImmutableList<T>.Insert(int index, T element) => throw new NotSupportedException();

        IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items) => throw new NotSupportedException();

        IImmutableList<T> IImmutableList<T>.SetItem(int index, T value) => throw new NotSupportedException();

        void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

        void IList<T>.RemoveAt(int index) => throw new NotSupportedException();

        void ICollection<T>.Add(T item) => throw new NotSupportedException();

        void ICollection<T>.Clear() => throw new NotSupportedException();

        bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

        int IList.Add(object value) => throw new NotSupportedException();

        void IList.Clear() => throw new NotSupportedException();

        bool IList.Contains(object value) => throw null;

        int IList.IndexOf(object value) => throw null;

        void IList.Insert(int index, object value) => throw new NotSupportedException();

        void IList.Remove(object value) => throw new NotSupportedException();

        void IList.RemoveAt(int index) => throw new NotSupportedException();

        void ICollection.CopyTo(Array array, int index)
        {
            ICollection collection = _treeList;
            collection.CopyTo(array, index);
        }

        internal void Validate(ValidationRules validationRules)
        {
            Debug.Assert(_comparer != null, $"Assertion failed: _comparer != null");
            Debug.Assert(_treeList != null, $"Assertion failed: _treeList != null");

            _treeList.Validate(validationRules);

            for (int i = 0; i < Count - 1; i++)
            {
                Debug.Assert(_comparer.Compare(this[i], this[i + 1]) <= 0, "Assertion failed: _comparer.Compare(this[i], this[i + 1]) <= 0");
                Debug.Assert(_comparer.Compare(this[i + 1], this[i]) >= 0, "Assertion failed: _comparer.Compare(this[i + 1], this[i]) >= 0");
            }
        }

        private sealed class CoercingComparer : IComparer<T>
        {
            private readonly IComparer<T> _underlyingComparer;
            private readonly int _coerceResult;
            private bool _foundMatch;

            public CoercingComparer(IComparer<T> underlyingComparer, int coerceResult)
            {
                _underlyingComparer = underlyingComparer;
                _coerceResult = coerceResult;
            }

            public bool FoundMatch => _foundMatch;

            public int Compare(T x, T y)
            {
                int result = _underlyingComparer.Compare(x, y);
                if (result != 0)
                    return result;

                // if we get here from a binary search, it means we found the item we searched for
                _foundMatch = true;
                return _coerceResult;
            }
        }
    }
}
