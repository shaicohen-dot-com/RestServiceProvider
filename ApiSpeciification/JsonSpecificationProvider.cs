using System.IO;
using System.Text.Json;
using VDT.Common.ServiceProvider.Endpoint;

namespace VDT.Common.ServiceProvider.ApiSpeciification
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