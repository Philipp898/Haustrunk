using FluentValidation;
using Haustrunk.Application.Bestellung.Commands;
using Haustrunk.Application.Common.Interfaces;

namespace Haustrunk.Application.Bestellung.Validators
{
    public class UpdateBestellungCommandValidator : AbstractValidator<UpdateBestellungCommand>
    {
        private readonly IDateTimeService _dateTime;

        public UpdateBestellungCommandValidator(IDateTimeService dateTime)
        {
            _dateTime = dateTime;

            RuleFor(b => b.Bestellpositionen)
                    .NotEmpty();
            RuleFor(b => b.BestelltZu)
                    .GreaterThan(_dateTime.Now)
                    .NotEmpty();
        }
    }
}
