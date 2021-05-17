using RestServiceProviderServiceProvider.Endpoint;

namespace RestServiceProviderServiceProvider.ApiSpeciification
{
	internal class InstanceSpecificationProvider : IApiSpecificationProvider
	{
		public SpecificationSource Source { get; } = SpecificationSource.Instance;
		public EndpointManager ApiDefinition { get; protected set; }
		public InstanceSpecificationProvider(EndpointManager manager)
		{
			ApiDefinition = manager;
		}
	}



}