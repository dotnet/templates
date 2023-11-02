// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using EnvDTE80;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Shell;
using System;
using System.Reflection;
using static Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Logging.Logger;
using Microsoft.CodeAnalysis.ExternalAccess.EditorConfigGenerator.Api;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Generator;

public class RoslynEditorConfigFileGenerator
{
    public RoslynEditorConfigFileGenerator(DTE2 dte)
    {
        try
        {
            _serviceProvider = new ServiceProvider(dte as OLE.Interop.IServiceProvider);
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable to create rolsyn editorconfig generator");
            throw;
        }

    }

    private readonly ServiceProvider _serviceProvider;

    public string? Generate(string language)
    {
        try
        {
            var editorConfigGenerator = _serviceProvider.GetService(typeof(IEditorConfigGenerator)) as IEditorConfigGenerator;
            return editorConfigGenerator?.Generate(language);
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable call roslyn editorconfig generator");
            return null;
        }
    }
}
