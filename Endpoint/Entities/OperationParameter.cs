using System;
using System.Text.Json.Serialization;
using RestServiceProviderServiceProvider.Endpoint.Enums;

namespace RestServiceProviderServiceProvider.Endpoint.Entities
{
	public class OperationParameter
	{
		public string Name { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ParameterLocation Location { get; set; }
		public bool IsRequired { get; set; }
		//public string Type { get; set; }
		[JsonPropertyName("Type")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ParameterValueType Type { get; set; }
	}



}