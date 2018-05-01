// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions
    {
        public partial class CSharpCodeFormattingOptions
        {
            public class IndentOptions : ChildOptionGroup, IOptionGroup
            {
                public OptionValue<bool> IndentBraces { get; }
                public OptionValue<bool> IndentBlock { get; }
                public OptionValue<bool>IndentSwitchSection { get; }
                public OptionValue<bool> IndentSwitchCaseSection { get; }
                public OptionValue<bool> IndentSwitchCaseSectionWhenBlock { get; }
                public OptionValue<LabelPositionOptions> LabelPositioning { get; }

                public IndentOptions(OptionSet options)
                {
                    IndentBraces = options.AsOptionValue(CSharpFormattingOptions.IndentBraces);
                    _options.Add(IndentBraces);

                    IndentBlock = options.AsOptionValue(CSharpFormattingOptions.IndentBlock);
                    _options.Add(IndentBlock);

                    IndentSwitchSection = options.AsOptionValue(CSharpFormattingOptions.IndentSwitchSection);
                    _options.Add(IndentSwitchSection);

                    IndentSwitchCaseSection = options.AsOptionValue(CSharpFormattingOptions.IndentSwitchCaseSection);
                    _options.Add(IndentSwitchCaseSection);

                    IndentSwitchCaseSectionWhenBlock = options.AsOptionValue(CSharpFormattingOptions.IndentSwitchCaseSectionWhenBlock);
                    _options.Add(IndentSwitchCaseSectionWhenBlock);

                    LabelPositioning = options.AsOptionValue(CSharpFormattingOptions.LabelPositioning);
                    _options.Add(LabelPositioning);
                }
            }
        }
    }
}
