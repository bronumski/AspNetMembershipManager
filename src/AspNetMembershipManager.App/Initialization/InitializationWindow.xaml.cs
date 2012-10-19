using System;
using System.IO;
using System.Linq;
using System.Windows;
using AspNetMembershipManager.Web;

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
				var webConfigDirectory = new FileInfo(viewModel.ConfigurationPath).Directory;
				var binDirectory = webConfigDirectory.GetDirectories("bin").FirstOrDefault();

				if (binDirectory == null)
				{
					throw new DirectoryNotFoundException("Could not find web site bin folder. Bin folder should be in the same directory as the web.config");
				}

				ProviderManagers = new WebProviderInitializer(new ProviderFactory(binDirectory)).InitializeFromConfigurationFile(viewModel.ConfigurationPath, viewModel.CreateMembershipDatabases);

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
