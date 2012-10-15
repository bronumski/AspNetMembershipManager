namespace AspNetMembershipManager.User
{
    class CreateUserModel : SaveViewModelBase
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }

    	public override string this[string columnName]
    	{
    		get
    		{
    			switch (columnName)
    			{
					case "EmailAddress":
						if (string.IsNullOrEmpty(EmailAddress) || EmailAddress.Length < 3)
						{
							return "Please enter a valid email address";
						}
						break;
                    case "Username":
                        if (string.IsNullOrEmpty(Username))
						{
							return "Please enter a unique username";
						}
						break;
    			}
    			return string.Empty;
    		}
    	}
    }
}