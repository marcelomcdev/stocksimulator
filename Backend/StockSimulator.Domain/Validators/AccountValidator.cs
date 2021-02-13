﻿using FluentValidation;
using StockSimulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 20);
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
