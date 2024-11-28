using TSE.Nexus.Auth.API.Bootstrapper;
using TSE.Nexus.NodeLink.API.Bootstrapper;
using TSE.Nexus.SDK.Workers.Bootstrapper;
using UMBIT.ToDo.API.Bootstrapper;
using UMBIT.ToDo.Core.API.Bootstrapper;
using UMBIT.ToDo.Core.Messages.Bootstrapper;
using UMBIT.ToDo.Core.Notificacao.Bootstrapper;
using UMBIT.ToDo.Core.Repositorio.Bootstrapper;
using UMBIT.ToDo.Core.Seguranca.Bootstrapper;
using UMBIT.ToDo.Infraestrutura.Contextos;
using TSE.Nexus.SDK.SignalR.Bootstrapper;

InicializeConfigurate.Inicialize();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddContextPrincipal()
    .AdicionarNotificacao(builder.Configuration)
    .AddDataBase<BusinessContext>(builder.Configuration)
    .AddIdentityConfiguration(builder.Configuration);

builder.Services
    .AddMessages(builder.Configuration)
    .AddSignalRHub()
    .AddWorkers()
    .AddApp()
    .AddDependencias(builder.Configuration);

builder.Services.AddSeguranca(builder.Configuration);

var app = builder.Build();

app.UseApp();
app.UseMigrations();
app.UseIdentityMigrations();

app.UseSignalRHub();
app.UseFabricaGenerica();

app.Run();
