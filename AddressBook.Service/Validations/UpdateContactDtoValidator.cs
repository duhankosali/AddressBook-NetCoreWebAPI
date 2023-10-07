using AddressBook.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Service.Validations
{
    public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
    {
        public UpdateContactDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required");

            RuleFor(c => c.Address)
                .NotNull().WithMessage("Address is required")
                .NotEmpty().WithMessage("Address is required");

            RuleFor(c => c.Phone)
                .NotNull().WithMessage("Phone is required.")
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone should be 10 digits long."); // just digit.

            RuleFor(c => c.MobilePhone)
                .Matches(@"^\d{10}$").WithMessage("Phone should be 10 digits long."); // just digit.

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Please enter a valid email.");
        }
    }
}
