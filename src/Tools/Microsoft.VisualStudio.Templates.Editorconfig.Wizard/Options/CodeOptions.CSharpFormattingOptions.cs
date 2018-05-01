// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using static Microsoft.VisualStudio.Templates.Editorconfig.Wizard.CodeOptions.CSharpCodeFormattingOptions;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions
    {
        public partial class CSharpCodeFormattingOptions : IOptionGroup
        {
            private IOptionValue[] _options = new IOptionValue[5];
            private Dictionary<string, IOptionGroup> _groups = new Dictionary<string, IOptionGroup>();

            public OptionValue<bool> UseTabs { get; }
            public OptionValue<int> TabSize { get; }
            public OptionValue<int> IndentationSize { get; }
            public OptionValue<FormattingOptions.IndentStyle> SmartIndent { get; }
            public OptionValue<string> NewLineValue { get; }
            public SpacingOptions Spacing { get; }
            public IndentOptions Indent { get; }
            public NewLineOptions NewLines { get; }

            public IEnumerable<string> Keys => _groups.Keys;

            public IEnumerable<IOptionGroup> Values => _groups.Values;

            public int Count => _options.Length;

            public IOptionValue this[int index] => _options[index];

            public IOptionGroup this[string key] => _groups[key];

            public CSharpCodeFormattingOptions(OptionSet options)
            {

                UseTabs = options.AsOptionValue(FormattingOptions.UseTabs, LanguageNames.CSharp);
                _options[0] = UseTabs;

                TabSize = options.AsOptionValue(FormattingOptions.TabSize, LanguageNames.CSharp);
                _options[1] = TabSize;

                IndentationSize = options.AsOptionValue(FormattingOptions.IndentationSize, LanguageNames.CSharp);
                _options[2] = IndentationSize;

                SmartIndent = options.AsOptionValue(FormattingOptions.SmartIndent, LanguageNames.CSharp);
                _options[3] = SmartIndent;

                NewLineValue = options.AsOptionValue(FormattingOptions.NewLine, LanguageNames.CSharp);
                _options[4] = NewLineValue;


                Spacing = new SpacingOptions(options);
                Indent = new IndentOptions(options);
                NewLines = new NewLineOptions(options);
                _groups.Add(nameof(Spacing), Spacing);
                _groups.Add(nameof(Indent), Indent);
                _groups.Add(nameof(NewLines), NewLines);
            }

            public bool AreAllOptionsDefault() => _options.All(x => x.IsDefault);

            public IEnumerable<(string groupName, IOptionGroup group)> GetSubGroups() => _groups.Select(kvp => (kvp.Key, kvp.Value));

            public bool ContainsKey(string key) => _groups.ContainsKey(key);

            public bool TryGetValue(string key, out IOptionGroup value) => _groups.TryGetValue(key, out value);

            public IEnumerator<KeyValuePair<string, IOptionGroup>> GetEnumerator() => _groups.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _options.GetEnumerator();

            IEnumerator<IOptionValue> IEnumerable<IOptionValue>.GetEnumerator() => _options.AsEnumerable().GetEnumerator();
        }
    }
}
