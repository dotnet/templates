// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Options;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public partial class CodeOptions
    {
        public partial class CSharpCodeFormattingOptions
        {
            public class SpacingOptions : ChildOptionGroup, IOptionGroup
            {
                public OptionValue<bool> SpacingAfterMethodDeclarationName { get; }
                public OptionValue<bool> SpaceWithinMethodDeclarationParenthesis { get; }
                public OptionValue<bool> SpaceBetweenEmptyMethodDeclarationParentheses { get; }
                public OptionValue<bool> SpaceAfterMethodCallName { get; }
                public OptionValue<bool> SpaceWithinMethodCallParentheses { get; }
                public OptionValue<bool> SpaceBetweenEmptyMethodCallParentheses { get; }
                public OptionValue<bool> SpaceAfterControlFlowStatementKeyword { get; }
                public OptionValue<bool> SpaceWithinExpressionParentheses { get; }
                public OptionValue<bool> SpaceWithinCastParentheses { get; }
                public OptionValue<bool> SpaceWithinOtherParentheses { get; }
                public OptionValue<bool> SpaceAfterCast { get; }
                public OptionValue<bool> SpacesIgnoreAroundVariableDeclaration { get; }
                public OptionValue<bool> SpaceBeforeOpenSquareBracket { get; }
                public OptionValue<bool> SpaceBetweenEmptySquareBrackets { get; }
                public OptionValue<bool> SpaceWithinSquareBrackets { get; }
                public OptionValue<bool> SpaceAfterColonInBaseTypeDeclaration { get; }
                public OptionValue<bool> SpaceAfterComma { get; }
                public OptionValue<bool> SpaceAfterDot { get; }
                public OptionValue<bool> SpaceAfterSemicolonsInForStatement { get; }
                public OptionValue<bool> SpaceBeforeColonInBaseTypeDeclaration { get; }
                public OptionValue<bool> SpaceBeforeComma { get; }
                public OptionValue<bool> SpaceBeforeDot { get; }
                public OptionValue<bool> SpaceBeforeSemicolonsInForStatement { get; }
                public OptionValue<BinaryOperatorSpacingOptions> SpacingAroundBinaryOperator { get; }

                public SpacingOptions(OptionSet options)
                {
                    SpacingAfterMethodDeclarationName = options.AsOptionValue(CSharpFormattingOptions.SpacingAfterMethodDeclarationName);
                    _options.Add(SpacingAfterMethodDeclarationName);

                    SpaceWithinMethodDeclarationParenthesis = options.AsOptionValue(CSharpFormattingOptions.SpaceWithinMethodDeclarationParenthesis);
                    _options.Add(SpaceWithinMethodDeclarationParenthesis);

                    SpaceBetweenEmptyMethodDeclarationParentheses = options.AsOptionValue(CSharpFormattingOptions.SpaceBetweenEmptyMethodDeclarationParentheses);
                    _options.Add(SpaceBetweenEmptyMethodDeclarationParentheses);

                    SpaceAfterMethodCallName = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterMethodCallName);
                    _options.Add(SpaceAfterMethodCallName);

                    SpaceWithinMethodCallParentheses = options.AsOptionValue(CSharpFormattingOptions.SpaceWithinMethodCallParentheses);
                    _options.Add(SpaceWithinMethodCallParentheses);

                    SpaceBetweenEmptyMethodCallParentheses = options.AsOptionValue(CSharpFormattingOptions.SpaceBetweenEmptyMethodCallParentheses);
                    _options.Add(SpaceBetweenEmptyMethodCallParentheses);

                    SpaceAfterControlFlowStatementKeyword = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterControlFlowStatementKeyword);
                    _options.Add(SpaceAfterControlFlowStatementKeyword);

                    SpaceWithinExpressionParentheses = options.AsOptionValue(CSharpFormattingOptions.SpaceWithinExpressionParentheses);
                    _options.Add(SpaceWithinExpressionParentheses);

                    SpaceWithinCastParentheses = options.AsOptionValue(CSharpFormattingOptions.SpaceWithinCastParentheses);
                    _options.Add(SpaceWithinCastParentheses);

                    SpaceWithinOtherParentheses = options.AsOptionValue(CSharpFormattingOptions.SpaceWithinOtherParentheses);
                    _options.Add(SpaceWithinOtherParentheses);

                    SpaceAfterCast = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterCast);
                    _options.Add(SpaceAfterCast);

                    SpacesIgnoreAroundVariableDeclaration = options.AsOptionValue(CSharpFormattingOptions.SpacesIgnoreAroundVariableDeclaration);
                    _options.Add(SpacesIgnoreAroundVariableDeclaration);

                    SpaceBeforeOpenSquareBracket = options.AsOptionValue(CSharpFormattingOptions.SpaceBeforeOpenSquareBracket);
                    _options.Add(SpaceBeforeOpenSquareBracket);

                    SpaceBetweenEmptySquareBrackets = options.AsOptionValue(CSharpFormattingOptions.SpaceBetweenEmptySquareBrackets);
                    _options.Add(SpaceBetweenEmptySquareBrackets);

                    SpaceWithinSquareBrackets = options.AsOptionValue(CSharpFormattingOptions.SpaceWithinSquareBrackets);
                    _options.Add(SpaceWithinSquareBrackets);

                    SpaceAfterColonInBaseTypeDeclaration = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterColonInBaseTypeDeclaration);
                    _options.Add(SpaceAfterColonInBaseTypeDeclaration);

                    SpaceAfterComma = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterComma);
                    _options.Add(SpaceAfterComma);

                    SpaceAfterDot = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterDot);
                    _options.Add(SpaceAfterDot);

                    SpaceAfterSemicolonsInForStatement = options.AsOptionValue(CSharpFormattingOptions.SpaceAfterSemicolonsInForStatement);
                    _options.Add(SpaceAfterSemicolonsInForStatement);

                    SpaceBeforeColonInBaseTypeDeclaration = options.AsOptionValue(CSharpFormattingOptions.SpaceBeforeColonInBaseTypeDeclaration);
                    _options.Add(SpaceBeforeColonInBaseTypeDeclaration);

                    SpaceBeforeComma = options.AsOptionValue(CSharpFormattingOptions.SpaceBeforeComma);
                    _options.Add(SpaceBeforeComma);

                    SpaceBeforeDot = options.AsOptionValue(CSharpFormattingOptions.SpaceBeforeDot);
                    _options.Add(SpaceBeforeDot);

                    SpaceBeforeSemicolonsInForStatement = options.AsOptionValue(CSharpFormattingOptions.SpaceBeforeSemicolonsInForStatement);
                    _options.Add(SpaceBeforeSemicolonsInForStatement);

                    SpacingAroundBinaryOperator = options.AsOptionValue(CSharpFormattingOptions.SpacingAroundBinaryOperator);
                    _options.Add(SpacingAroundBinaryOperator);
                }
            }
        }
    }
}
