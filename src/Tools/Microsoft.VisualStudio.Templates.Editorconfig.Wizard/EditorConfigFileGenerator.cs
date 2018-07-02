// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.IO;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Templates.Editorconfig.Wizard;
using System.Windows;
using VSServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options;
using System.Text;

namespace Templates.EditorConfig.FileGenerator
{
    internal class EditorConfigFileGenerator
    {
        private DTE2 _dte;

        public EditorConfigFileGenerator(DTE2 dte)
        {
            this._dte = dte;
        }

        internal (bool success, string fileName) TryGenerateFile(bool isDotnet)
        {
            CodeOptions codeOptions = null;
            if (_dte is VSServiceProvider sp)
            {
                using (var serviceProvider = new ServiceProvider(sp))
                {
                    var options = serviceProvider.GetService<VisualStudioWorkspace>()?.Options;
                    if (options != null)
                    {
                        codeOptions = new CodeOptions(options);
                    }
                }
            }

            var (success, path, isAtSolutionLevel, selectedItem) = TryGetSelectedItemAndPath();
            if (!success)
            {
                return (false, null);
            }

            var createFileResult = TryCreateFile(path, isDotnet, isAtSolutionLevel, codeOptions);
            if (!createFileResult.success)
            {
                return (false, null);
            }

            var fileHierarchyResult = TryAddFileToHierarchy(selectedItem, createFileResult.fileName);
            if (fileHierarchyResult.success)
            {
                return (fileHierarchyResult.projectItem != null, createFileResult.fileName);
            }

            return (false, null);
        }

        private (bool success, string path, bool isAtSolutionLevel, object selectedItem) TryGetSelectedItemAndPath()
        {
            var items = (Array)_dte.ToolWindows.SolutionExplorer.SelectedItems;
            object selectedItem = null;

            foreach (UIHierarchyItem selItem in items)
            {
                selectedItem = selItem.Object;
                if (selItem.Object is ProjectItem item && item.Properties != null)
                {
                    return (selectedItem != null, item.Properties.Item("FullPath").Value.ToString(), false, selectedItem);
                }
                else if (selItem.Object is Project proj && proj.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}") // solution folder
                {
                    (bool success, string rootFolder) = proj.TryGetRootFolder(_dte.Solution.FullName);
                    return (success && selectedItem != null, rootFolder, true, selectedItem);
                }
            }

            return (selectedItem != null, Path.GetDirectoryName(_dte.Solution.FullName), false, selectedItem);
        }

        private (bool success, string fileName) TryCreateFile(string projectPath, bool isDotnet, bool isAtSolutionLevel, CodeOptions options)
        {
            try
            {
                string fileName = Path.Combine(projectPath, TemplateConstants.FileName);
                if (File.Exists(fileName))
                {

                    MessageBox.Show(WizardResource.AlreadyExists, ".editorconfig item template", MessageBoxButton.OK, MessageBoxImage.Information);
                    return (false, null);
                }
                else
                {
                    if (TryGetFileFromOptions(options, isAtSolutionLevel, out var file))
                    {
                        WriteFile(fileName, file);
                        return (true, fileName);
                    }

                    WriteFile(fileName, isDotnet, isAtSolutionLevel);
                    return (true, fileName);
                }
            }
            catch (Exception)
            {
                return (false, null);
            }
        }

