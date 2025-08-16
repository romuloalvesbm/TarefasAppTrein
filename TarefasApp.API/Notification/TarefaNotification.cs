using MediatR;
using TarefasApp.API.Queries;

namespace TarefasApp.API.Notification
{
    /// <summary>
    /// Define os dados que serão enviados para o MongoDB
    /// e que tipos de ações serão capturadas pelo Notification
    /// </summary>
    public class TarefaNotification : INotification
    {
        public ObterTarefasQuery? Tarefa { get; set; } //dados da tarefa
        public NotificationAction Action { get; set; } //tipo da ação
    }

    /// <summary>
    /// Enum para definir os tipos de ações
    /// </summary>
    public enum NotificationAction
    {
        Created = 1,
        Updated = 2,
        Deleted = 3
    }
}

