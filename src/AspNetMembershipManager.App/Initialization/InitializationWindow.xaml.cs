using System;
using System.Windows;

namespace AspNetMembershipManager.Initialization
{
	/// <summary>
	/// Interaction logic for InitializationWindow.xaml
	/// </summary>
	public partial class InitializationWindow : Window
	{
		private readonly InitializationModel viewModel;

		public InitializationWindow()
		{
			InitializeComponent();

			viewModel = new InitializationModel();
			DataContext = viewModel;
		}

		private void btnFindConfigFile_Click(object sender, RoutedEventArgs e)
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
				viewModel.ConfigurationPath = findConfigDialog.FileName;
			}
		}

        public ProviderManagers ProviderManagers { get; private set; }

		private void btnLoadConfigFile_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ProviderManagers = new WebProviderInitializer(new ProviderFactory()).InitializeFromConfigurationFile(viewModel.ConfigurationPath, viewModel.CreateMembershipDatabases);

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
