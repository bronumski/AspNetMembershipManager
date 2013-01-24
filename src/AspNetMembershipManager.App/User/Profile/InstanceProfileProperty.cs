using System;

namespace AspNetMembershipManager.User.Profile
{
	class InstanceProfileProperty : IProfileProperty
	{
		public InstanceProfileProperty(object value)
		{
			PropertyValue = value;
			PropertyType = value.GetType();
		}

		public string PropertyName { get; private set; }

		public Type PropertyType { get; private set; }

		public object PropertyValue { get; set; }
	}
}