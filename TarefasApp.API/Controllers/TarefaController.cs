using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TarefasApp.API.Commands;
using TarefasApp.API.Queries;
using TarefasApp.Infra.MongoDB.Collections;
using TarefasApp.Infra.MongoDB.Contexts;

namespace TarefasApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController(IMediator mediator, IMapper mapper, MongoDBContext mongoDBContext) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ObterTarefasQuery), 201)]
        public async Task<IActionResult> Post([FromBody] CriarTarefaCommand command)
        {
            command.UsuarioId = Guid.Parse(User.FindFirst("jti")?.Value); //Id do usuário contido no TOKEN            

            var result = await mediator.Send(command);
            return StatusCode(201, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ObterTarefasQuery), 200)]
        public async Task<IActionResult> Put([FromBody] AtualizarTarefaCommand command)
        {
            command.UsuarioId = Guid.Parse(User.Identity.Name); //Id do usuário contido no TOKEN

            var result = await mediator.Send(command);
            return StatusCode(200, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ObterTarefasQuery), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new ExcluirTarefaCommand
            {
                Id = id,
                UsuarioId = Guid.Parse(User.Identity.Name)
            };

            var result = await mediator.Send(command);
            return StatusCode(200, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ObterTarefasQuery>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] int pagina = 1, [FromQuery] int quantidade = 25)
        {
            if (quantidade > 25)
                return BadRequest(new { Message = "A quantidade máxima para consulta é de 25 registros." });

            //Calcula quantos registros serão pulados com base na página atual
            var skip = (pagina - 1) * quantidade;

            //Cria um filtro para buscar os registros no mongodb (somente registros ativos)
            var filter = Builders<TarefasCollection>.Filter.Eq(t => t.Ativo, false);

            //Definindo uma ordenação por data de criação decrescente
            var sort = Builders<TarefasCollection>.Sort.Descending(t => t.DataHoraCriacao);

            //Executando a consulta no banco de dados
            var tarefas = await mongoDBContext.Tarefas
                            .Find(filter)   //aplica o filtro (somente tarefas ativas)
                            .Sort(sort)     //aplica a ordenação
                            .Skip(skip)     //pular os registros conforme a página
                            .Limit(quantidade)  //define o número máximo de registros retornados
                            .ToListAsync(); //executa a consulta e retorna uma lista

            var tarefasQuery = mapper.Map<List<ObterTarefasQuery>>(tarefas);

            return Ok(tarefasQuery);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterTarefasQuery), 200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }
    }
}


