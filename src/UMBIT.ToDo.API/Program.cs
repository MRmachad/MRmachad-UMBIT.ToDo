using UMBIT.ToDo.API.Bootstrapper;
using UMBIT.ToDo.Infraestrutura.Contextos;
using UMBIT.ToDo.Core.Repositorio.Bootstrapper;
using UMBIT.ToDo.Core.Notificacao.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApp();
builder.Services.AddDataBase<AppContexto>(builder.Configuration);
builder.Services.AdicionarNotificacao(builder.Configuration);
builder.Services.AddDependencias();

var app = builder.Build();

app.UseApp();
app.UseMigrations();
app.Run();
