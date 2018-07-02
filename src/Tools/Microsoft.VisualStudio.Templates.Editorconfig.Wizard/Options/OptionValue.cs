using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public class OptionValue<T> : IOptionValue
    {
        public bool IsDefault { get; }

        public object Value { get; }

        public Type Type { get; }

        public string Name { get; }

        public T GetValue() => (T)Value;

        public OptionValue(T value, string name, bool isDefault)
        {
            Value = value;
            Name = name;
            Type = typeof(T);
            IsDefault = isDefault;
        }
    }
}
