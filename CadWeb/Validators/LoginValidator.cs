using CadWeb.Models;
using FluentValidation;

namespace CadWeb.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Usuario)
                .MaximumLength(20).WithMessage("Campo com máximo de 20 caracteres.")
                .NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.Senha)
                .MaximumLength(20).WithMessage("Campo com máximo de 20 caracteres.")
                .NotEmpty().WithMessage("Campo obrigatório.");
        }
    }
}
