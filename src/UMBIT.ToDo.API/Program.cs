using System;
using UMBIT.ToDo.SDK.Notificacao.Bootstrapper;
using UMBIT.ToDo.SDK.Repositorio.Bootstrapper;
using UMBIT.ToDo.API.Bootstrapper;
using UMBIT.ToDo.Infraestrutura.Contextos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApp();
builder.Services.AddDataBase<AppContexto>(builder.Configuration, "UMBIT.ToDo.API");
builder.Services.AdicionarNotificacao();
builder.Services.AddDependencias();

var app = builder.Build();

app.UseApp();
app.UseMigrations();
app.Run();
