using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Hubs;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.Repositories;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

//adiciona o serviço do signar ao projeto
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adiciona serviço de Jwt Bearer (forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem está solicitando
        ValidateIssuer = true,

        //valida quem está recebendo
        ValidateAudience = true,

        //define se o tempo de expiração será validado
        ValidateLifetime = true,

        //forma de criptografia e valida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("techconnect-webapi-chave-autenticacao-ef")),

        //valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //nome do issuer (de onde está vindo)
        ValidIssuer = "webapi.techconnect",

        //nome do audience (para onde está indo)
        ValidAudience = "webapi.techconnect"
    };
});


builder.Services.AddDbContext<TechConnectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependencias
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IComentario, ComentarioRepository>();
builder.Services.AddScoped<IPublicacao, PublicacaoRepository>();
builder.Services.AddScoped<ICurtida, CurtidaRepository>();
builder.Services.AddScoped<ISeguidores, SeguidoresRepository>();
builder.Services.AddScoped<IChat, ChatRepository>();

string ContentModeratorApiKey = Environment.GetEnvironmentVariable("ContentModeratorApiKey")!;
string ContentModeratorEndpoint = Environment.GetEnvironmentVariable("ContentModeratorEndpoint")!;

builder.Services.AddSingleton(provider => new ContentModeratorClient(
    new ApiKeyServiceClientCredentials(ContentModeratorApiKey))
{
    Endpoint = ContentModeratorEndpoint
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//Habilita o CORS
app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//add map hub
app.MapHub<HubProvider>("/Hub");

app.Run();