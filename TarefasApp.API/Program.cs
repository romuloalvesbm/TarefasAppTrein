using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using TarefasApp.API.Middlewares;
using TarefasApp.API.Profiles;
using TarefasApp.Infra.MongoDB.Extensions;
using TarefasApp.Infra.SqlServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSqlServer(builder.Configuration); //M�todo de extens�o
builder.Services.AddMongoDB(builder.Configuration); //M�todo de extens�o

//Swagger
builder.Services.AddRouting(map => map.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper
builder.Services.AddAutoMapper(typeof(TarefasProfile));

//MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

// JWT
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // Configura��es para validar o token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true, // Valida a expira��o do token
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do token
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetValue<string>("JwtToken:SecretKey")!
                )
            )
        };
    });

var app = builder.Build();

//Middlewares
app.UseMiddleware<ExceptionMiddleware>();

app.MapOpenApi();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapScalarApiReference(s => s.WithTheme(ScalarTheme.BluePlanet));

app.UseAuthentication(); //Aplicar as politicas de autentica��o
app.UseAuthorization(); //Verificar as permiss�es de acesso

app.MapControllers();
app.Run();