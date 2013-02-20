using System;
using System.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipSettingsFixtures
{
	[TestFixture(false, false, false)]
	[TestFixture(true, false, true)]
	[TestFixture(false, true, false)]
	[TestFixture(true, true, false)]
	class When_getting_if_password_reset_is_enabled : When_getting_membership_settings<bool>
	{
		private readonly bool enablePasswordReset;
		private readonly bool requiresQuestionAndAnswer;
		private readonly bool expectedCanResetPasswordValue;

		public When_getting_if_password_reset_is_enabled(bool enablePasswordReset, bool requiresQuestionAndAnswer, bool expectedCanResetPasswordValue)
		{
			this.enablePasswordReset = enablePasswordReset;
			this.requiresQuestionAndAnswer = requiresQuestionAndAnswer;
			this.expectedCanResetPasswordValue = expectedCanResetPasswordValue;
		}

		protected override void SetupContext()
		{
			GetDependency<MembershipProvider>().EnablePasswordReset.Returns(enablePasswordReset);
			GetDependency<MembershipProvider>().RequiresQuestionAndAnswer.Returns(requiresQuestionAndAnswer);
		}

		[Test]
		public void Should_return_true_only_when_provided_password_reset_is_enabled_and_there_is_no_requirement_for_a_password_question_and_answer()
		{
			Result.Should().Be(expectedCanResetPasswordValue);
		}

		protected override Func<bool> ActWithResult(MembershipSettings classUnderTest)
		{
			return () => classUnderTest.CanResetPasswords;
		}
	}
}