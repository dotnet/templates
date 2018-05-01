// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions
    {
        public partial class CSharpCodeFormattingOptions
        {
            public class NewLineOptions : ChildOptionGroup, IOptionGroup
            {
                public OptionValue<bool> NewLinesForBracesInTypes { get; }
                public OptionValue<bool> NewLinesForBracesInMethods { get; }
                public OptionValue<bool> NewLinesForBracesInProperties { get; }
                public OptionValue<bool> NewLinesForBracesInAccessors { get; }
                public OptionValue<bool> NewLinesForBracesInAnonymousMethods { get; }
                public OptionValue<bool> NewLinesForBracesInControlBlocks { get; }
                public OptionValue<bool> NewLinesForBracesInAnonymousTypes { get; }
                public OptionValue<bool> NewLinesForBracesInObjectCollectionArrayInitializers { get; }
                public OptionValue<bool> NewLinesForBracesInLambdaExpressionBody { get; }
                public OptionValue<bool> NewLineForElse { get; }
                public OptionValue<bool> NewLineForCatch { get; }
                public OptionValue<bool> NewLineForFinally { get; }
                public OptionValue<bool> NewLineForMembersInObjectInit { get; }
                public OptionValue<bool> NewLineForMembersInAnonymousTypes { get; }
                public OptionValue<bool> NewLineForClausesInQuery { get; }
                public OptionValue<bool> WrappingPreserveSingleLine { get; }
                public OptionValue<bool> WrappingKeepStatementsOnSingleLine { get; }

                public NewLineOptions(OptionSet options)
                {
                    NewLinesForBracesInTypes = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInTypes);
                    _options.Add(NewLinesForBracesInTypes);

                    NewLinesForBracesInMethods = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInMethods);
                    _options.Add(NewLinesForBracesInMethods);

                    NewLinesForBracesInProperties = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInProperties);
                    _options.Add(NewLinesForBracesInProperties);

                    NewLinesForBracesInAccessors = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInAccessors);
                    _options.Add(NewLinesForBracesInAccessors);

                    NewLinesForBracesInAnonymousMethods = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInAnonymousMethods);
                    _options.Add(NewLinesForBracesInAnonymousMethods);

                    NewLinesForBracesInControlBlocks = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInControlBlocks);
                    _options.Add(NewLinesForBracesInControlBlocks);

                    NewLinesForBracesInAnonymousTypes = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInAnonymousTypes);
                    _options.Add(NewLinesForBracesInAnonymousTypes);

                    NewLinesForBracesInObjectCollectionArrayInitializers = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInObjectCollectionArrayInitializers);
                    _options.Add(NewLinesForBracesInObjectCollectionArrayInitializers);

                    NewLinesForBracesInLambdaExpressionBody = options.AsOptionValue(CSharpFormattingOptions.NewLinesForBracesInLambdaExpressionBody);
                    _options.Add(NewLinesForBracesInLambdaExpressionBody);

                    NewLineForElse = options.AsOptionValue(CSharpFormattingOptions.NewLineForElse);
                    _options.Add(NewLineForElse);

                    NewLineForCatch = options.AsOptionValue(CSharpFormattingOptions.NewLineForCatch);
                    _options.Add(NewLineForCatch);

                    NewLineForFinally = options.AsOptionValue(CSharpFormattingOptions.NewLineForFinally);
                    _options.Add(NewLineForFinally);

                    NewLineForMembersInObjectInit = options.AsOptionValue(CSharpFormattingOptions.NewLineForMembersInObjectInit);
                    _options.Add(NewLineForMembersInObjectInit);

                    NewLineForMembersInAnonymousTypes = options.AsOptionValue(CSharpFormattingOptions.NewLineForMembersInAnonymousTypes);
                    _options.Add(NewLineForMembersInAnonymousTypes);

                    NewLineForClausesInQuery = options.AsOptionValue(CSharpFormattingOptions.NewLineForClausesInQuery);
                    _options.Add(NewLineForClausesInQuery);

                    WrappingPreserveSingleLine = options.AsOptionValue(CSharpFormattingOptions.WrappingPreserveSingleLine);
                    _options.Add(WrappingPreserveSingleLine);

                    WrappingKeepStatementsOnSingleLine = options.AsOptionValue(CSharpFormattingOptions.WrappingKeepStatementsOnSingleLine);
                    _options.Add(WrappingKeepStatementsOnSingleLine);

                }
            }
        }
    }
}
