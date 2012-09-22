using System.ComponentModel;
using System.IO;

namespace AspMembershipManager.Initialization
{
	class InitializationModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string configurationPath;

		public string ConfigurationPath
		{
			get { return configurationPath; }
			set
			{
				configurationPath = value;
				OnPropertyChanged("ConfigurationPath");
                OnPropertyChanged("CanLoad");
			}
		}

	    public bool CanLoad
	    {
            get { return File.Exists(configurationPath); }
	    }

		public bool CreateMembershipDatabases { get; set; }
			
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}