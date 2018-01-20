using System.Configuration;
using Shuttle.Core.Configuration;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
    public class CorruptTransportMessageSection : ConfigurationSection
    {
        [ConfigurationProperty("folder", IsRequired = false, DefaultValue = ".\\corrupt-transport-messages")]
        public string Folder => (string) this["folder"];

        public static ICorruptTransportMessageConfiguration Configuration()
        {
            var section =
                ConfigurationSectionProvider.Open<CorruptTransportMessageSection>("shuttle", "corruptTransportMessage");
            var configuration = new CorruptTransportMessageConfiguration();

            if (section != null)
            {
                configuration.CorruptTransportMessageFolder = section.Folder;
            }

            return configuration;
        }
    }
}