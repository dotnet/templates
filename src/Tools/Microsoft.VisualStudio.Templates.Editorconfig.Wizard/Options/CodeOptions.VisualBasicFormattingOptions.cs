// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions
    {
        public class VisualBasicFormattingOptions : ChildOptionGroup, IOptionGroup
        {
            public OptionValue<bool> UseTabs { get; }
            public OptionValue<int> TabSize { get; }
            public OptionValue<int> IndentationSize { get; }
            public OptionValue<FormattingOptions.IndentStyle> SmartIndent { get; }
            public OptionValue<string> NewLineValue { get; }

            public VisualBasicFormattingOptions(OptionSet options)
            {
                UseTabs = options.AsOptionValue(FormattingOptions.UseTabs, LanguageNames.VisualBasic);
                _options.Add(UseTabs);

                TabSize = options.AsOptionValue(FormattingOptions.TabSize, LanguageNames.VisualBasic);
                _options.Add(TabSize);

                IndentationSize = options.AsOptionValue(FormattingOptions.IndentationSize, LanguageNames.VisualBasic);
                _options.Add(IndentationSize);

                SmartIndent = options.AsOptionValue(FormattingOptions.SmartIndent, LanguageNames.VisualBasic);
                _options.Add(SmartIndent);

                NewLineValue = options.AsOptionValue(FormattingOptions.NewLine, LanguageNames.VisualBasic);
                _options.Add(NewLineValue);
            }
        }
    }
}
