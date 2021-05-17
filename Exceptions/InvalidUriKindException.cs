using System;
using System.Collections.Generic;
using System.Text;

namespace RestServiceProvider.Exceptions
{
	/// <summary>
	/// The exception thrown when a Uri is not the expected UriKind
	/// </summary>
	public class InvalidUriKindException : Exception
	{
		UriKind UriKindExpected { get; set; }
		UriKind UriKindReceived { get; set; }
		string Identifier { get; set; }

		public InvalidUriKindException(UriKind uriKindExpected, UriKind uriKindReceived, string identifier = "")
		{
			UriKindExpected = uriKindExpected;
			UriKindReceived = uriKindReceived;
			Identifier = identifier;
		}
	}
}
