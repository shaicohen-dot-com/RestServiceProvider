using System;
using System.Collections.Generic;
using System.Net;
using RestServiceProviderServiceProvider.Providers;

namespace RestServiceProviderServiceProvider.Providers
{
	public class ClientCredentialsTokenProvider : OAuthTokenProviderBase
	{
		public ClientCredentialsTokenProvider(Uri uriApi, NetworkCredential credentials, Uri tokenPath)
			: base(uriApi, credentials, tokenPath) { }

		public ClientCredentialsTokenProvider(Uri uriApi, Uri tokenPath)
			: base(uriApi, tokenPath) { }

		protected override IEnumerable<KeyValuePair<string, string>> Content =>
			new KeyValuePair<string, string>[] {
				new KeyValuePair<string, string>("grant_type", "client_credentials"),
				new KeyValuePair<string, string>("client_id", CredentialInternal.UserName),
				new KeyValuePair<string, string>("client_secret", CredentialInternal.Password)};
	}
}