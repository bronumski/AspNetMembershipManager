using System;
using System.Collections;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using NSubstitute;

namespace AspNetMembershipManager
{
	public class LazyComponentAutoMocker : ILazyComponentLoader
	{
		public IRegistration Load(string key, Type service, IDictionary arguments)
		{
			return Component.For(service).Instance(Substitute.For(new[] { service }, null));
		}
	}
}