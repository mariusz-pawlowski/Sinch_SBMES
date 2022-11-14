using FluentValidation;
using Microsoft.VisualBasic;
using SBMES.Application.Extenstions;
using System.Text;

namespace SBMES.Application.Validators
{
    internal class HeaderValidator : AbstractValidator<KeyValuePair<string, string>>
    {
        internal HeaderValidator()
        {
            // The headers are name-value pairs, where both names and values are ASCII - encoded strings.
            RuleFor(kv => kv.Key)
                .Must(k => k.IsASCII())
                ;

            RuleFor(kv => kv.Value)
                .Must(v => v.IsASCII())
                ;
            //Header names and values are limited to 1023 bytes (independently).
            RuleFor(kv => kv.Key)
                .Must(k => k.GetBytes().Length <= 1023)
                ;
            RuleFor(kv => kv.Value)
                .Must(v => v.GetBytes().Length <= 1023)
                ;
        }
    }
}
