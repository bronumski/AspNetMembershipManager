using System;
using System.Web.Profile;

namespace TestWebsiteWithCustomProfile
{
    public class CustomProfile : ProfileBase
    {
        public bool BooleanValue { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public decimal DecimalValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public CustomProperty CustomProperty { get; set; }
		public bool[] BoolArray { get; set; }
		public string[] StringArray { get; set; }
		public int[] IntArray { get; set; }
		public decimal[] DecimalArray { get; set; }
		public DateTime[] DateTimeArray { get; set; }
		public object[] ObjectArray { get; set; }
		public CustomProperty[] CustomPropertyArray { get; set; }
		public ComplexCustomProperty[] ComplexCustomPropertyArray { get; set; }
    }

    public class CustomProperty
    {
        public string StringValue { get; set; }
    }

	public class ComplexCustomProperty
    {
		public ComplexCustomProperty(string stringValue)
		{
			StringValue = stringValue;
		}

		public string StringValue { get; set; }
    }

}