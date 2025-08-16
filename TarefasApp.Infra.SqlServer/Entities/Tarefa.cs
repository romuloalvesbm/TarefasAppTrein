using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Infra.SqlServer.Entities
{
    /// <summary>
    /// Modelo de dados de tarefa
    /// </summary>
    public class Tarefa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public bool Finalizado { get; set; }
        public DateTime DataHoraCriacao { get; set; } = DateTime.Now;
        public DateTime DataHoraUltimaAlteracao { get; set; } = DateTime.Now;
        public Guid UsuarioId { get; set; }
        public bool Ativo { get; set; } = true;
    }
}

