using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.User
{
	/// <summary>
	/// Interaction logic for UserDetailsWindow.xaml
	/// </summary>
	partial class UserDetailsWindow : Window
	{
		private readonly UserDetailsModel userDetails;
		private readonly RoleProvider roleProvider;

		internal UserDetailsWindow(MembershipUser user, RoleProvider roleProvider)
		{
			InitializeComponent();

			this.roleProvider = roleProvider;
			userDetails = new UserDetailsModel(user, roleProvider);

			DataContext = userDetails;
		}

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
        	foreach (var userInRole in userDetails.Roles)
        	{
        		if (userInRole.IsMember)
        		{
        			if (!roleProvider.IsUserInRole(userDetails.Username, userInRole.RoleName))
        			{
        				roleProvider.AddUserToRole(userDetails.Username, userInRole.RoleName);
        			}
        		}
        		else
        		{
        			if (roleProvider.IsUserInRole(userDetails.Username, userInRole.RoleName))
        			{
        				roleProvider.RemoveUserFromRole(userDetails.Username, userInRole.RoleName);
        			}
        		}
        	}

			DialogResult = true;
            Close();
        }
	}
}
