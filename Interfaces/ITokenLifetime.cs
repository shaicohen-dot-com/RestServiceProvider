using System;

namespace RestServiceProviderServiceProvider.Interfaces
{
	public interface ITokenLifetime
	{
		event EventHandler<TokenLifetimeEventArgs> TokenLifetimeEvent;
	}

}