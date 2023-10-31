﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.RpcContracts.FileSystem;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Logging.Kinds;
using Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Logging.Messages;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using static Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Logging.Logger;
// using Microsoft.CodeAnalysis.ExternalAccess.EditorConfigGenerator;
using Microsoft.CodeAnalysis.Options.EditorConfig;
using Microsoft.VisualStudio.OLE.Interop;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Generator;

public class RoslynEditorConfigFileGenerator
{
    private const BindingFlags PublicBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    private const BindingFlags NonPublicBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

    public RoslynEditorConfigFileGenerator(DTE2 dte)
    {
        try
        {
            string? basePath = Path.GetDirectoryName(dte.FullName);
            string languageServiceSubPath = @"CommonExtensions\Microsoft\VBCSharp\LanguageServices";
            var baseDirectory = Path.Combine(basePath!, languageServiceSubPath);

            _microsoftVisualStudioLanguageServicesVisualBasicAssembly =
                new Lazy<Assembly>(() => Assembly.LoadFrom(Path.Combine(baseDirectory, "Microsoft.VisualStudio.LanguageServices.VisualBasic.dll")));

            _microsoftVisualStudioLanguageServicesCSharpAssembly =
                new Lazy<Assembly>(() => Assembly.LoadFrom(Path.Combine(baseDirectory, "Microsoft.VisualStudio.LanguageServices.CSharp.dll")));

            _serviceProvider = new ServiceProvider(dte as OLE.Interop.IServiceProvider);

            _generatorMethod = new Lazy<MethodInfo?>(() =>
            {
                var editorConfigFileGeneratorType = typeof(OptionSet).Assembly.GetType("Microsoft.CodeAnalysis.Options.EditorConfigFileGenerator");
                Type[] targetParams = new Type[]
                {
                typeof(ImmutableArray<(string feature, ImmutableArray<IOption> options)>),
                typeof(OptionSet),
                typeof(string)
                };

                return editorConfigFileGeneratorType?.GetMethod("Generate", PublicBindingFlags, binder: null, types: targetParams, modifiers: null);
            });
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable to create rolsyn editorconfig generator");
            throw;
        }

    }

    private readonly ServiceProvider _serviceProvider;
    private readonly Lazy<Assembly> _microsoftVisualStudioLanguageServicesVisualBasicAssembly;
    private readonly Lazy<Assembly> _microsoftVisualStudioLanguageServicesCSharpAssembly;
    private readonly Lazy<MethodInfo?> _generatorMethod;
    private static readonly Lazy<Type?> _ieditorConfigStorageLocation2Type =
        new(() => typeof(OptionSet).Assembly.GetType("Microsoft.CodeAnalysis.Options.IEditorConfigStorageLocation2"));

    private static readonly Lazy<MethodInfo?> _getEditorConfigStringMethod =
        new(() => typeof(OptionSet).Assembly.GetType("Microsoft.CodeAnalysis.Options.IEditorConfigStorageLocation2")?.GetMethod("GetEditorConfigString"));

    private Assembly VisualBasicLanguageServiceAssembly => _microsoftVisualStudioLanguageServicesVisualBasicAssembly.Value;
    private Assembly CSharpLanguageServiceAssembly => _microsoftVisualStudioLanguageServicesCSharpAssembly.Value;
    private static Type? IEditorConfigStorageLocation2Type => _ieditorConfigStorageLocation2Type.Value;

    private OptionSet? GetEditorConfigOptionSet()
    {
        try
        {
            // get IOptionService
            var componentModel = _serviceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
            var workspace = componentModel?.GetService<VisualStudioWorkspace>();

            // var editorService = (EditorConfigGeneratorWrapper)_serviceProvider.GetService(typeof(EditorConfigGeneratorWrapper));
            if (workspace is null)
            {
                return null;
            }

            var ioptionServiceType = typeof(OptionSet).Assembly.GetType("Microsoft.CodeAnalysis.Options.IOptionService");
            if (ioptionServiceType is null)
            {
                return null;
            }

            var getServiceGenericMethod = workspace.Services.GetType().GetMethod(nameof(HostWorkspaceServices.GetService));
            if (getServiceGenericMethod is null)
            {
                return null;
            }

            var getServiceMethod = getServiceGenericMethod.MakeGenericMethod(ioptionServiceType);
            var optionService = getServiceMethod.Invoke(workspace.Services, null);

            var getOptionsMethod = ioptionServiceType.GetMethod("GetOptions");
            if (getOptionsMethod is null)
            {
                return null;
            }

            return (OptionSet?)getOptionsMethod.Invoke(optionService, null);
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable to get OptionSet from roslyn");
            return null;
        }

    }

