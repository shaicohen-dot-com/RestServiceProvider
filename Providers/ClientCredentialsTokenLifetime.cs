using System;
using VDT.Common.ServiceProvider.Interfaces;
using VDT.Common.ServiceProvider.Models;
using VDT.Utilities.Logging;

namespace VDT.Common.ServiceProvider.Providers
{
	public class ClientCredentialsTokenLifetime : ITokenLifetime 
	{
		private int secondsBeforeExpiration = 3600;
		private System.Timers.Timer updateTimer = new System.Timers.Timer();
		private ITokenProvider TokenRetriever = null;

		public ClientCredentialsTokenLifetime(ITokenProvider tokenRetriever)
		{
			updateTimer.Elapsed += UpdateTimer_Elapsed;
			updateTimer.AutoReset = false; //run once
			TokenRetriever = tokenRetriever;
			GetToken();
		}

		private void UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			GetToken();
		}

		private async void GetToken()
		{
			TokenModel token =
				await TokenRetriever.GetTokenAsync();

			OnTokenLifetimeEvent(new TokenLifetimeEventArgs(token.TokenType, token.Token));
			SetTimer(token.ExpiresIn);
		}

		private void SetTimer(long expiresInLong)
		{
			new Exception().SetMethod(nameof(ClientCredentialsTokenLifetime), nameof(SetTimer)).AddInstanceValue(() => expiresInLong).Log($"Token Timer Elapsed: {DateTimeOffset.UtcNow}");
			int expiresIn = ConvertOAuthTokenExpiresInFromLongToInt32(expiresInLong);
			//new Exception().Log($"Token Timer Elapsed: {DateTimeOffset.UtcNow} | expireInLong: {expiresInLong} | expiresIn: {expiresIn} | Interval: {expiresIn - secondsBeforeExpiration} | secondsBeforeExpiration: {secondsBeforeExpiration}");
			updateTimer.Stop();
			updateTimer.Interval = expiresIn - secondsBeforeExpiration;
			updateTimer.Start();
		}

		private int ConvertOAuthTokenExpiresInFromLongToInt32(long expiresLong)
		{
			if (expiresLong > int.MaxValue)
				return int.MaxValue;
			if (expiresLong < int.MinValue)
				return int.MinValue;

			return (int)expiresLong;
		}

		public event EventHandler<TokenLifetimeEventArgs> TokenLifetimeEvent;

		protected virtual void OnTokenLifetimeEvent(TokenLifetimeEventArgs args)
		{
			EventHandler<TokenLifetimeEventArgs> handler = TokenLifetimeEvent;
			if (handler != null) handler(this, args);
		}
	}

}