using AutoMapper;
using MediatR;
using TarefasApp.API.Commands;
using TarefasApp.API.Notification;
using TarefasApp.API.Queries;
using TarefasApp.API.Validators;
using TarefasApp.Infra.SqlServer.Contexts;
using TarefasApp.Infra.SqlServer.Entities;

namespace TarefasApp.API.Handlers
{
    /// <summary>
    /// Handler para processar as requisições 
    /// de Criar, Atualizar e Excluir tarefas
    /// </summary>
    public class TarefaRequestHandler(SqlServerContext context, IMapper mapper, IMediator mediator) :
        IRequestHandler<CriarTarefaCommand, ObterTarefasQuery>,
        IRequestHandler<AtualizarTarefaCommand, ObterTarefasQuery>,
        IRequestHandler<ExcluirTarefaCommand, ObterTarefasQuery>
    {
        public async Task<ObterTarefasQuery> Handle(CriarTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = mapper.Map<Tarefa>(request);

            var tarefaValidator = new TarefaValidator();
            var result = tarefaValidator.Validate(tarefa);

            if (!result.IsValid) 
                throw new FluentValidation.ValidationException(result.Errors);

            await context.AddAsync(tarefa);
            await context.SaveChangesAsync();

            var tarefaQuery = mapper.Map<ObterTarefasQuery>(tarefa);

            var notification = new TarefaNotification
            {
                Action = NotificationAction.Created,
                Tarefa = tarefaQuery
            };

            await mediator.Publish(notification);

            return tarefaQuery;
        }

        public async Task<ObterTarefasQuery> Handle(AtualizarTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await context.Set<Tarefa>().FindAsync(request.Id);

            if (tarefa == null)
                throw new ApplicationException("Tarefa não encontrada para edição.");

            mapper.Map(request, tarefa);

            var tarefaValidator = new TarefaValidator();
            var result = tarefaValidator.Validate(tarefa);

            if (!result.IsValid)
                throw new FluentValidation.ValidationException(result.Errors);

            context.Update(tarefa);
            await context.SaveChangesAsync();

            var tarefaQuery = mapper.Map<ObterTarefasQuery>(tarefa);

            var notification = new TarefaNotification
            {
                Action = NotificationAction.Updated,
                Tarefa = tarefaQuery
            };

            await mediator.Publish(notification);

            return tarefaQuery;
        }

        public async Task<ObterTarefasQuery> Handle(ExcluirTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await context.Set<Tarefa>().FindAsync(request.Id);

            if (tarefa == null)
                throw new ApplicationException("Tarefa não encontrada para exclusão.");

            context.Remove(tarefa);
            await context.SaveChangesAsync();

            var tarefaQuery = mapper.Map<ObterTarefasQuery>(tarefa);

            var notification = new TarefaNotification
            {
                Action = NotificationAction.Deleted,
                Tarefa = tarefaQuery
            };

            await mediator.Publish(notification);

            return tarefaQuery;
        }
    }
}



