using FluentValidation;
using Haustrunk.Application.Artikel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haustrunk.Application.Artikel.Validators
{
    public class UpdateArtikelCommandValidator : AbstractValidator<UpdateArtikelCommand>
    {
        public UpdateArtikelCommandValidator()
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
