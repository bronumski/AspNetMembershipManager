using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using NUnit.Framework;

namespace AspNetMembershipManager
{
	abstract class AutoMockedSpecificationFor<TClassUnderTest, TResult> : AutoMockedSpecificationFor<TClassUnderTest> where TClassUnderTest : class
	{
		protected TResult Result { get; private set; }

		protected abstract Func<TResult> ActWithResult(TClassUnderTest classUnderTest);

		protected override Action Act(TClassUnderTest classUnderTest)
		{
			return () => Result = ActWithResult(classUnderTest)();
		}
	}

	abstract class AutoMockedSpecificationFor<TClassUnderTest> : SpecificationFor<TClassUnderTest> where TClassUnderTest : class
	{
		private IWindsorContainer mockContainer;

		protected abstract Action Act(TClassUnderTest classUnderTest);

		protected override void Because()
		{
			Act(ClassUnderTest)();
		}

		protected override void SetupDependencies()
		{
			mockContainer = new WindsorContainer();
			CustomWindsorWireup(mockContainer);
			mockContainer.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyComponentAutoMocker>());
			mockContainer.Register(RegisterClassUnderTest(Component.For<TClassUnderTest>()));
		}

		protected virtual ComponentRegistration<TClassUnderTest> RegisterClassUnderTest(ComponentRegistration<TClassUnderTest> componentRegistration)
		{
			return componentRegistration;
		}

		protected override TClassUnderTest CreateClassUnderTest()
		{
			return mockContainer.Resolve<TClassUnderTest>();
		}

		protected virtual void CustomWindsorWireup(IWindsorContainer windsorContainer)
		{
			
		}

		protected TDependency GetDependency<TDependency>()
		{
			return mockContainer.Resolve<TDependency>();
		}

		protected TDependency RegisterDependency<TDependency>(TDependency dependency) where TDependency : class
		{
			mockContainer.Register(Component.For<TDependency>().Instance(dependency));
			return dependency;
		}

		protected void RegisterDependency<TService, TDependency>() where TDependency : TService where TService : class
		{
			mockContainer.Register(Component.For<TService>().ImplementedBy<TDependency>());
		}
	}

	abstract class SpecificationFor<TClassUnderTest>
	{
		[SetUp]
		public void BaseSetUp()
		{
			SetupParameters();
			SetupDependencies();
			InitializeClassUnderTest();
			SetupContext();
			Because();
		}

		[TearDown]
		public void BaseTearDown()
		{
			DisposeContext();
		}

		protected virtual void SetupParameters() { }
		protected virtual void SetupDependencies() { }
		protected virtual void SetupContext() { }
		protected abstract void Because();
		protected virtual void DisposeContext() { }

		private void InitializeClassUnderTest()
		{
			ClassUnderTest = CreateClassUnderTest();
		}

		protected abstract TClassUnderTest CreateClassUnderTest();

		protected TClassUnderTest ClassUnderTest { get; private set; }
	}
}