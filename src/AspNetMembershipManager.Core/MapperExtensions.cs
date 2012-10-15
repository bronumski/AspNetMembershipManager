using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetMembershipManager
{
	public static class MapperExtensions
	{
		public static IEnumerable<TOutput> MapAll<TInput, TOutput>(this IMapper<TInput, TOutput> mapper, IEnumerable<TInput> input)
		{
			return input.Select(mapper.Map);
		}

		public static Converter<TInput, TOutput> GetConverter<TInput, TOutput>(this IMapper<TInput, TOutput> mapper)
		{
			return mapper.Map;
		}
	}
}