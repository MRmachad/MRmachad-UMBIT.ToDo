﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using UMBIT.MicroService.SDK.API.Models;
using UMBIT.MicroService.SDK.Basicos.Excecoes;
using UMBIT.MicroService.SDK.Notificacao;
using UMBIT.MicroService.SDK.Notificacao.Interfaces;

namespace UMBIT.MicroService.SDK.API.Extensoes
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IHostingEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext, INotificador notificador)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ExcecaoBasicaUMBIT ex)
            {
                notificador.AdicionarErroSistema(new ErroSistema(ex.Mensagem, ex));
                await HandleExceptionAsync(httpContext, notificador);
            }
            catch (Exception ex)
            {
                notificador.AdicionarErroSistema(new ErroSistema("Erro Generico!", ex));
                await HandleExceptionAsync(httpContext, notificador);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, INotificador notificador)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var dadosResposta = new Resposta();
            dadosResposta.Sucesso = false;
            dadosResposta.Erros = notificador.ObterNotificacoes();

            if (_environment.IsDevelopment())
                dadosResposta.ErrosSistema = notificador.ObterErrosSistema();

            var result = JsonSerializer.Serialize(dadosResposta);
            await context.Response.WriteAsync(result);
        }
    }
}
