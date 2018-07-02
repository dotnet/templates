// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard
{
    public interface IOptionValue
    {
        bool IsDefault { get; }
        object Value { get; }
        string Name { get; }
        Type Type { get; }
    }
}
