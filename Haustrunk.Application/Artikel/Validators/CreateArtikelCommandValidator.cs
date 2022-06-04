using FluentValidation;
using Haustrunk.Application.Artikel.Commands;

namespace Haustrunk.Application.Artikel.Validators
{
    public class CreateArtikelCommandValidator : AbstractValidator<CreateArtikelCommand>
    {
        public CreateArtikelCommandValidator()
        {
            RuleFor(a => a.Marke)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(a => a.Sorte)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
