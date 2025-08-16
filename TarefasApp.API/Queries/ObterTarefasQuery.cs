namespace TarefasApp.API.Queries
{
    /// <summary>
    /// Modelo de dados para consulta de tarefas
    /// </summary>
    /// <param name="Id">Id da tarefa</param>
    /// <param name="Titulo">Título da tarefa</param>
    /// <param name="Descricao">Descrição da tarefa</param>
    /// <param name="DataHoraInicio">Data e hora de início da tarefa</param>
    /// <param name="Finalizado">Status de finalizado</param>
    /// <param name="DataHoraCriacao">Data e hora da criação da tarefa</param>
    /// <param name="DataHoraUltimaAlteracao">Data e hora da última alteração da tarefa</param>
    /// <param name="UsuarioId">Id do usuário associado a tarefa</param>
    public record ObterTarefasQuery(
            Guid Id,
            string Titulo,
            string Descricao,
            DateTime DataHoraInicio,
            bool Finalizado,
            DateTime DataHoraCriacao,
            DateTime DataHoraUltimaAlteracao,
            Guid UsuarioId
        );
}

