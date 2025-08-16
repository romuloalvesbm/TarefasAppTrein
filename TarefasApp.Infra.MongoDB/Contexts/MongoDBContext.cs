using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.MongoDB.Collections;

namespace TarefasApp.Infra.MongoDB.Contexts
{
    /// <summary>
    /// Classe de contexto para conexão com o banco de dados
    /// do MongoDB e configuação das collections
    /// </summary>
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TarefasCollection> Tarefas
            => _database.GetCollection<TarefasCollection>("tarefas");
    }
}

