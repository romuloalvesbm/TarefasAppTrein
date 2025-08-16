using MediatR;
using TarefasApp.API.Queries;

namespace TarefasApp.API.Commands
{
    /// <summary>
    /// Modelo de dados para atualização de uma tarefa
    /// </summary>
    public class AtualizarTarefaCommand : IRequest<ObterTarefasQuery>
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public bool Finalizado { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}





