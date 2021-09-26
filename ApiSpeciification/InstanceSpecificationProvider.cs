using RestServiceProviderServiceProvider.Endpoint;

namespace RestServiceProviderServiceProvider.ApiSpeciification
{
	/// <summary>
	/// use this provider if you are building the EndPointManager in code
	/// </summary>
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