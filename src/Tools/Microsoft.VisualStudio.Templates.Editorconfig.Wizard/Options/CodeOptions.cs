// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeStyle;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions : IOptionGroup
    {
        private Dictionary<string, IOptionGroup> _groups = new Dictionary<string, IOptionGroup>();

        public CSharpCodeFormattingOptions CSharpFormattingOptions { get; }
        public CSharpCodeStyleOptions CSharpCodeStyle { get; }
        public VisualBasicFormattingOptions VisualBasicFormatting { get; }

        public IEnumerable<string> Keys => _groups.Keys;

        public IEnumerable<IOptionGroup> Values => _groups.Values;

        public int Count => 0;

        public IOptionValue this[int index] => throw new IndexOutOfRangeException();

        public IOptionGroup this[string key] => _groups[key];

        public CodeOptions(OptionSet options)
        {
            if (options == null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }

            CSharpFormattingOptions = new CSharpCodeFormattingOptions(options);
            _groups.Add(nameof(CSharpFormattingOptions), CSharpFormattingOptions);

            VisualBasicFormatting = new VisualBasicFormattingOptions(options);
            _groups.Add(nameof(VisualBasicFormatting), VisualBasicFormatting);

            CSharpCodeStyle = new CSharpCodeStyleOptions(options);
            _groups.Add(nameof(CSharpCodeStyle), CSharpCodeStyle);

        }

        public bool AreAllOptionsDefault() => true;

        public IEnumerable<(string groupName, IOptionGroup group)> GetSubGroups()
            => _groups.Select(kvp => (kvp.Key, kvp.Value));

        public bool ContainsKey(string key) => _groups.ContainsKey(key);

        public bool TryGetValue(string key, out IOptionGroup value)
            => _groups.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<string, IOptionGroup>> GetEnumerator()
            => _groups.GetEnumerator();

        IEnumerator<IOptionValue> IEnumerable<IOptionValue>.GetEnumerator()
            => Enumerable.Empty<IOptionValue>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
             => Enumerable.Empty<object>().GetEnumerator();
    }
}
