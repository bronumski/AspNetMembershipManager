using System.ComponentModel;

namespace AspNetMembershipManager.Role
{
    class CreateRoleModel : SaveViewModelBase
    {
        public string Name { get; set; }

    	public override string this[string columnName]
    	{
    		get
			{ 
    			switch (columnName)
    			{
					case "Name":
						if (string.IsNullOrEmpty(Name) || Name.Length < 3)
						{
							return "Please enter a valid role name";
						}
						break;
    			}
    			return string.Empty;
    		}
    	}
    }
}