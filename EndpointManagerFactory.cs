using RestServiceProviderServiceProvider.ApiSpeciification;
using RestServiceProviderServiceProvider.Endpoint;

namespace RestServiceProviderServiceProvider
{
	internal static class EndpointManagerFactory
	{
		public static EndpointManager Create(IApiSpecificationProvider apiSpecification) => 
			apiSpecification.ApiDefinition;
	}



}