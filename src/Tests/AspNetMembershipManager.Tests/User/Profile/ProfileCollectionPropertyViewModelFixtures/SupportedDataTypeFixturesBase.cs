using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AspNetMembershipManager.User.Profile.ProfileCollectionPropertyViewModelFixtures
{
	[TestFixture]
	abstract class SupportedDataTypeFixturesBase
	{
		public IEnumerable<ITestCaseData> DataTypes()
		{
			return SupportedDataTypes().Select(x => new TestCaseData(x, true))
				.Union(UnsupportedDataTypes().Select(x => new TestCaseData(x, false)));
		}

		private IEnumerable<Type> SupportedDataTypes()
		{
			yield return typeof (string);
			yield return typeof (sbyte);
			yield return typeof (byte);
			yield return typeof (short);
			yield return typeof (ushort);
			yield return typeof (int);
			yield return typeof (uint);
			yield return typeof (long);
			yield return typeof (ulong);
			yield return typeof (float);
			yield return typeof (double);
			yield return typeof (decimal);
			yield return typeof (bool);
			yield return typeof (DateTime);
		}

		private IEnumerable<Type> UnsupportedDataTypes()
		{
			yield return typeof (Foo);
		}

		class Foo
		{
			public Foo(int i)
			{
				
			}
		}
	}
}