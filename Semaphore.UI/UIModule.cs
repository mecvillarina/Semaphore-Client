using DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Semaphore.Common.Constants;
using Semaphore.UI.ViewModels;
using Semaphore.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.UI
{
	public class UIModule : IModule
	{
		private readonly IRegionManager _regionManager;
		public UIModule( IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
			_regionManager.RequestNavigate("ContentRegion", ViewNames.MainPage);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
		}
	}
}
