using System.ComponentModel;

namespace AspNetMembershipManager.User
{
    class CreateUserModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string EmailAddress { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    	public string this[string columnName]
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

        private string error;
    	public string Error 
    	{
    		get { return error; }
            set
            {
                error = value;
                OnPropertyChanged("Error");
            }
    	}
    }
}