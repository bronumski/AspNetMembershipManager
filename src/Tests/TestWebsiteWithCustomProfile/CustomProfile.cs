using System.Web.Profile;

namespace TestWebsiteWithCustomProfile
{
    public class CustomProfile : ProfileBase
    {
        public bool BooleanValue { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public decimal DecimalValue { get; set; }
        public CustomProperty CustomProperty { get; set; }
    }

    public class CustomProperty
    {
        public string StringValue { get; set; }
    }
}