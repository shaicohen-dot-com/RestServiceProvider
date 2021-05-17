using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestServiceProviderServiceProvider.Models;
using RestServiceProviderServiceProvider.Utilities;

namespace RestServiceProviderServiceProvider.Providers
{

	public class ResourceOwnerTokenProvider : OAuthTokenProviderBase
	{
		public ResourceOwnerTokenProvider(Uri uriApi, NetworkCredential credentialInternal, NetworkCredential credentialResourceOwner, Uri tokenPath, Uri refreshPath)
			: base(uriApi, credentialInternal, credentialResourceOwner, tokenPath, refreshPath) { }

		public ResourceOwnerTokenProvider(Uri uriApi, NetworkCredential credentialInternal, Uri tokenPath)
			: base(uriApi, credentialInternal, tokenPath) { }

		protected override IEnumerable<KeyValuePair<string, string>> Content =>
			new KeyValuePair<string, string>[]{
				new KeyValuePair<string, string>("grant_type", "password"), 
				new KeyValuePair<string, string>("username", CredentialResourceOwner.UserName),
				new KeyValuePair<string, string>("password", CredentialResourceOwner.Password)};

		public override async Task<TokenModel> GenerateTokenAsync(NetworkCredential credentialResourceOwner) =>
			await base.GenerateTokenAsync(credentialResourceOwner);

		protected override AuthenticationHeaderValue AuthenticationHeader =>
			new AuthenticationHeaderValue(
				"basic",
				$"{CredentialInternal.UserName}:{CredentialInternal.Password}"
					.ToBase64());

	}

}

/*				void setContent() =>
					request.Content = new FormUrlEncodedContent(
						new KeyValuePair<string, string>("grant_type", "password")
							.StartEnumerable(
								new KeyValuePair<string, string>("password", userCredentials.Password),
								new KeyValuePair<string, string>("username", userCredentials.UserName)));

				void setHeaders()
				{
					request.Headers.Add("connection", "keep-alive");
					request.Headers.Add("cache-control", "no-cache");
					request.Headers.Authorization = getAuthHeader();
					
					//request.Headers.Add("client_id", serviceCredentials.UserName);
					//request.Headers.Add("client_secret", serviceCredentials.Password);
				}
			}
			catch (Exception)
			{
				throw;
			}

			AuthenticationHeaderValue getAuthHeader() =>
				new AuthenticationHeaderValue(
					"basic",
					$"{serviceCredentials.UserName}:{serviceCredentials.Password}"
						.ToBase64());
		}
*/