using System;

namespace VDT.Common.ServiceProvider.Interfaces
{
	public interface ITokenLifetime
	{
		event EventHandler<TokenLifetimeEventArgs> TokenLifetimeEvent;
	}

}