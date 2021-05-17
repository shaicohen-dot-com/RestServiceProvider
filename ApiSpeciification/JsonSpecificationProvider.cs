using System.IO;
using System.Text.Json;
using RestServiceProviderServiceProvider.Endpoint;

namespace RestServiceProviderServiceProvider.ApiSpeciification
{
	internal class JsonSpecificationProvider : IApiSpecificationProvider
	{
		public SpecificationSource Source { get; } = SpecificationSource.JsonFile;
		public EndpointManager ApiDefinition { get; protected set; }
		public JsonSpecificationProvider(string filePath)
		{
			ApiDefinition = JsonSerializer.Deserialize<EndpointManager>(
				File.ReadAllText(filePath));
		}
	}



}