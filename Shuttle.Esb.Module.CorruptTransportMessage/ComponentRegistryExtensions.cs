using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
	public static class ComponentRegistryExtensions
	{
		public static void RegisterCorruptTransportMessage(this IComponentRegistry registry)
		{
			Guard.AgainstNull(registry, nameof(registry));

		    if (!registry.IsRegistered<ICorruptTransportMessageConfiguration>())
		    {
		        registry.AttemptRegisterInstance(CorruptTransportMessageSection.Configuration());
		    }

		    registry.AttemptRegister<CorruptTransportMessageModule>();
		}
	}
}