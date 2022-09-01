using System;
using System.IO;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Streams;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
    public class CorruptTransportMessageModule
    {
        private readonly string _corruptTransportMessageFolder;

        public CorruptTransportMessageModule(IOptions<CorruptTransportMessageOptions> corruptTransportMessageOptions, IDeserializeTransportMessageObserver deserializeTransportMessageObserver)
        {
            Guard.AgainstNull(corruptTransportMessageOptions, nameof(corruptTransportMessageOptions));
            Guard.AgainstNull(corruptTransportMessageOptions.Value, nameof(corruptTransportMessageOptions.Value));
            Guard.AgainstNull(deserializeTransportMessageObserver, nameof(deserializeTransportMessageObserver));

	        _corruptTransportMessageFolder = corruptTransportMessageOptions.Value.MessageFolder;

            deserializeTransportMessageObserver.TransportMessageDeserializationException += OnTransportMessageDeserializationException;
        }

        private void OnTransportMessageDeserializationException(object sender, DeserializationExceptionEventArgs deserializationExceptionEventArgs)
        {
            var filePath = Path.Combine(_corruptTransportMessageFolder, $"{Guid.NewGuid()}.stm");

            using (Stream file = File.OpenWrite(filePath))
            using (
                var stream =
                    deserializationExceptionEventArgs.PipelineEvent.Pipeline.State.GetReceivedMessage().Stream.Copy()
                )
            {
                stream.CopyTo(file);
            }
        }
    }
}