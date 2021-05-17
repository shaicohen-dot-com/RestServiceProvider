using System;
using System.Collections.Generic;
using System.Linq;
using RestServiceProviderServiceProvider.Endpoint.Entities;
using RestServiceProviderServiceProvider.Parser;

namespace RestServiceProviderServiceProvider.Endpoint.Entities
{
	public class ApiResourceEndpoint
	{
		public string Path { get; set; }
		public IEnumerable<EndpointOperation> Operations { get; set; } = new List<EndpointOperation>();

		public ApiResourceEndpoint() { }
		public ApiResourceEndpoint(string pathRaw, Operation operation) { }

		public EndpointOperation GetOperation(string operationName) =>
			Operations.FirstOrDefault(o => o.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase));

		public bool ContainsOperation(string operationName) =>
			Operations.Any(o => o.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase));

		/// <summary>
		/// Perform various checks to validate the operation's parameter values
		/// </summary>
		/// <param name="instanceParameters"></param>
		//public (bool isSuccess, IEnumerable<string> invalidParameters) ValidateParameters(List<InstanceParameter> instanceParameters)
		//{
		//	//look for required parameters that have not been set
		//	IEnumerable<string> missingParameters =
		//		instanceParameters
		//			.Where(p => p.IsRequired && p.InstanceValue is null)
		//			.Select(p => p.Name);

		//	if (missingParameters.Any())
		//		return (false, missingParameters);

		//	return (true, null);
		//}

	}



}