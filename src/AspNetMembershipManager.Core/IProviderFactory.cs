using System.Configuration;
using System.Configuration.Provider;

namespace AspNetMembershipManager
{
	public interface IProviderFactory
	{
		TProvider CreateProviderFromConfig<TProvider>(ProviderSettings settings) where TProvider : ProviderBase;
	}
}