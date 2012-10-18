using System.Globalization;

namespace AspNetMembershipManager
{
    public static class StringExtensions
    {
         public static string Format(this string formatString, params object[] parameters)
         {
             return string.Format(CultureInfo.InvariantCulture, formatString, parameters);
         }
    }
}