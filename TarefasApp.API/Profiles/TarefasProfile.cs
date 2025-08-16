using AutoMapper;
using TarefasApp.API.Commands;
using TarefasApp.API.Queries;
using TarefasApp.Infra.MongoDB.Collections;
using TarefasApp.Infra.SqlServer.Entities;

namespace TarefasApp.API.Profiles
{
    /// <summary>
    /// Classe para configuração dos mapeamentos de cópias de dados do AutoMapper
    /// para tarefas (Commands, Queries, Entidades e Collections)
    /// </summary>
    public class TarefasProfile : Profile
    {
        public TarefasProfile()
        {
            //Create Command -> Entity
            CreateMap<CriarTarefaCommand, Tarefa>();

            //Update Command -> Entity
            CreateMap<AtualizarTarefaCommand, Tarefa>();

            //Entity -> Query
            CreateMap<Tarefa, ObterTarefasQuery>();

            //Query -> Collection
            CreateMap<ObterTarefasQuery, TarefasCollection>().ReverseMap();
        }
    }
}

