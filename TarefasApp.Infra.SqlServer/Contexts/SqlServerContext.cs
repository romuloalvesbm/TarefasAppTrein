using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.SqlServer.Mappings;

namespace TarefasApp.Infra.SqlServer.Contexts
{
    /// <summary>
    /// Classe de contexto para conexão e configuração do acesso
    /// ao banco de dados do Entity Framework
    /// </summary>
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
        }
    }
}


