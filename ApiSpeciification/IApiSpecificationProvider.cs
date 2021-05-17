using RestServiceProviderServiceProvider.Endpoint;

namespace RestServiceProviderServiceProvider.ApiSpeciification
{
	interface IApiSpecificationProvider
	{
		SpecificationSource Source { get; }
		EndpointManager ApiDefinition { get; }

	}



}