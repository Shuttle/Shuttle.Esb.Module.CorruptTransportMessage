using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.CorruptTransportMessage
{
	public class Bootstrap :
		IComponentRegistryBootstrap,
		IComponentResolverBootstrap
	{
		private static bool _registryBootstrapCalled;
		private static bool _resolverBootstrapCalled;

		public void Register(IComponentRegistry registry)
		{
			Guard.AgainstNull(registry, nameof(registry));Guard.AgainstNull(registry, nameof(registry));

			if (_registryBootstrapCalled)
			{
				return;
			}

		    if (!registry.IsRegistered<ICorruptTransportMessageConfiguration>())
		    {
		        registry.AttemptRegisterInstance(CorruptTransportMessageSection.Configuration());
		    }

		    registry.AttemptRegister<CorruptTransportMessageModule>();

			_registryBootstrapCalled = true;
		}

		public void Resolve(IComponentResolver resolver)
		{
			Guard.AgainstNull(resolver, nameof(resolver));Guard.AgainstNull(resolver, nameof(resolver));

			if (_resolverBootstrapCalled)
			{
				return;
			}

			resolver.Resolve<CorruptTransportMessageModule>();

			_resolverBootstrapCalled = true;
		}
	}
}