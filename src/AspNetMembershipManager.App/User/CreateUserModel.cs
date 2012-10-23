using System.Linq;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
    class CreateUserModel : SaveViewModelBase
    {
    	private readonly IMembershipSettings membershipSettings;

    	public CreateUserModel(IMembershipSettings membershipSettings)
    	{
    		this.membershipSettings = membershipSettings;
    	}

    	public string Username { get; set; }

		public string EmailAddress { get; set; }

    	public string Password { get; set; }

    	public bool RequiresQuestionAndAnswer
    	{
    		get { return membershipSettings.RequiresQuestionAndAnswer; }
    	}

    	public string PasswordQuestion { get; set; }

    	public string PasswordQuestionAnswer { get; set; }

    	public override string this[string columnName]
    	{
    		get
    		{
    			switch (columnName)
    			{
                    case "Username":
                        if (string.IsNullOrEmpty(Username))
						{
							return "Please enter a unique username";
						}
						break;
					case "Password":
                        if (! ValidatePassword(Password))
						{
							return "Password does not meet the length or complexity requirements";
						}
						break;
    			}
    			return string.Empty;
    		}
    	}

		private bool ValidatePassword(string password)
		{
            if (string.IsNullOrEmpty(password) || password.Length < membershipSettings.MinRequiredPasswordLength)
			{
				return false;
			}
		    return membershipSettings.MinRequiredNonAlphanumericCharacters <= password.Count(c => ! char.IsLetterOrDigit(c));
		}
    }
}