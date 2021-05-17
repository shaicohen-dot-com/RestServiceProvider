using System;
using System.Security.Cryptography;
using System.Text;

namespace RestServiceProviderServiceProvider.Utilities
{
	public static class StringExtensions
	{
		/// <summary>
		/// Uses Encoding.UTF8
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static string ToBase64(this string source, bool encrypt = false) =>
			source.ToBase64(Encoding.UTF8, encrypt);

		public static string ToBase64(this string source, Encoding encoding, bool encrypt = false) =>
			encrypt ?
				 Convert.ToBase64String(
					 new SHA256CryptoServiceProvider()
						.ComputeHash(encoding.GetBytes(source)))
				:
				Convert.ToBase64String(encoding.GetBytes(source));
	}
}
