using System.Web.Configuration;
using AspNetMembershipManager.Web.Security;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	[TestFixture]
	abstract class RoleManagerFixtureBase<TResult> : AutoMockedSpecificationFor<RoleManager, TResult>
	{
		protected override void SetupDependencies()
		{
			base.SetupDependencies();

			var roleManagerSection = new RoleManagerSection();

			RegisterDependency(roleManagerSection);
		}
	}
}