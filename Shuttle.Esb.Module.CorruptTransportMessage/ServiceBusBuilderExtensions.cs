using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
    public static class ServiceBusBuilderExtensions
    {
        public static ServiceBusBuilder AddCorruptTransportMessageModule(this ServiceBusBuilder serviceBusBuilder,
            Action<CorruptTransportMessageBuilder> builder = null)
        {
            Guard.AgainstNull(serviceBusBuilder, nameof(serviceBusBuilder));

            var corruptTransportMessageBuilder = new CorruptTransportMessageBuilder(serviceBusBuilder.Services);

            builder?.Invoke(corruptTransportMessageBuilder);

            serviceBusBuilder.Services.TryAddSingleton<CorruptTransportMessageModule, CorruptTransportMessageModule>();

            serviceBusBuilder.Services.AddOptions<CorruptTransportMessageOptions>().Configure(options =>
            {
                options.MessageFolder = corruptTransportMessageBuilder.Options.MessageFolder;
            });

            serviceBusBuilder.Services.AddPipelineModule<CorruptTransportMessageModule>();

            return serviceBusBuilder;
        }
    }
}