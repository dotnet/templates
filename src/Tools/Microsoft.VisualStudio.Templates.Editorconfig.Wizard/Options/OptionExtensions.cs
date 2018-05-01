using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.VisualStudio.Templates.Editorconfig.Wizard.Options
{
    public static class OptionExtensions
    {
        public static OptionValue<T> AsOptionValue<T>(this OptionSet options, Option<T> option)
        {
            var value = options.GetOption(option);
            return new OptionValue<T>(value, option.Name, value.Equals(option.DefaultValue));
        }

        public static OptionValue<T> AsOptionValue<T>(this OptionSet options, PerLanguageOption<T> option, string language)
        {
            var value = options.GetOption(option, language);
            return new OptionValue<T>(value, option.Name, value.Equals(option.DefaultValue));
        }

        public static IEnumerable<IOptionValue> GetAllOptions(this IOptionGroup group)
        {
            var values = group as IReadOnlyList<IOptionValue>;
            foreach (var value in values)
            {
                yield return value;
            }

            foreach (var (groupName, subGroup) in group.GetSubGroups())
            {
                foreach (var value in GetAllOptions(subGroup))
                {
                    yield return value;
                }
            }
        }
    }
}
