using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestServiceProviderServiceProvider.Interfaces;

namespace RestServiceProviderServiceProvider
{
	public class InternalAuthenticationManager : IAuthenticationManager
	//where R : ITokenRetrieval
	//public class AuthenticationManager : IAuthenticationManager
	{
		private AuthenticationHeaderValue _header { get; set; } // = new AuthenticationHeaderValue();
		private object _lockHeader = new Object();

		ITokenLifetime Lifetime { get; set; }
		//ITokenProvider Provider { get; set; }

		public InternalAuthenticationManager(ITokenLifetime lifetime)
		{
			//Provider = provider;
			Lifetime = lifetime;
			Lifetime.TokenLifetimeEvent += HandleTokenLifetimeEvent;
		}
		//public AuthenticationManager<R>(ClientCredentialsTokenUpdater<R> updater)
		//{
		//	Updater = updater;
		//	Updater.tokenUpdatedEvent += HandleTokenUpdated;
		//}

		public async Task<AuthenticationHeaderValue> GetAuthenticationHeader() =>
			await Task.FromResult(Header);

		private void HandleTokenLifetimeEvent(object sender, TokenLifetimeEventArgs e)
		{
			AuthenticationHeaderValue header = new AuthenticationHeaderValue(e.Scheme, e.Parameter);
			Header = header;
		}

		protected AuthenticationHeaderValue Header
		{
			get
			{
				lock (_lockHeader)
				{
					return _header;
				}
			}
			set
			{
				lock (_lockHeader)
				{
					_header = value;
				}
			}
		}

	}
	public interface IAuthenticationManager
	{
		Task<AuthenticationHeaderValue> GetAuthenticationHeader();
	}

	public class TokenLifetimeEventArgs : EventArgs
	{
		public string Parameter { set; get; }
		//public string Parameter { get; set; }
		public string Scheme { get; set; }

		public TokenLifetimeEventArgs(string scheme, string parameter)
		{
			Parameter = parameter;
			Scheme = scheme;
		}

	}

}