using VDT.Common.ServiceProvider.Endpoint;

namespace VDT.Common.ServiceProvider.ApiSpeciification
{
	interface IApiSpecificationProvider
	{
		SpecificationSource Source { get; }
		EndpointManager ApiDefinition { get; }

	}



}