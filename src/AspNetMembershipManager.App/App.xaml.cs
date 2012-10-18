using System;
using System.IO;
using System.Windows;
using AspNetMembershipManager.Initialization;
using AspNetMembershipManager.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net.Config;
using log4net.Layout;

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
            
            ConfigureLogging();

            LoadRemoteConfig();

            DisplayDashboard();
        }

	    private void DisplayDashboard()
	    {
            var mainWindow = new MainWindow(container.Resolve<IProviderManagers>());
	        Current.MainWindow = mainWindow;
	        mainWindow.Show();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

	    private void LoadRemoteConfig()
	    {
            var initializeDialog = new InitializationWindow();
            var initializationResult = initializeDialog.ShowDialog();

            if (initializationResult != true)
            {
                Current.Shutdown();
            }

            container.Register(Component.For<IProviderManagers>().Instance(initializeDialog.ProviderManagers));
	    }

	    private void ClearConfig()
		{
			File.Delete(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		}

	    private static void ConfigureLogging()
        {
            XmlConfigurator.Configure();

            var pl = new PatternLayout
            {
                ConversionPattern =
                    "%date{HH:mm:ss.fff} : %5level [%logger] - [%thread] - %newline%message%exception%newline%newline"
            };
            pl.ActivateOptions();

            var delegateAppender = new DelegateAppender { Name = "DelegateAppender", Layout = pl };
            delegateAppender.ActivateOptions();

            BasicConfigurator.Configure(delegateAppender);

            LoggerProvider.Provider = new Log4NetLoggerProvider();
        }

	    private void Application_Exit(object sender, ExitEventArgs e)
	    {
	        container.Dispose();
	    }
	}
}
