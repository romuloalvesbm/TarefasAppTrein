using AutoMapper;
using MediatR;
using MongoDB.Driver;
using TarefasApp.Infra.MongoDB.Collections;
using TarefasApp.Infra.MongoDB.Contexts;

namespace TarefasApp.API.Notification
{
    /// <summary>
    /// Implementação do Notification Handler de tarefas
    /// </summary>
    public class TarefaNotificationHandler(MongoDBContext mongoDBContext, IMapper mapper) : INotificationHandler<TarefaNotification>
    {
        public async Task Handle(TarefaNotification notification, CancellationToken cancellationToken)
        {
            var tarefaCollection = mapper.Map<TarefasCollection>(notification.Tarefa);

            switch (notification.Action)
            {
                case NotificationAction.Created:

                    await mongoDBContext.Tarefas.InsertOneAsync(tarefaCollection);

                    break;

                case NotificationAction.Updated:

                    var filterUpdate = Builders<TarefasCollection>.Filter.Eq(t => t.Id, tarefaCollection.Id);

                    await mongoDBContext.Tarefas.ReplaceOneAsync(filterUpdate, tarefaCollection);

                    break;

                case NotificationAction.Deleted:

                    var filterDelete = Builders<TarefasCollection>.Filter.Eq(t => t.Id, tarefaCollection.Id);

                    await mongoDBContext.Tarefas.DeleteOneAsync(filterDelete);

                    break;
            }
        }
    }
}



