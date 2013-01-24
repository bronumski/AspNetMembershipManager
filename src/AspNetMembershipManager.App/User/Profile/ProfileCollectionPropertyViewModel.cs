using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AspNetMembershipManager.User.Profile
{
	class ProfileCollectionPropertyViewModel : SaveViewModelBase
	{
		private readonly ObservableCollection<IProfileProperty> values;

		public ProfileCollectionPropertyViewModel(IProfileProperty profileProperty)
		{
			Name = profileProperty.PropertyName;
			Type = profileProperty.PropertyType;

			values = CreateObservableCollection(profileProperty);
		}

		private ObservableCollection<IProfileProperty> CreateObservableCollection(IProfileProperty profileProperty)
		{
			if (profileProperty.PropertyValue == null)
			{
				return new ObservableCollection<IProfileProperty>();
			}

			var v = (Array) profileProperty.PropertyValue;
			return new ObservableCollection<IProfileProperty>(v.Cast<object>().Select(x => new InstanceProfileProperty(x)));
		}

		public bool IsSupportedDataType
		{
			get
			{
				if (Type.IsArray)
				{
					var elementType = Type.GetElementType();
					if (SupportedDataTypes().Contains(elementType))
					{
						return true;
					}

					var constructor = elementType.GetConstructor(new Type[0]);

					return constructor != null;
				}
				return false;
			}
		}

		private IEnumerable<Type> SupportedDataTypes()
		{
			return SupportedSystemNumericTypes()
				.Union(new[] {typeof (string), typeof (DateTime)});
		}

		private IEnumerable<Type> SupportedSystemNumericTypes()
		{
			yield return typeof (sbyte);
			yield return typeof (byte);
			yield return typeof (short);
			yield return typeof (ushort);
			yield return typeof (int);
			yield return typeof (uint);
			yield return typeof (long);
			yield return typeof (ulong);
			yield return typeof (float);
			yield return typeof (double);
			yield return typeof (decimal);
			yield return typeof (bool);
		}
 

		public string Name { get; private set; }

		public Type Type { get; private set; }

		public IEnumerable<IProfileProperty> Values { get { return values; } }

		public void RemoveItem(IProfileProperty item)
		{
			values.Remove(item);
		}

		public void AddNewItem()
		{
			if (Type.IsArray)
			{
				object value = null;
				
				var elementType = Type.GetElementType();

				if (elementType == typeof(string))
				{
					value = string.Empty;
				}
				else if (elementType == typeof(DateTime))
				{
					value = DateTime.MinValue;
				}
				else if (SupportedSystemNumericTypes().Contains(elementType))
				{
					value = Convert.ChangeType(0, elementType);
				}
				else
				{
					var constructor = elementType.GetConstructor(new Type[0]);
					if (constructor != null)
					{
						value = constructor.Invoke(new object[0]);
					}
				}

				if (value != null)
				{
					values.Add(new InstanceProfileProperty(value));
				}
			}

			OnPropertyChanged("Values");
		}

		public override string this[string columnName]
		{
			get { return string.Empty; }
		}
	}
}