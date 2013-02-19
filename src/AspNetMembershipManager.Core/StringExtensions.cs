using System.Globalization;

namespace AspNetMembershipManager
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string stringToCheck)
		{
			return string.IsNullOrEmpty(stringToCheck);
		}

		public static bool IsNotNullOrEmpty(this string stringToCheck)
		{
			return ! string.IsNullOrEmpty(stringToCheck);
		}

		public static string Composite(this string formatString, params object[] parameters)
		{
			return string.Format(CultureInfo.InvariantCulture, formatString, parameters);
		}
	}
}