﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace TunnelVisionLabs.Collections.Trees.Immutable
{
    using System.Collections;
    using System.Collections.Generic;

    internal sealed partial class ImmutableSortedTreeList<T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private readonly ImmutableTreeList<T>.Enumerator _enumerator;

            internal Enumerator(ImmutableTreeList<T>.Enumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public T Current => _enumerator.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
                => _enumerator.Dispose();

            public bool MoveNext()
                => _enumerator.MoveNext();

            public void Reset()
                => _enumerator.Reset();
        }
    }
}
