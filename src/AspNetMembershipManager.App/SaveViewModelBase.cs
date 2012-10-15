using System.ComponentModel;

namespace AspNetMembershipManager
{
	abstract class SaveViewModelBase : ViewModelBase, IDataErrorInfo
	{
		public abstract string this[string columnName] { get; }

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