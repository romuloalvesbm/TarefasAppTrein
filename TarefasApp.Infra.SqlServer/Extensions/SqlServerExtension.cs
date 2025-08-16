using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.SqlServer.Contexts;

namespace TarefasApp.Infra.SqlServer.Extensions
{
    /// <summary>
    /// Classe para métodos de extensão que irão configurar
    /// as injeções de dependência do EntityFramework
    /// </summary>
    public static class SqlServerExtension
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<SqlServerContext>
                (options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
