using VDT.Common.ServiceProvider.ApiSpeciification;
using VDT.Common.ServiceProvider.Endpoint;

namespace VDT.Common.ServiceProvider
{
	internal static class EndpointManagerFactory
	{
		public static EndpointManager Create(IApiSpecificationProvider apiSpecification) => 
			apiSpecification.ApiDefinition;
	}



}