        private bool TryGetFileFromOptions(CodeOptions options, bool isAtSolutionLevel, out string file)
        {
            if (options == null)
            {
                file = null;
                return false;
            }

            var builder = new StringBuilder();
            builder.AppendLine($@"# {WizardResource.MoreAbout} https://aka.ms/editorconfigdocs");
            if (isAtSolutionLevel)
            {
                builder.AppendLine(@"root = true");
            }
            builder.AppendLine();

            // All Files
            builder.AppendLine(@"# All files");
            builder.AppendLine(@"[*]");
            builder.AppendLine(@"indent_style = space");

            builder.AppendLine();

            // Code Files
            if (options.VisualBasicFormatting.IndentationSize.IsDefault &&
                options.VisualBasicFormatting.NewLineValue.IsDefault)
            {
                builder.AppendLine(@"# Code files");
                builder.AppendLine(@"[*.{{cs,csx,vb,vbx}}]");
                builder.AppendLine($@"indent_size = {options.CSharpFormattingOptions.IndentationSize}");
                builder.AppendLine($@"end_of_line = {options.CSharpFormattingOptions.NewLineValue}");
            }
            else
            {
                builder.AppendLine(@"# C# files");
                builder.AppendLine(@"[*.{{cs,csx]");
                builder.AppendLine($@"indent_size = {options.CSharpFormattingOptions.IndentationSize}");
                builder.AppendLine($@"end_of_line = {options.CSharpFormattingOptions.NewLineValue}");
                builder.AppendLine();

                builder.AppendLine(@"# Visual Basic files");
                builder.AppendLine(@"[*.{{vb,vbx]");
                builder.AppendLine($@"indent_size = {options.VisualBasicFormatting.IndentationSize}");
                builder.AppendLine($@"indent_size = {options.VisualBasicFormatting.NewLineValue}");
            }
            builder.AppendLine();

            // XML Files
            builder.AppendLine(@"# Xml files");
            builder.AppendLine(@"[*.xml]");
            builder.AppendLine(@"indent_size = 2");
            builder.AppendLine();

            // Dotnet code style
            builder.AppendLine(@"# Dotnet code style");
            builder.AppendLine(@"[*.{{cs,vb}}]");
            builder.AppendLine(@"# Organize usings");
            builder.AppendLine(@"dotnet_sort_system_directives_first = true");
            builder.AppendLine();
            builder.AppendLine(@"# this. qualification");
            var qualifyFieldAccess = options.CSharpCodeStyle.QualifyFieldAccess.GetValue();
            builder.AppendLine(@$"dotnet_style_qualification_for_field = {qualifyFieldAccess.Value}:{qualifyFieldAccess.Notification.AsString()}");
            var qualifyPropertyAccess = options.CSharpCodeStyle.QualifyPropertyAccess.GetValue();
            builder.AppendLine(@$"dotnet_style_qualification_for_property = {qualifyPropertyAccess.Value}:{qualifyPropertyAccess.Notification.AsString()}");
            var qualifyMethodAccess = options.CSharpCodeStyle.QualifyMethodAccess.GetValue();
            builder.AppendLine(@$"dotnet_style_qualification_for_method = {qualifyMethodAccess.Value}:{qualifyMethodAccess.Notification.AsString()}");
            var qualifyEventAccess = options.CSharpCodeStyle.QualifyEventAccess.GetValue();
            builder.AppendLine(@$"dotnet_style_qualification_for_event = {qualifyEventAccess.Value}:{qualifyEventAccess.Notification.AsString()}");
            builder.AppendLine();
            builder.AppendLine(@"# Language keywords use on BCL types");
            var preferIntrinsicPredefinedTypeKeywordInDeclaration = options.CSharpCodeStyle.PreferIntrinsicPredefinedTypeKeywordInDeclaration.GetValue();
            builder.AppendLine(@$"dotnet_style_predefined_type_for_locals_parameters_members = {preferIntrinsicPredefinedTypeKeywordInDeclaration.Value}:{preferIntrinsicPredefinedTypeKeywordInDeclaration.Notification.AsString()}");
            var preferIntrinsicPredefinedTypeKeywordInMemberAccess = options.CSharpCodeStyle.PreferIntrinsicPredefinedTypeKeywordInMemberAccess.GetValue();
            builder.AppendLine(@$"dotnet_style_predefined_type_for_member_access = {preferIntrinsicPredefinedTypeKeywordInMemberAccess.Value}:{preferIntrinsicPredefinedTypeKeywordInMemberAccess.Notification.AsString()}");
            builder.AppendLine();
            builder.AppendLine(@"# Naming conventions");
            builder.AppendLine(@"dotnet_naming_style.pascal_case_style.capitalization        = pascal_case");
            builder.AppendLine(@"# Classes, structs, methods, enums, events, properties, namespaces, delegates must be PascalCase");
            builder.AppendLine(@"dotnet_naming_rule.general_naming.severity                  = suggestion");
            builder.AppendLine(@"dotnet_naming_rule.general_naming.symbols                   = general");
            builder.AppendLine(@"dotnet_naming_rule.general_naming.style                     = pascal_case_style");
            builder.AppendLine(@"dotnet_naming_symbols.general.applicable_kinds              = class,struct,enum,property,method,event,namespace,delegate");
            builder.AppendLine(@"dotnet_naming_symbols.general.applicable_accessibilities    = *");
            builder.AppendLine();

            file = builder.ToString();
            return true;
        }

        private void WriteFile(string fileName, bool isDotnet, bool isAtSolutionLevel)
        {
            if (isAtSolutionLevel)
            {
                if (isDotnet)
                {
                    WriteFile(fileName, TemplateConstants.DotNetFileContentIsRoot);
                }
                else
                {
                    WriteFile(fileName, TemplateConstants.DefaultFileContentIsRoot);
                }
            }
            else
            {
                if (isDotnet)
                {
                    WriteFile(fileName, TemplateConstants.DotNetFileContent);
                }
                else
                {
                    WriteFile(fileName, TemplateConstants.DefaultFileContent);
                }
            }
        }

        private void WriteFile(string fileName, string fileContests)
        {
            File.WriteAllText(fileName, fileContests);
        }

        private (bool success, ProjectItem projectItem) TryAddFileToHierarchy(object item, string fileName)
        {
            if (item is Project proj)
            {
                // Added to project
                return proj.TryAddFileToProject(_dte, fileName, "None");
            }
            else if (item is ProjectItem projItem && projItem.ContainingProject != null)
            {
                // Added to folder in project
                return projItem.ContainingProject.TryAddFileToProject(_dte, fileName, "None");
            }
            else if (item is Solution2 solution)
            {
                // Added to solution
                return solution.TryAddFileToSolution(_dte, fileName);
            }

            return (false, null);
        }

        internal void OpenFile(string fileName)
        {
            if (_dte is VSServiceProvider sp)
            {
                using (var serviceProvider = new ServiceProvider(sp))
                {
                    VsShellUtilities.OpenDocument(serviceProvider, fileName);
                    Command command = _dte.Commands.Item("SolutionExplorer.SyncWithActiveDocument");
                    if (command.IsAvailable)
                    {
                        _dte.Commands.Raise(command.Guid, command.ID, null, null);
                    }
                    _dte.ActiveDocument.Activate();
                }
            }
        }
    }
}
