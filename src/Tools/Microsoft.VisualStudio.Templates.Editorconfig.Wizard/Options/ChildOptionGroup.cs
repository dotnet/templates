// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard
{
    public abstract class ChildOptionGroup : IOptionGroup
    {
        protected List<IOptionValue> _options = new List<IOptionValue>();
        public IOptionValue this[int index] => _options[index];
        public int Count => _options.Count;
        public bool AreAllOptionsDefault() => _options.All(x => x.IsDefault);

        public IOptionGroup this[string key] => throw new KeyNotFoundException();
        public IEnumerable<string> Keys => Enumerable.Empty<string>();
        public IEnumerable<IOptionGroup> Values => Enumerable.Empty<IOptionGroup>();
        public bool ContainsKey(string key) => false;
        public IEnumerator<KeyValuePair<string, IOptionGroup>> GetEnumerator()
            => Enumerable.Empty<KeyValuePair<string, IOptionGroup>>().GetEnumerator();
        public IEnumerable<(string groupName, IOptionGroup group)> GetSubGroups()
            => Enumerable.Empty<(string groupName, IOptionGroup group)>();
        public bool TryGetValue(string key, out IOptionGroup value)
        {
            value = null;
            return false;
        }

        IEnumerator<IOptionValue> IEnumerable<IOptionValue>.GetEnumerator() => _options.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _options.GetEnumerator();
    }
}
