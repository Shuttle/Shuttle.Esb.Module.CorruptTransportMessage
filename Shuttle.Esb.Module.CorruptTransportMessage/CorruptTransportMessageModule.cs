using System;
using System.IO;
using Shuttle.Core.Contract;
using Shuttle.Core.Streams;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
    public class CorruptTransportMessageModule
    {
        private readonly string _corruptTransportMessageFolder;

        public CorruptTransportMessageModule(IServiceBusEvents events, ICorruptTransportMessageConfiguration corruptTransportMessageConfiguration)
        {
            Guard.AgainstNull(events, nameof(events));Guard.AgainstNull(events, nameof(events));

	        _corruptTransportMessageFolder = corruptTransportMessageConfiguration.CorruptTransportMessageFolder;

			events.TransportMessageDeserializationException += OnTransportMessageDeserializationException;
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