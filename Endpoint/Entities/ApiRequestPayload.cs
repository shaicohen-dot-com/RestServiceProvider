using System.Collections.Generic;

namespace RestServiceProviderServiceProvider.Endpoint.Entities
{
	public class ApiRequestPayload
	{
		public ApiResourceEndpoint Endpoint { get; set; } = null;
		public EndpointOperation Operation { get; set; } = null;
		public IEnumerable<ParameterValue> ParameterValues { get; set; } = null;

		internal ApiRequestPayload(ApiResourceEndpoint endpoint, string operationName, IEnumerable<ParameterValue> parameterValues)
		{
			Endpoint = endpoint;
			Operation = endpoint.GetOperation(operationName);
			ParameterValues = parameterValues;
		}
	}
}