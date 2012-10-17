using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.TypeNameFixtures
{
    [TestFixture]
    class When_loading_a_type_name_without_a_fully_quallifed_name
    {
        private TypeName typeName;

        [SetUp]
        public void SetUp()
        {
            typeName = new TypeName("Wibble.Wobble");
        }

        [Test]
        public void Should_load_the_type_name()
        {
            typeName.Name.Should().Be("Wibble.Wobble");
        }

        [Test]
        public void Should_not_have_an_assembly_name()
        {
            typeName.AssemblyName.Should().BeNull();
        }
    }
}