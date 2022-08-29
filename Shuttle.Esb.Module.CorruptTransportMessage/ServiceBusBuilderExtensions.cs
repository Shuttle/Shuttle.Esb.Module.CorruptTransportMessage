using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
    public static class ServiceBusBuilderExtensions
    {
        public static IServiceCollection AddCorruptTransportMessageModule(this IServiceCollection services,
            Action<CorruptTransportMessageBuilder> builder = null)
        {
            Guard.AgainstNull(services, nameof(services));

            var corruptTransportMessageBuilder = new CorruptTransportMessageBuilder(services);

            builder?.Invoke(corruptTransportMessageBuilder);

            services.TryAddSingleton<CorruptTransportMessageModule, CorruptTransportMessageModule>();

            services.AddOptions<CorruptTransportMessageOptions>().Configure(options =>
            {
                options.MessageFolder = corruptTransportMessageBuilder.Options.MessageFolder;
            });

            services.AddPipelineModule<CorruptTransportMessageModule>();

            return services;
        }
    }
}