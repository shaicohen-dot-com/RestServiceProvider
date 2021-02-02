using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VDT.Common.ServiceProvider.Enums;
using VDT.Common.ServiceProvider.Interfaces;
using VDT.Common.ServiceProvider.Models;
using VDT.Utilities.Logging;

namespace VDT.Common.ServiceProvider.Providers
{
	public abstract class OAuthTokenProviderBase : ITokenProvider
	{
		private HttpClient api = null;
		public TokenModel Token { get; set; }
		internal Uri UriBase { get; set; }
		internal Uri UriToken { get; set; }
		internal GrantTypes GrantType { get; set; }
		/// <summary>
		/// The client-secret credentials for accessing the api
		/// <para>Used for 'client_credentials` and 'password' grant types</para>
		/// </summary>
		internal NetworkCredential CredentialInternal { get; set; }
		/// <summary>
		/// The password credentials for authorizing a resource owner
		/// The identity for accessing resources "on behalf of"
		/// <para>Used for 'password' grant types</para>
		/// </summary>
		internal NetworkCredential CredentialResourceOwner { get; set; }

		/// <summary>
		/// defaults to 10
		/// </summary>
		public int ConnectionLimit { get; set; } = 10;

		protected OAuthTokenProviderBase(Uri uriApi, Uri tokenPath)
			: this(GrantTypes.ClientCredential, uriApi, null, null, tokenPath, null) { }

		protected OAuthTokenProviderBase(Uri uriApi, NetworkCredential credential, Uri tokenPath)
			: this(GrantTypes.ClientCredential, uriApi, credential, null, tokenPath, null) { }

		protected OAuthTokenProviderBase(Uri uriApi, NetworkCredential credentialInternal, NetworkCredential credentialResourceOwner, Uri tokenPath, Uri refreshPath)
			: this(GrantTypes.Password, uriApi, credentialInternal, credentialResourceOwner, tokenPath, refreshPath) { }

		private OAuthTokenProviderBase(GrantTypes grantType, Uri uriApi, NetworkCredential credentialInternal, NetworkCredential credentialResourceOwner, Uri tokenPath, Uri refreshPath)
		{
			try
			{
				GrantType = grantType;
				UriBase = uriApi;
				UriToken = tokenPath;
				CredentialInternal = credentialInternal ?? new NetworkCredential();
				CredentialResourceOwner = credentialResourceOwner ?? new NetworkCredential();

				SetApi();

			}
			catch (Exception)
			{
				throw;
			}
		}

		private void SetApi()
		{
			//setup http client for authentication
			api = new HttpClient();
			api.BaseAddress = UriBase;
			api.DefaultRequestHeaders.Clear();

			api.DefaultRequestHeaders.ConnectionClose = false;
			//api.DefaultRequestHeaders.ConnectionClose = true; //TODO: verify correct value
			api.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			api.DefaultRequestHeaders.Connection.Add("keep-alive");
			//api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic");
			SetServicePoint();
		}

		private void SetServicePoint()
		{
			ServicePoint servicePoint = ServicePointManager.FindServicePoint(api.BaseAddress);
			servicePoint.UseNagleAlgorithm = false;
			servicePoint.Expect100Continue = false;
			servicePoint.ConnectionLimit = ConnectionLimit; //TODO: validate value (10)
		}

		protected abstract IEnumerable<KeyValuePair<string, string>> Content { get; }
		//protected abstract IEnumerable<KeyValuePair<string, string>> GenerateContent(NetworkCredential credential);

		protected virtual AuthenticationHeaderValue AuthenticationHeader =>
			api.DefaultRequestHeaders.Authorization;

		public async Task<TokenModel> GetTokenAsync() =>
			await GetTokenAsync(
				new FormUrlEncodedRequestMessage(
					UriToken,
					Content,
					AuthenticationHeader)
				.ToRequest());

		private async Task<TokenModel> GetTokenAsync(HttpRequestMessage requestMessage)
		{

			HttpResponseMessage response = null;

			try
			{
				response =
					await api.SendAsync(requestMessage);

				if (!response.IsSuccessStatusCode)
					throw new Exception("No Token");

				TokenModel token =
					await response.Content.ReadAsAsync<TokenModel>();

				if (response is null)
					return TokenModel.Empty(); //TODO: handle

				return token;

			}
			catch (Exception ex) when (ex.IsNotLogged())
			{
				ex.SetMethod(signature())
					.AddInstanceValue(() => requestMessage)
					.AddInstanceValue(() => response)
					.Log("Error retrieving a token.");
				throw;

				(string, string) signature() =>
					(nameof(OAuthTokenProviderBase),
					$@"private async Task<{nameof(TokenModel)}> {nameof(GetTokenAsync)} (
						HttpRequestMessage {nameof(requestMessage)}");
			}

		}

		public virtual async Task<TokenModel> GenerateTokenAsync(NetworkCredential credentialResourceOwner)
		{
			CredentialResourceOwner = credentialResourceOwner;
			return await GetTokenAsync(
				new FormUrlEncodedRequestMessage(
					UriToken,
					Content,
					AuthenticationHeader)
				.ToRequest());
		}

	}

	//public class AuthenticationHeaderValue
	//{

	//	public string Parameter { get; set; }
	//	public string Scheme { get; set; }
	//	public AuthenticationHeaderValue()
	//	{

	//	}
	//	public AuthenticationHeaderValue(string scheme, string parameter)
	//	{
	//		Parameter = parameter;
	//		Scheme = scheme;
	//	}
	//}

}