using VDT.Common.ServiceProvider.Endpoint;

namespace VDT.Common.ServiceProvider.ApiSpeciification
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