using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.Initialization
{
	/// <summary>
	/// Interaction logic for InitializationWindow.xaml
	/// </summary>
	public partial class InitializationWindow : Window
	{
		public InitializationWindow()
		{
			InitializationViewModel = new InitializationViewModel();

			InitializeComponent();
		}

		private InitializationViewModel InitializationViewModel
		{
			get { return (InitializationViewModel) DataContext; }
			set { DataContext = value; }
		}

        public ProviderManagers ProviderManagers { get; private set; }

		private void BrowseForConfig(object sender, ExecutedRoutedEventArgs e)
		{
			var findConfigDialog = new Microsoft.Win32.OpenFileDialog
			                       	{
			                       		FileName = "web",
			                       		DefaultExt = ".config",
			                       		Filter = "Configurations (.config)|*.config",
			                       		Multiselect = false,
			                       		CheckFileExists = true
			                       	};

			bool? result = findConfigDialog.ShowDialog();

			if (result == true)
			{
				InitializationViewModel.ConfigurationPath = findConfigDialog.FileName;
			}
		}

		private void OpenConfig_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = InitializationViewModel.CanLoad;
			e.Handled = true;
		}

		private void OpenConfig_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				var webConfigDirectory = new FileInfo(InitializationViewModel.ConfigurationPath).Directory;
				var binDirectory = webConfigDirectory.GetDirectories("bin").FirstOrDefault();

				if (binDirectory == null)
				{
					throw new DirectoryNotFoundException("Could not find web site bin folder. Bin folder should be in the same directory as the web.config");
				}

				ProviderManagers = new WebProviderInitializer(new ProviderFactory(binDirectory)).InitializeFromConfigurationFile(InitializationViewModel.ConfigurationPath, InitializationViewModel.CreateMembershipDatabases);

				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error loading config", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
