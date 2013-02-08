using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.TypeNameFixtures
{
    [TestFixture]
    class When_loading_a_type_name_with_a_fully_quallifed_name
    {
        private TypeName typeName;

        [SetUp]
        public void SetUp()
        {
            typeName = new TypeName("Wibble.Wobble, Foo");
        }

        [Test]
        public void Should_load_the_type_name()
        {
            typeName.Name.Should().Be("Wibble.Wobble");
        }

        [Test]
        public void Should_have_an_assembly_name()
        {
            typeName.AssemblyName.Should().NotBeNull();
        }

        [Test]
        public void Should_have_an_assembly_name_with_the_correct_assembly_name()
        {
            typeName.AssemblyName.Name.Should().Be("Foo");
        }
    }
}