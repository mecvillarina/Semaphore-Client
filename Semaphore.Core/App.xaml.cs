using Prism.Ioc;
using Prism.Modularity;
using Semaphore.Common.Utilities;
using Semaphore.Core.Views;
using Semaphore.Manager;
using Semaphore.Manager.Mappers;
using Semaphore.UI;
using Semaphore.WebService;
using System.Windows;

namespace Semaphore.Core
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IServiceEntityMapper, ServiceEntityMapper>();
            containerRegistry.RegisterSingleton<IConnectivity, Connectivity>();

			containerRegistry.Register<IAccountWebService, AccountWebService>();
			containerRegistry.Register<IMessageWebService, MessageWebService>();

            containerRegistry.Register<IAccountManager, AccountManager>();
            containerRegistry.Register<IMessageManager, MessageManager>();

        }

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			moduleCatalog.AddModule(typeof(UIModule));
		}
	}
}
