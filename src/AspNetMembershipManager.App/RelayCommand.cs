using System;
using System.Windows.Input;

namespace AspNetMembershipManager
{
	class RelayCommand<T> : ICommand 
	{
		private readonly Predicate<T> canExecute;
		private readonly Action<T> execute;

		public RelayCommand(Action<T> action) : this(x => true, action)
		{ 
			execute = action; 
		}

		public RelayCommand(Predicate<T> canExecute, Action<T> execute)
		{
			this.canExecute = canExecute;
			this.execute = execute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return canExecute((T)parameter);
		}

		public void UpdateCanExecuteState()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, new EventArgs());
			}
		}

		public void Execute(object parameter) 
		{ 
			execute((T)parameter);

			UpdateCanExecuteState();
		}
	}
}