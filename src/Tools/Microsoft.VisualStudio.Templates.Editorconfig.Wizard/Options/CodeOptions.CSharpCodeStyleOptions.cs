// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeStyle;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions
    {
        public class CSharpCodeStyleOptions : ChildOptionGroup, IOptionGroup
        {
            public OptionValue<CodeStyleOption<bool>> PreferIntrinsicPredefinedTypeKeywordInDeclaration { get; }
            public OptionValue<CodeStyleOption<bool>> PreferIntrinsicPredefinedTypeKeywordInMemberAccess { get; }
            public OptionValue<CodeStyleOption<bool>> QualifyEventAccess { get; }
            public OptionValue<CodeStyleOption<bool>> QualifyFieldAccess { get; }
            public OptionValue<CodeStyleOption<bool>> QualifyMethodAccess { get; }
            public OptionValue<CodeStyleOption<bool>> QualifyPropertyAccess { get; }

            public CSharpCodeStyleOptions(OptionSet options)
            {
                PreferIntrinsicPredefinedTypeKeywordInDeclaration = options.AsOptionValue(CodeStyleOptions.PreferIntrinsicPredefinedTypeKeywordInDeclaration, LanguageNames.CSharp);
                _options.Add(PreferIntrinsicPredefinedTypeKeywordInDeclaration);

                PreferIntrinsicPredefinedTypeKeywordInMemberAccess = options.AsOptionValue(CodeStyleOptions.PreferIntrinsicPredefinedTypeKeywordInMemberAccess, LanguageNames.CSharp);
                _options.Add(PreferIntrinsicPredefinedTypeKeywordInMemberAccess);

                QualifyEventAccess = options.AsOptionValue(CodeStyleOptions.QualifyEventAccess, LanguageNames.CSharp);
                _options.Add(QualifyEventAccess);

                QualifyFieldAccess = options.AsOptionValue(CodeStyleOptions.QualifyFieldAccess, LanguageNames.CSharp);
                _options.Add(QualifyFieldAccess);

                QualifyMethodAccess = options.AsOptionValue(CodeStyleOptions.QualifyMethodAccess, LanguageNames.CSharp);
                _options.Add(QualifyMethodAccess);

                QualifyPropertyAccess = options.AsOptionValue(CodeStyleOptions.QualifyPropertyAccess, LanguageNames.CSharp);
                _options.Add(QualifyPropertyAccess);
            }
        }
    }
}
