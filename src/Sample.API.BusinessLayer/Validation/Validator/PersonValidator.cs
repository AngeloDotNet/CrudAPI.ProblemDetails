﻿using FluentValidation;
using Sample.API.DataAccessLayer.Entity;

namespace Sample.API.BusinessLayer.Validation.Validator;

public class PersonValidator : AbstractValidator<PersonEntity>
{
    public PersonValidator()
    {
        RuleFor(x => x.Cognome)
            .NotEmpty().WithMessage("Il cognome è obbligatorio");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Il nome è obbligatorio");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email è obbligatoria")
            .EmailAddress().WithMessage("L'email non è valida");
    }
}