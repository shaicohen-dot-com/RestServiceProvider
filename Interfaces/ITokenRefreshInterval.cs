using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestServiceProviderServiceProvider.Interfaces
{
	/// <summary>
	/// Provides the number of seconds to wait before attempting to refresh a token.
	/// <para>double milliseconds</para>
	/// </summary>
	interface ITokenRefreshInterval
	{
		uint RefreshIntervalInSeconds(uint tokenTimeToLive);
	}

	//public abstract class TokenRefreshIntervalBase : ITokenRefreshInterval
	//{
	//	public uint RefreshIntervalInSeconds(uint tokenTimeToLiveInSeconds)
	//	{

	//	}
	//}

	public class TimeSpanRefreshInterval : ITokenRefreshInterval
	{
		public uint NumberOfSeconds { get; set; }
		public TimeSpan IntervalWait{ get; set; }
		public TimeSpanRefreshInterval(TimeSpan intervalWait)
		{
			IntervalWait = intervalWait;
		}
		public TimeSpanRefreshInterval(uint numberOfSeconds)
		{
			NumberOfSeconds = numberOfSeconds;
		}
		public TimeSpan RefreshInterval(TimeSpan timeSpan)
		{
			throw new NotImplementedException();
			//TimeSpan intervalActual = IntervalWait
		}
		public uint RefreshIntervalInSeconds(uint tokenTimeToLive)
		{
			if (tokenTimeToLive <= NumberOfSeconds)
				return 1; //handles a potential System.ArgumentException in System.Timers.Timer if the interval is less than or equal to zero.-or-The interval is greater than System.Int32.MaxValue

			return 1; //TODO

		}
	}

}
//
// Summary:
//     Gets or sets the interval, expressed in milliseconds, at which to raise the System.Timers.Timer.Elapsed
//     event.
//
// Returns:
//     The time, in milliseconds, between System.Timers.Timer.Elapsed events. The value
//     must be greater than zero, and less than or equal to System.Int32.MaxValue. The
//     default is 100 milliseconds.
//
// Exceptions:
//   T:System.ArgumentException:
//     The interval is less than or equal to zero.-or-The interval is greater than System.Int32.MaxValue,
//     and the timer is currently enabled. (If the timer is not currently enabled, no
//     exception is thrown until it becomes enabled.)
