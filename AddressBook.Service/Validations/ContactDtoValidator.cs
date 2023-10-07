using AddressBook.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Service.Validations
{
    public class ContactDtoValidator : AbstractValidator<ContactDto> // Fluent Validation 
    {
        public ContactDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Address)
                .NotNull().WithMessage("{PropertyAddress} is required")
                .NotEmpty().WithMessage("{PropertyAddress} is required");

            RuleFor(c => c.Phone)
                .NotNull().WithMessage("{PropertyPhone} is required.")
                .NotEmpty().WithMessage("{PropertyPhone} is required.")
                .Matches(@"^\d{10}$").WithMessage("{PropertyPhone} should be 10 digits long."); // just digit.

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Please enter a valid email.");
        }
    }
}
