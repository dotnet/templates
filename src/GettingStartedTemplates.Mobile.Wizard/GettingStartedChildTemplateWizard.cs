// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

public partial class GettingStartedChildTemplateWizard
{
    public virtual void OnProjectFinishedGenerating(Project project) { }

    private void OnRunStarted(DTE dTE, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
    {
        // Add the root project name to the projects replacement dictionary
        if (GettingStartedRootTemplateWizard.GlobalDictionary.TryGetValue("$saferootprojectname$", out var safeRootProjectName))
        {
            replacementsDictionary.Add("$saferootprojectname$", safeRootProjectName);
        }

        if (GettingStartedRootTemplateWizard.GlobalDictionary.TryGetValue("$saferootidentifiername$", out var saferootidentifiername))
        {
            replacementsDictionary.Add("$saferootidentifiername$", saferootidentifiername);
        }
    }
}
