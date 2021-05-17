using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using RestServiceProviderServiceProvider.Endpoint.Enums;

namespace RestServiceProviderServiceProvider.Endpoint.Entities
{
	public class EndpointOperation
	{
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public OperationMethod Method { get; set; }
		public string Name { get; set; }
		public IEnumerable<OperationParameter> Parameters { get; set; } = new List<OperationParameter>();
		[JsonIgnore]
		public IEnumerable<OperationParameter> RequiredParameters => Parameters?.Where(p => p.IsRequired);

		public bool HasParameters(ParameterLocation location) =>
			Parameters?.Any(p => p.Location == location)
			?? false;
	}



}