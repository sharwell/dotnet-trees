// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace TunnelVisionLabs.Collections.Trees.Immutable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public sealed partial class ImmutableSortedTreeSet<T> : IImmutableSet<T>, ISet<T>, IList<T>, IReadOnlyList<T>, IList
    {
        public static readonly ImmutableSortedTreeSet<T> Empty = new ImmutableSortedTreeSet<T>(ImmutableSortedTreeList<T>.Empty);

        private readonly ImmutableSortedTreeList<T> _sortedList;

        internal ImmutableSortedTreeSet(ImmutableSortedTreeList<T> sortedList)
        {
            _sortedList = sortedList;
        }

        public IComparer<T> KeyComparer => _sortedList.Comparer;

        public int Count => _sortedList.Count;

        public bool IsEmpty => _sortedList.IsEmpty;

        public T Max => _sortedList.Max;

        public T Min => _sortedList.Min;

        bool ICollection<T>.IsReadOnly => true;

        bool ICollection.IsSynchronized => true;

        object ICollection.SyncRoot => this;

        bool IList.IsFixedSize => true;

        bool IList.IsReadOnly => true;

        public T this[int index] => _sortedList[index];

        T IList<T>.this[int index]
        {
            get => _sortedList[index];
            set => throw new NotSupportedException();
        }

        object IList.this[int index]
        {
            get => _sortedList[index];
            set => throw new NotSupportedException();
        }

        public ImmutableSortedTreeSet<T> Add(T value)
        {
            var sortedList = _sortedList.Add(value, addIfPresent: false);
            if (sortedList == _sortedList)
                return this;

            return new ImmutableSortedTreeSet<T>(sortedList);
        }

        public ImmutableSortedTreeSet<T> Clear()
        {
            var sortedList = _sortedList.Clear();
            if (sortedList == _sortedList)
                return this;

            return new ImmutableSortedTreeSet<T>(sortedList);
        }

        public bool Contains(T value)
            => _sortedList.Contains(value);

        public ImmutableSortedTreeSet<T> Except(IEnumerable<T> other)
        {
            var sortedList = _sortedList.RemoveRange(other);
            if (sortedList == _sortedList)
                return this;

            return new ImmutableSortedTreeSet<T>(sortedList);
        }

        public Enumerator GetEnumerator()
            => new Enumerator(_sortedList.GetEnumerator());

        public int IndexOf(T item)
            => _sortedList.IndexOf(item);

        public ImmutableSortedTreeSet<T> Intersect(IEnumerable<T> other) => throw null;

        public bool IsProperSubsetOf(IEnumerable<T> other) => throw null;

        public bool IsProperSupersetOf(IEnumerable<T> other) => throw null;

        public bool IsSubsetOf(IEnumerable<T> other) => throw null;

        public bool IsSupersetOf(IEnumerable<T> other) => throw null;

        public bool Overlaps(IEnumerable<T> other) => throw null;

        public ImmutableSortedTreeSet<T> Remove(T value)
        {
            var sortedList = _sortedList.Remove(value);
            if (sortedList == _sortedList)
                return this;

            return new ImmutableSortedTreeSet<T>(sortedList);
        }

        public IEnumerable<T> Reverse()
            => _sortedList.Reverse();

        public bool SetEquals(IEnumerable<T> other) => throw null;

        public ImmutableSortedTreeSet<T> SymmetricExcept(IEnumerable<T> other) => throw null;

        public bool TryGetValue(T equalValue, out T actualValue)
        {
            var index = IndexOf(equalValue);
            if (index < 0)
            {
                actualValue = default;
                return false;
            }

            actualValue = this[index];
            return true;
        }

        public ImmutableSortedTreeSet<T> Union(IEnumerable<T> other) => throw null;

        public Builder ToBuilder() => throw null;

        public ImmutableSortedTreeSet<T> WithComparer(IComparer<T> comparer) => throw null;

        IImmutableSet<T> IImmutableSet<T>.Clear() => throw null;

        IImmutableSet<T> IImmutableSet<T>.Add(T value) => throw null;

        IImmutableSet<T> IImmutableSet<T>.Remove(T value) => throw null;

        IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other) => throw null;

        IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other) => throw null;

        IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other) => throw null;

        IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other) => throw null;

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => throw null;

        void ICollection.CopyTo(Array array, int index) => throw null;

        bool IList.Contains(object value) => throw null;

        int IList.IndexOf(object value) => throw null;

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw null;

        IEnumerator IEnumerable.GetEnumerator() => throw null;

        bool ISet<T>.Add(T item) => throw new NotSupportedException();

        void ISet<T>.UnionWith(IEnumerable<T> other) => throw new NotSupportedException();

        void ISet<T>.IntersectWith(IEnumerable<T> other) => throw new NotSupportedException();

        void ISet<T>.ExceptWith(IEnumerable<T> other) => throw new NotSupportedException();

        void ISet<T>.SymmetricExceptWith(IEnumerable<T> other) => throw new NotSupportedException();

        void ICollection<T>.Add(T item) => throw new NotSupportedException();

        void ICollection<T>.Clear() => throw new NotSupportedException();

        bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

        void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

        void IList<T>.RemoveAt(int index) => throw new NotSupportedException();

        int IList.Add(object value) => throw new NotSupportedException();

        void IList.Clear() => throw new NotSupportedException();

        void IList.Insert(int index, object value) => throw new NotSupportedException();

        void IList.Remove(object value) => throw new NotSupportedException();

        void IList.RemoveAt(int index) => throw new NotSupportedException();
    }
}
