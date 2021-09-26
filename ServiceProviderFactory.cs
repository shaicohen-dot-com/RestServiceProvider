using System;
using System.Net;
using RestServiceProviderServiceProvider.ApiSpeciification;
using RestServiceProviderServiceProvider.Interfaces;

namespace RestServiceProviderServiceProvider
{
	public static class ServiceProviderFactory
	{
		public static IInternalServiceProvider StartProvider(Uri uriApi, IAuthenticationManager authenticationManager, string apiJsonDefinitionFilePath) =>
			new InternalServiceProvider(
				uriApi: uriApi,
				specificationProvider: new JsonSpecificationProvider(apiJsonDefinitionFilePath),
				authenticationManager: authenticationManager);
	}

}