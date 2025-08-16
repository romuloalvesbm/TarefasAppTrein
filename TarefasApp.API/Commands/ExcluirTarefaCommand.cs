using MediatR;
using TarefasApp.API.Queries;

namespace TarefasApp.API.Commands
{
    /// <summary>
    /// Modelo de dados para exclusão de uma tarefa
    /// </summary>
    public class ExcluirTarefaCommand : IRequest<ObterTarefasQuery>
    {
        public Guid Id { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}

