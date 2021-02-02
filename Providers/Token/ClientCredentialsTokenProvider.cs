using System;
using System.Collections.Generic;
using System.Net;
using VDT.Common.ServiceProvider.Providers;
using VDT.Utilities.Collections;

namespace VDT.Common.ServiceProvider.Providers
{
	public class ClientCredentialsTokenProvider : OAuthTokenProviderBase
	{
		public ClientCredentialsTokenProvider(Uri uriApi, NetworkCredential credentials, Uri tokenPath)
			: base(uriApi, credentials, tokenPath) { }

		public ClientCredentialsTokenProvider(Uri uriApi, Uri tokenPath)
			: base(uriApi, tokenPath) { }

		protected override IEnumerable<KeyValuePair<string, string>> Content =>
			new KeyValuePair<string, string>("grant_type", "client_credentials")
				.StartEnumerable(
					new KeyValuePair<string, string>("client_id", CredentialInternal.UserName),
					new KeyValuePair<string, string>("client_secret", CredentialInternal.Password));
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