using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.SqlServer.Entities;

namespace TarefasApp.Infra.SqlServer.Mappings
{
    /// <summary>
    /// Classe para mapeamento ORM da entidade Tarefa
    /// </summary>
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas");

            builder.HasKey(t => t.Id);
        }
    }
}

