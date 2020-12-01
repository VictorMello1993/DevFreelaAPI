using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Validators
{
    public class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
    {
        public CreateUserInputModelValidator()
        {
            RuleFor(im => im.Email)
                .NotNull()
                .WithMessage("E-mail precisa ser preenchido")
                .NotEmpty()
                .WithMessage("E-mail precisa ser preenchido");
        }
    }
}
