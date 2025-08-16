using System.Net;
using System.Text.Json;

namespace TarefasApp.API.Middlewares
{
    /// <summary>
    /// Classe global para tratamento de exceções
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        /// <summary>
        /// Método para interceptar as requisições enviadas para o servidor
        /// e capturar as exceções provocadas por essas requisições
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (FluentValidation.ValidationException e)
            {
                await HandleValidationException(context, e);
            }
        }

        /// <summary>
        /// Tratamento dos erros de validação do FLuentValidation
        /// </summary>
        private static Task HandleValidationException(HttpContext context, FluentValidation.ValidationException e)
        {
            //Definir a resposta como HTTP 400 (BAD REQUEST)
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            //Organizar as mensagens de erro que serão retornadas
            var erros = e.Errors.Select(e =>

            new
            {
                Campo = e.PropertyName,
                Mensagem = e.ErrorMessage
            });

            //Serializar os dados em JSON
            var json = JsonSerializer.Serialize(new { erros });
            return context.Response.WriteAsync(json);
        }
    }
}
