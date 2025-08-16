using FluentValidation;
using TarefasApp.Infra.SqlServer.Entities;

namespace TarefasApp.API.Validators
{
    /// <summary>
    /// Classe de validação para a entidade Tarefa
    /// </summary>
    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("O ID da tarefa é obrigatório.");

            RuleFor(t => t.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .Length(3, 100).WithMessage("O título deve ter entre 3 e 100 caracteres.");

            RuleFor(t => t.Descricao)
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");

            RuleFor(t => t.DataHoraInicio)
                .NotNull().WithMessage("A data e hora de início é obrigatório.");

            RuleFor(t => t.DataHoraCriacao)
                .NotNull().WithMessage("A data e hora de criação é obrigatório.");

            RuleFor(t => t.DataHoraUltimaAlteracao)
                .NotNull().WithMessage("A data e hora de última alteração é obrigatório.");

            RuleFor(t => t.Finalizado)
                .NotNull().WithMessage("O status de finalizado é obrigatório.");

            RuleFor(t => t.Ativo)
                .NotNull().WithMessage("O status de ativo é obrigatório.");

            RuleFor(t => t.UsuarioId)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório.");
        }
    }
}
