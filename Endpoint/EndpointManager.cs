using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using RestServiceProviderServiceProvider.Endpoint.Entities;
using RestServiceProviderServiceProvider.Endpoint.Enums;
using RestServiceProviderServiceProvider.Models.Endpoint;

namespace RestServiceProviderServiceProvider.Endpoint
{
	internal class EndpointManager
	{
		public IEnumerable<ApiResourceEndpoint> Endpoints { get; set; } = new List<ApiResourceEndpoint>();

		public HttpRequestMessage BuildRequest(string operationName, IEnumerable<ParameterValue> parameterValues) =>
			BuildRequest(new ApiRequestPayload(
				endpoint: Endpoints.First(e => e.ContainsOperation(operationName)),
				operationName: operationName,
				parameterValues: parameterValues));

		public HttpRequestMessage BuildRequest(ApiRequestPayload payload)
		{

			//create new set of InstanceParameter data for each request,
			//isolating parameters/values to a single request in an async workflow
			IEnumerable<IInstanceParameter> instanceParameters = 
				GetInstanceParameters(
					operationParameters: payload.Operation.Parameters, 
					parameterValues: payload.ParameterValues);

			//ValidateParameters(instanceParameters);
			return new HttpRequestMessage
			{
				Method = getMethod(),
				RequestUri = getRequestUri(),
				Content = getContent()
			};

			HttpMethod getMethod() =>
				new HttpMethod(payload.Operation.Method.ToString());

			Uri getRequestUri()
			{
				string path = GetPath(payload.Endpoint.Path, instanceParameters);
				string query = GetQueryString(instanceParameters);
				if (!string.IsNullOrWhiteSpace(query))
					path = $"{path}?{query}";

				return new Uri(path, UriKind.Relative);
			}

			HttpContent getContent() =>
				GetBodyContent(instanceParameters);

		}

		public bool HasRequest(string name) =>
			Endpoints.Any(e => e.ContainsOperation(name));

		private string GetQueryString(IEnumerable<IInstanceParameter> instanceParameters) => //join query parameters by "&". 
			string.Join("&", instanceParameters //if there are no query parameters, output is an empty string
			.Where(ip => ip.Location == ParameterLocation.Query)
				.Select(ip => ip.Value).ToArray());

		private string GetPath(string path, IEnumerable<IInstanceParameter> instanceParameters) =>
			!instanceParameters.Any(p => p.Location == ParameterLocation.Path) ?
				path //no path parameters, return untouched path
				: instanceParameters //replace path parameters ("{id}") using parameter name ("id") as the key for getting the value
					.Where(ip => ip.Location == ParameterLocation.Path)
					.Aggregate(path, (pathNew, parameterValue) =>
							pathNew.Replace($"{{{parameterValue.Name}}}", parameterValue.Value));

		private HttpContent GetBodyContent(IEnumerable<IInstanceParameter> instanceParameters) =>
			!instanceParameters.Any(p => p.Location == ParameterLocation.Body) ?
				null
				: new StringContent(
					instanceParameters?.FirstOrDefault(
						p => p.Location == ParameterLocation.Body)?
					.Value);		

		//private HttpContent getBodyContent(IEnumerable<IInstanceParameter> instanceParameters, ApiRequestPayload payload)
		//{
		//	if (!payload.Operation.HasParameters(ParameterLocation.Body))
		//		return null;

		//	IInstanceParameter parameter = 
		//		instanceParameters.FirstOrDefault(
		//			p => p.Location == ParameterLocation.Body);

		//	if (parameter is null)
		//		return null;

		//	return new StringContent(
		//		parameter.Value);
		//}

		/// <summary>
		/// attempts to retrieve the parameter value for the specified parameter name 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="payload"></param>
		/// <returns></returns>
		private object GetParameterValue(string name, IEnumerable<ParameterValue> values) =>
			values.FirstOrDefault(v =>
				v.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				?.Value;

		//private bool TryGetParameterValue<T>(string name, IEnumerable<ParameterValue> values, out T value) where T : class:
		//{
		//	object valueAsObject = getParameterValue(name, values);
		//	if (valueAsObject is null || (valueAsObject as T) is null)
		//	{
		//		value = default(T);
		//		return false;
		//	}

		//	value = (valueAsObject as T);
		//	return true;

		//}
		//private bool TryGetParameterValue<T>(string name, IEnumerable<ParameterValue> values, out T value) where T : struct
		//{
		//	object valueAsObject = getParameterValue(name, values);
		//	if (valueAsObject is null || (valueAsObject as T) is null)
		//	{
		//		value = default(T);
		//		return false;
		//	}

		//	value = (valueAsObject as T);
		//	return true;

		//}

		//private IEnumerable<IInstanceParameter> GetInstanceParameters(ApiRequestPayload payload)
		private IEnumerable<IInstanceParameter> GetInstanceParameters(IEnumerable<OperationParameter> operationParameters, IEnumerable<ParameterValue> parameterValues)
		{
			List<IInstanceParameter> outputParameters = new List<IInstanceParameter>();
			foreach (OperationParameter operationParameter in operationParameters)
			{
				object parameterValue = GetParameterValue(operationParameter.Name, parameterValues);
				switch (operationParameter.Type)
				{
					case ParameterValueType.Boolean:
						if (!bool.TryParse(parameterValue.ToString(), out bool isBool))
							break;
						outputParameters.Add(
							OutputParameterFactory.Retrieve(operationParameter, isBool));
						continue;
					case ParameterValueType.String:
						if (parameterValue is null)
							break;
						outputParameters.Add(
							OutputParameterFactory.Retrieve(operationParameter, parameterValue.ToString()));
						continue;
					case ParameterValueType.DateTime:
						if (!DateTime.TryParse(parameterValue.ToString(), out DateTime dateTimeParsed))
							break;
						outputParameters.Add(
							OutputParameterFactory.Retrieve(operationParameter, dateTimeParsed));
						continue;
					case ParameterValueType.Array:
						if (parameterValue is null || (parameterValue as Array) is null)
							break;
						outputParameters.Add(
							OutputParameterFactory.Retrieve(operationParameter, (parameterValue as Array)));
						continue;
					case ParameterValueType.None:
					default:
						break;
				}
				throw new ArgumentException($"Value cannot be parsed as type {operationParameter.Type}: {parameterValue}", $"{operationParameter.Name}");
			}
			return outputParameters;
		}

	}
}