    private (bool success, ImmutableArray<(string feature, ImmutableArray<IOption> options)>) GetEditorConfigOptions(string language)
    {
        try
        {
            var codeStylePageType = GetCodePage(language);
            Type[] targetParams = new Type[] { };
            var getEditorConfigOptionsMethod = codeStylePageType?.GetMethod("GetEditorConfigOptions", NonPublicBindingFlags, binder: null, types: targetParams, modifiers: null);
            if (getEditorConfigOptionsMethod is null)
            {
                return (false, default);
            }

            return (true, (ImmutableArray<(string feature, ImmutableArray<IOption> options)>)getEditorConfigOptionsMethod.Invoke(null, null)!);
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable to get visual studio options from tools->options page");
            return (false, default(ImmutableArray<(string feature, ImmutableArray<IOption> options)>));
        }

        Type? GetCodePage(string language)
        {
            var languageName = language == LanguageNames.CSharp ? "CSharp" : "VisualBasic";
            var codePageTypeName = $"Microsoft.VisualStudio.LanguageServices.{languageName}.Options.Formatting.CodeStylePage";
            var langueServiceAssembly = GetLanguageServiceAssembly(language);
            return langueServiceAssembly.GetType(codePageTypeName);
        }

        Assembly GetLanguageServiceAssembly(string language)
        {
            return language switch
            {
                LanguageNames.CSharp => CSharpLanguageServiceAssembly,
                LanguageNames.VisualBasic => VisualBasicLanguageServiceAssembly,
                _ => throw new ArgumentException(nameof(language))
            };
        }
    }

    private string? Generate(ImmutableArray<(string feature, ImmutableArray<IOption> options)> groupedOptions, OptionSet optionSet, string language)
    {
        try
        {
            LogEvents(EventId.Options, new OptionsInfo(groupedOptions, optionSet));
            return (string?)_generatorMethod.Value?.Invoke(null, new object[] { groupedOptions, optionSet, language });
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable call roslyn editorconfig generator");
            return null;
        }
    }

    public string? Generate(string language)
    {
        var (success, options) = GetEditorConfigOptions(language);
        if (!success)
        {
            return null;
        }

        var optionSet = GetEditorConfigOptionSet();
        if (optionSet is null)
        {
            return null;
        }

        return Generate(options, optionSet, language);
    }

    public static bool CanGetEditorConfigString(IOption option)
    {
        foreach (var storageLocation in option.StorageLocations)
        {
            if (IEditorConfigStorageLocation2Type?.IsAssignableFrom(storageLocation.GetType()) == true)
            {
                return true;
            }
        }

        return false;
    }

    public static string? GetEditorConfigOptionString(IOption option, OptionSet optionSet)
    {
        string? editorconfigOptionString = null;
        foreach (var storageLocation in option.StorageLocations)
        {
            var storageType = storageLocation.GetType();
            if (IEditorConfigStorageLocation2Type?.IsAssignableFrom(storageType) == true)
            {
                var genericEditorConfigStorageLocationType = typeof(OptionSet).Assembly.GetType("Microsoft.CodeAnalysis.Options.EditorConfigStorageLocation`1");
                var genericArguments = storageType.GenericTypeArguments;
                if (genericEditorConfigStorageLocationType is null)
                {
                    continue;
                }

                var editorConfigStorageLocationType = genericEditorConfigStorageLocationType.MakeGenericType(genericArguments);
                editorconfigOptionString = (string?)editorConfigStorageLocationType.GetProperty("KeyName")?.GetValue(storageLocation, null);
                break;
            }
        }

        Assert(editorconfigOptionString is not null, "Unable call roslyn EditorConfigStorageLocation.KeyName method");
        return editorconfigOptionString;
    }

    public static string? GetEditorConfigValueString(IOption option, OptionSet optionSet)
    {
        string? editorconfigOptionString = null;
        foreach (var storageLocation in option.StorageLocations)
        {
            var storageType = storageLocation.GetType();
            if (IEditorConfigStorageLocation2Type?.IsAssignableFrom(storageType) == true)
            {
                var methodInfo = IEditorConfigStorageLocation2Type.GetMethod("GetEditorConfigStringValue");
                editorconfigOptionString = (string?) methodInfo?.Invoke(storageLocation, new[] { option.DefaultValue, optionSet } );
                break;
            }
        }

        Assert(editorconfigOptionString is not null, "Unable call roslyn EditorConfigStorageLocation.KeyName method");
        return editorconfigOptionString;
    }
}
