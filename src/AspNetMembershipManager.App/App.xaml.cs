using System;
using System.IO;
using System.Windows;
using AspNetMembershipManager.Initialization;
using AspNetMembershipManager.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AspNetMembershipManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        private readonly IWindsorContainer container = new WindsorContainer();

	    private void Application_Startup(object sender, StartupEventArgs e)
	    {
            ClearConfig();
            
			if (! LoadRemoteConfig())
            {
                Current.Shutdown();
            }
			else
			{
	            DisplayDashboard();
			}
        }

	    private void DisplayDashboard()
	    {
            var mainWindow = new MainWindow(container.Resolve<IProviderManagers>());
	        Current.MainWindow = mainWindow;
	        mainWindow.Show();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

	    private bool LoadRemoteConfig()
	    {
            var initializeDialog = new InitializationWindow();
            var initializationResult = initializeDialog.ShowDialog();

            if (initializationResult == true)
            {
                container.Register(Component.For<IProviderManagers>().Instance(initializeDialog.ProviderManagers));	
            }
	    	return initializationResult.HasValue && initializationResult.Value;
	    }

	    private void ClearConfig()
		{
			File.Delete(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		}

		private void Application_Exit(object sender, ExitEventArgs e)
	    {
	        container.Dispose();
	    }
	}
}
