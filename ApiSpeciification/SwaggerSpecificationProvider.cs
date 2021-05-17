using System.IO;
using RestServiceProviderServiceProvider.Endpoint;
using RestServiceProviderServiceProvider.Parser;

namespace RestServiceProviderServiceProvider.ApiSpeciification
{
	internal class SwaggerSpecificationProvider : IApiSpecificationProvider
	{
		public SpecificationSource Source { get; } = SpecificationSource.SwaggerDefinition;
		public EndpointManager ApiDefinition { get; protected set; }
		public SwaggerSpecificationProvider(string filePath)
		{
			ApiDefinition = ApiSwaggerDocumentParser.Parse(
				File.ReadAllText(filePath))
					.ToManager();
		}
	}



}