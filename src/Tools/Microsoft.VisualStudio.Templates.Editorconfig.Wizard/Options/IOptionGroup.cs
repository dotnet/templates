// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard
{
    public interface IOptionGroup : IReadOnlyDictionary<string, IOptionGroup>, IReadOnlyList<IOptionValue>
    {
        bool AreAllOptionsDefault();

        IEnumerable<(string groupName, IOptionGroup group)> GetSubGroups();
    }
}
