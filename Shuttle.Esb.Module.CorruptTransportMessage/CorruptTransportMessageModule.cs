using System;
using System.IO;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
    public class CorruptTransportMessageModule
    {
        private readonly string _corruptTransportMessageFolder;

        public CorruptTransportMessageModule(IServiceBusEvents events, ICorruptTransportMessageConfiguration corruptTransportMessageConfiguration)
        {
            Guard.AgainstNull(events, "events");

	        _corruptTransportMessageFolder = corruptTransportMessageConfiguration.CorruptTransportMessageFolder;

			events.TransportMessageDeserializationException += OnTransportMessageDeserializationException;
        }

        private void OnTransportMessageDeserializationException(object sender,
            DeserializationExceptionEventArgs deserializationExceptionEventArgs)
        {
            var filePath = Path.Combine(_corruptTransportMessageFolder, string.Format("{0}.stm", Guid.NewGuid()));

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