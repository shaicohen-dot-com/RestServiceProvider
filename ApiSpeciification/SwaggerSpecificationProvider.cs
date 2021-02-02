using System.IO;
using VDT.Common.ServiceProvider.Endpoint;
using VDT.Common.ServiceProvider.Parser;

namespace VDT.Common.ServiceProvider.ApiSpeciification
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