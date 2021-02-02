using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VDT.Common.ServiceProvider.Enums;
using VDT.Common.ServiceProvider.Interfaces;
using VDT.Common.ServiceProvider.Models;
using VDT.Core.Enums;
using VDT.Utilities;
using VDT.Utilities.Collections;
using VDT.Utilities.Types;

namespace VDT.Common.ServiceProvider
{
	//public class InternalTokenProvider : ITokenProvider
	//{
	//	private class ProviderConfiguration
	//	{
	//		internal Uri UriBase { get; set; }
	//		public Uri UriToken { get; set; }
	//		internal GrantTypes GrantType { get; set; }
	//		/// <summary>
	//		/// The client-secret credentials for accessing the api
	//		/// <para>Used for 'client_credentials` and 'password' grant types</para>
	//		/// </summary>
	//		internal NetworkCredential Credentials { get; set; }
	//		/// <summary>
	//		/// The password credentials for accessing the api
	//		/// The identity for accessing resources "on behalf of"
	//		/// <para>Used for 'client_credentials` and 'password' grant types</para>
	//		/// </summary>
	//		//internal NetworkCredential PasswordCredentials { get; set; }

	//		/// <summary>
	//		/// defaults to 10
	//		/// </summary>
	//		public int ConnectionLimit { get; set; } = 10;
	//	}

	//	private HttpClient api = null;
	//	private ProviderConfiguration Config { get; set; }
	//	//private EndpointManager EndpointStore { get; set; }
	//	public TokenModel Token { get; set; }

	//	//internal InternalTokenProvider(Uri uriApi, NetworkCredential credentials, Uri tokenPath)
	//	//	: this(uriApi, credentials, tokenPath)
	//	//{

	//	//}

	//	internal InternalTokenProvider(Uri uriApi, NetworkCredential credentials, Uri tokenPath)
	//	{
	//		try
	//		{
	//			Config = new ProviderConfiguration
	//			{
	//				UriBase = uriApi,
	//				//GrantType = grantType,
	//				Credentials = credentials,
	//				UriToken = tokenPath
	//			};

	//			SetApi();

	//			//retrieve auth tokens
	//			GetTokenAsync().Wait();
	//		}
	//		catch (Exception)
	//		{

	//		}
	//	}

	//	private void SetApi()
	//	{
	//		//setup http client for authentication
	//		api = new HttpClient();
	//		api.BaseAddress = Config.UriBase;
	//		api.DefaultRequestHeaders.Clear();
	//		api.DefaultRequestHeaders.ConnectionClose = false;
	//		//api.DefaultRequestHeaders.ConnectionClose = true; //TODO: verify correct value
	//		api.DefaultRequestHeaders.Accept.Add(
	//			new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
	//		api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic");
	//		SetServicePoint();
	//	}

	//	private void SetServicePoint()
	//	{
	//		ServicePoint servicePoint = ServicePointManager.FindServicePoint(api.BaseAddress);
	//		servicePoint.UseNagleAlgorithm = false;
	//		servicePoint.Expect100Continue = false;
	//		servicePoint.ConnectionLimit = Config.ConnectionLimit; //TODO: validate value (10)
	//	}

	//	public async Task<TokenModel> GetTokenAsync()
	//	{

	//		try
	//		{
	//			HttpRequestMessage request =
	//				new FormUrlEncodedRequestMessage(
	//					Config.UriToken,
	//					getContent())
	//				.ToRequest();


	//			HttpResponseMessage response =
	//				await api.SendAsync(request);

	//			if (!response.IsSuccessStatusCode)
	//				return TokenModel.Empty(); //TODO: handle

	//			TokenModel token =
	//				await response.Content.ReadAsAsync<TokenModel>();

	//			if (response is null)
	//				return TokenModel.Empty(); //TODO: handle

	//			return token;

	//			IEnumerable<KeyValuePair<string, string>> getContent() =>
	//				new KeyValuePair<string, string>("client_id", Config.Credentials.UserName)
	//				.StartEnumerable(
	//					new KeyValuePair<string, string>("client_secret", Config.Credentials.Password),
	//					new KeyValuePair<string, string>("grant_type", "client_credentials"));

	//		}
	//		catch (Exception)
	//		{
	//			throw;
	//		}

	//		//AuthenticationHeaderValue getAuthHeader() =>
	//		//	new AuthenticationHeaderValue(
	//		//		"client_credentials",
	//		//		$"{Config.Credentials.UserName}:{Config.Credentials.Password}"
	//		//			.ToBase64());
	//	}

	//	//public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
	//	//{

	//	//	HttpResponseMessage response =
	//	//		await api.SendAsync(request);

	//	//	if (!response.IsSuccessStatusCode)
	//	//		return default;

	//	//	T payload =
	//	//			await response.Content.ReadAsAsync<T>();
	//	//	return payload;
	//	//}

	//	//internal AuthenticationHeaderValue AuthenticationHeader() =>
	//	//	new AuthenticationHeaderValue(
	//	//			"Bearer",
	//	//			Token.Token);


	//}

}
