// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IndieSystem.Text.RegularExpressions
{
    internal sealed class CollectionDebuggerProxy<T>
    {
        private readonly ICollection<T> _collection;

        public CollectionDebuggerProxy(ICollection<T> collection)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(collection);
            _collection = collection;
#else
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
#endif
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var items = new T[_collection.Count];
                _collection.CopyTo(items, 0);
                return items;
            }
        }
    }
}
