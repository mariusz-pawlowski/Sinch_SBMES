using FluentValidation;
using SBMES.Application.Models;

namespace SBMES.Application.Validators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            //A message can have max 63 headers
            RuleFor(m => m.Headers.Count).LessThanOrEqualTo(63);

            // Detailed validation of headertd
            RuleForEach(m => m.Headers)
                .SetValidator(new HeaderValidator())
                ;

            //The message payload is limited to 256 KiB.
            RuleFor(m => m.Payload)
                .Must(p => Buffer.ByteLength(p) <= 1024*256);
        }
    }
}
