using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.MapperExtensionsFixture
{
    [TestFixture]
    public class When_mapping_an_enumerable_with_the_appropriate_mapper
    {
        private IMapper<char, string> mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = Substitute.For<IMapper<char, string>>();
        }

        [Test]
        public void Should_return_mapped_object()
        {
            var input = new[] { 'a' };

            mapper.Map('a').Returns("a");

            IEnumerable<string> output = mapper.MapAll(input);

            output.First().Should().Be("a");
        }

        [Test]
        public void Should_call_mapper_for_each_object()
        {
            var input = new[] { 'a', 'b' };

            mapper.Map('a').Returns("a");
            mapper.Map('b').Returns("b");

            mapper.MapAll(input).Should().ContainInOrder(new [] { "a", "b" });
        }
    }
}
