using MediatR;
using TarefasApp.API.Queries;

namespace TarefasApp.API.Commands
{
    /// <summary>
    /// Modelo de dados para criação de uma tarefa
    /// </summary>
    public class CriarTarefaCommand : IRequest<ObterTarefasQuery>
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public bool Finalizado { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}




