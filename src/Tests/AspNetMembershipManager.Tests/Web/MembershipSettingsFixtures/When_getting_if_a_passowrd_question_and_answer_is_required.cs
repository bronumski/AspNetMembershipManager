using System;
using System.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipSettingsFixtures
{
	class When_getting_if_a_passowrd_question_and_answer_is_required : When_getting_membership_settings<bool>
	{
		protected override void SetupContext()
		{
			GetDependency<MembershipProvider>().RequiresQuestionAndAnswer.Returns(true);
		}

		[Test]
		public void Should_return_the_requires_question_and_answer_value_from_the_membership_provider()
		{
			Result.Should().BeTrue();
		}

		protected override Func<bool> ActWithResult(MembershipSettings classUnderTest)
		{
			return () => classUnderTest.RequiresQuestionAndAnswer;
		}
	}
}