using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using RestServiceProviderServiceProvider.ApiSpeciification;
using RestServiceProviderServiceProvider.Endpoint;
using RestServiceProviderServiceProvider.Endpoint.Entities;
using RestServiceProviderServiceProvider.Interfaces;

namespace RestServiceProviderServiceProvider
{

	internal class InternalServiceProvider : IVdtServiceProvider
	{
		private HttpClient apiResource = null;
		private EndpointManager EndpointStore { get; set; }
		private IAuthenticationManager AuthenticationManager { get; set; }
		private Uri UriApi { get; set; } = null;
		/// <summary>
		/// defaults to 10
		/// </summary>
		public int ConnectionLimit { get; set; } = 10;

		internal InternalServiceProvider(Uri uriApi, IAuthenticationManager authenticationManager, IApiSpecificationProvider specificationProvider)
		{
			if (!uriApi.IsAbsoluteUri)
				throw new Exception("uriBase is not absolute");

			Uri uriApiValidated = new Uri(
				uriApi.OriginalString.EndsWith("/")
					? uriApi.OriginalString
					: $"{uriApi.OriginalString}/"
				, UriKind.Absolute);

			UriApi = uriApiValidated;
			EndpointStore = specificationProvider.ApiDefinition;
			AuthenticationManager = authenticationManager;
			SetApi();

		}
		private void SetApi()
		{
			//setup http client for the api
			apiResource = new HttpClient();
			apiResource.BaseAddress = UriApi;
			apiResource.DefaultRequestHeaders.Clear();
			apiResource.DefaultRequestHeaders.ConnectionClose = false;
			apiResource.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			SetServicePoint(apiResource.BaseAddress);
		}

		private void SetServicePoint(Uri uri)
		{
			ServicePoint servicePoint = ServicePointManager.FindServicePoint(uri);
			servicePoint.UseNagleAlgorithm = false;
			servicePoint.Expect100Continue = false;
			servicePoint.ConnectionLimit = ConnectionLimit;
		}

		public async Task<T> RunAsync<T>(string operationName, IEnumerable<ParameterValue> parameterValues = null)
		{
			if (!EndpointStore.HasRequest(operationName))
				throw new ArgumentException($"Operation {operationName} was not found", nameof(operationName));

			HttpRequestMessage request = EndpointStore.BuildRequest(operationName, parameterValues);
			T result =
				await ProcessRequest<T>(request);

			return result;
		}

		public async Task<T> ProcessRequest<T>(HttpRequestMessage request)
		{
			request.Headers.Authorization =
				await AuthenticationManager.GetAuthenticationHeader();

			if (request.RequestUri.IsAbsoluteUri)
				throw new Exception($"HttpRequestMessage.RequestUri must be UriKind.Relative");

			EnsurePathHasNoLeadingForwardSlashes();

			HttpResponseMessage response =
				await apiResource.SendAsync(request);

			if (!response.IsSuccessStatusCode)
				return default;

			T payload =
					await JsonSerializer.DeserializeAsync<T>(
						await response.Content.ReadAsStreamAsync());

			return payload;

			void EnsurePathHasNoLeadingForwardSlashes()
			{
				string requestPath = request.RequestUri.OriginalString;
				while (requestPath.StartsWith("/"))
				{
					requestPath = requestPath.Remove(0, 1);
				}
				request.RequestUri = new Uri(requestPath, UriKind.Relative);
			}
		}

	}

}