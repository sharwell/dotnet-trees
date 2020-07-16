﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace TunnelVisionLabs.Collections.Trees.Test
{
    using System;
    using System.Collections.Generic;

    internal sealed class SubsetHashCodeEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly IEqualityComparer<T> _equalityComparer;
        private readonly Func<T, int> _getHashCode;

        public SubsetHashCodeEqualityComparer(IEqualityComparer<T> equalityComparer, IEqualityComparer<T> hashCodeEqualityComparer)
            : this(equalityComparer, hashCodeEqualityComparer.GetHashCode)
        {
        }

        public SubsetHashCodeEqualityComparer(IEqualityComparer<T> equalityComparer, Func<T, int> getHashCode)
        {
            _equalityComparer = equalityComparer;
            _getHashCode = getHashCode;
        }

        public bool Equals(T x, T y) => _equalityComparer.Equals(x, y);

        public int GetHashCode(T obj) => _getHashCode(obj);
    }
}
