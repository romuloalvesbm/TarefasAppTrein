using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.MongoDB.Contexts;

namespace TarefasApp.Infra.MongoDB.Extensions
{
    /// <summary>
    /// Classe de extensão para definir as configurações
    /// de conexão de banco de dados do MongoDB
    /// </summary>
    public static class MongoDBExtension
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var databaseName = configuration.GetSection("MongoDbSettings:Database").Value;

            services.AddScoped(context => new MongoDBContext(connectionString!, databaseName!));

            return services;
        }
    }
}
