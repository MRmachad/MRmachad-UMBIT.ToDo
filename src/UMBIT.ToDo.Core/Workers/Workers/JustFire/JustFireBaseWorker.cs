using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TSE.Nexus.SDK.Workers.Interfaces;

namespace TSE.Nexus.SDK.Workers.Workers.JustFire
{
    public abstract class JustFireBaseWorker : BackgroundService, IWorker
    {
        protected readonly IServiceScopeFactory ServiceScopeFactory;

        public JustFireBaseWorker( IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

    }
}
