// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

public partial class GettingStartedChildTemplateWizard : IWizard
{
    public void BeforeOpeningFile(ProjectItem projectItem) { }
    public void ProjectFinishedGenerating(Project project)
        => OnProjectFinishedGenerating(project);

    public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }

    public void RunFinished() { }

    public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        => OnRunStarted(automationObject as DTE, replacementsDictionary, runKind, customParams);

    public bool ShouldAddProjectItem(string filePath) => true;
}
