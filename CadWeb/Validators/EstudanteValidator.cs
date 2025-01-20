using CadWeb.Models;
using FluentValidation;

namespace CadWeb.Validators
{
    public class EstudanteValidator : AbstractValidator<EstudanteViewModel>
    {
        public EstudanteValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(14).WithMessage("Campo com máximo de 11 caracteres.");
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(100).WithMessage("Campo com máximo de 100 caracteres.");
            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(200).WithMessage("Campo com máximo de 200 caracteres.");
            RuleFor(x => x.NomeCurso)
                .NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(100).WithMessage("Campo com máximo de 100 caracteres.");
            RuleFor(x => x.DataConclusao)
                .NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.SelectedEstadoUF)
                .NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.SelectedCidadeValue)
                .NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.SelectedInstituicaoEnsinoId)
                .NotEqual(0).WithMessage("Campo obrigatório.");
        }
    }
}
