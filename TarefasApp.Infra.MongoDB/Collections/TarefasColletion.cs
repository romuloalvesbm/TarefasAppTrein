using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Infra.MongoDB.Collections
{
    /// <summary>
    /// Modelo da collection onde ficarão os registros
    /// de tarefas dentro do banco de dados do MongoDB
    /// </summary>
    public class TarefasCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("_id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("titulo")]
        [BsonIgnoreIfNull]
        public string? Titulo { get; set; }

        [BsonElement("descricao")]
        [BsonIgnoreIfNull]
        public string? Descricao { get; set; }

        [BsonElement("data_hora_inicio")]
        [BsonIgnoreIfNull]
        public DateTime? DataHoraInicio { get; set; }

        [BsonElement("finalizado")]
        [BsonIgnoreIfNull]
        public bool? Finalizado { get; set; }

        [BsonElement("data_hora_criacao")]
        [BsonIgnoreIfNull]
        public DateTime? DataHoraCriacao { get; set; }

        [BsonElement("data_hora_ultima_alteracao")]
        [BsonIgnoreIfNull]
        public DateTime? DataHoraUltimaAlteracao { get; set; }

        [BsonElement("usuario_id")]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.String)]
        public Guid UsuarioId { get; set; }

        [BsonElement("ativo")]
        [BsonIgnoreIfNull]
        public bool Ativo { get; set; }
    }
}

