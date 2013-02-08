using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.MapperExtensionsFixture
{
    [TestFixture]
    public class When_creating_a_converter_from_a_mapper
    {
        [Test]
        public void Should_create_a_converter_of_the_correct_type()
        {
            var mapper = Substitute.For<IMapper<bool, string>>();
            
            mapper.GetConverter().Should().BeOfType<Converter<bool, string>>();
        }
    }
}
