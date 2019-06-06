using Prism.Regions;
using Semaphore.UI.ViewModels;

namespace Semaphore.Core.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(IRegionManager regionManager) : base(regionManager)
		{
		}

		private string _title = "Semaphore.co SMS Client App";
		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value);
		}
	}
}
