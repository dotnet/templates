// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using Microsoft.VisualStudio.Shell;
using System;
using Microsoft.VisualStudio.ComponentModelHost;
using static Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Logging.Logger;
using Microsoft.CodeAnalysis.ExternalAccess.EditorConfigGenerator.Api;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Generator;

public class RoslynEditorConfigFileGenerator
{
    public RoslynEditorConfigFileGenerator()
    {
        try
        {
            _componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable to create roslyn editorconfig generator");
            throw;
        }
    }

    private readonly IComponentModel _componentModel;

    public string? Generate(string language)
    {
        try
        {
            var editorConfigGenerator = _componentModel.GetService<IEditorConfigGenerator>();
            return editorConfigGenerator?.Generate(language);
        }
        catch (Exception ex)
        {
            LogException(ex, "Unable call roslyn editorconfig generator");
            return null;
        }
    }
}
