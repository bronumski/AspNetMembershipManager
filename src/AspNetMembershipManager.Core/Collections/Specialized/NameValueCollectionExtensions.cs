using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace AspNetMembershipManager.Collections.Specialized
{
    public static class NameValueCollectionExtensions
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            return collection.Keys.Cast<string>()
                .Where(x => x != null)
                .Select(k => new KeyValuePair<string, string>(k, collection[k]))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}