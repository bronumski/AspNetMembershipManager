using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AspNetMembershipManager.User.Profile
{
	/// <summary>
	/// Interaction logic for ProfileCollectionPropertyWindow.xaml
	/// </summary>
	partial class ProfileCollectionPropertyWindow : EditDialogWindow
	{
		private readonly IProfileProperty profileProperty;
		private Type arrayDataType;

		internal ProfileCollectionPropertyWindow(Window parentWindow, IProfileProperty profileProperty)
		{
			this.profileProperty = profileProperty;
			Owner = parentWindow;

			InitializeComponent();

			ProfileCollectionProperty = new ProfileCollectionPropertyViewModel(profileProperty);
		}

		private ProfileCollectionPropertyViewModel ProfileCollectionProperty
		{
			set { DataContext = value; }
            get { return (ProfileCollectionPropertyViewModel)DataContext; }
		}

		private void CustomProfilePropertyEdit(object sender, RoutedEventArgs e)
		{
            var profileProperty = (IProfileProperty)((Button)sender).DataContext;

			var propertyWindow = new ProfilePropertyWindow(this, profileProperty);

			propertyWindow.ShowDialog();
		}
		
		private void CollectionPropertyEdit(object sender, RoutedEventArgs e)
		{
            var profileProperty = (IProfileProperty)((Button)sender).DataContext;

			var propertyWindow = new ProfileCollectionPropertyWindow(this, profileProperty);

			propertyWindow.ShowDialog();
		}

		private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
		{
			ProfileCollectionProperty.AddNewItem();
		}

		private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
		{
			var item = (IProfileProperty)((Button) sender).DataContext;

			ProfileCollectionProperty.RemoveItem(item);
		}

		protected override bool OnOkExecuted()
		{
			try
			{
				var dataToCopy = ProfileCollectionProperty.Values.Select(x => x.PropertyValue).ToArray();
				arrayDataType = profileProperty.PropertyType.GetElementType();
				Array newData = Array.CreateInstance(arrayDataType, dataToCopy.Length);

				for(int i = 0; i < dataToCopy.Length; i++)
				{
					newData.SetValue(Convert.ChangeType(dataToCopy[i], arrayDataType), i);
				}

				profileProperty.PropertyValue = newData;
			}
			catch (Exception ex)
			{
				ProfileCollectionProperty.Error = ex.Message;
				return false;
			}

        	return base.OnOkExecuted();
		}
	}
}
