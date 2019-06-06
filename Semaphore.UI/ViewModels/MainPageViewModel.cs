using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using Semaphore.Common.Constants;
using Semaphore.Common.Enums;
using Semaphore.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Semaphore.UI.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		private readonly IAccountManager _accountManager;
		public MainPageViewModel(IRegionManager regionManager, IAccountManager accountManager) : base(regionManager)
		{
			_accountManager = accountManager;

			this.BrowseContactsCommand = new DelegateCommand(() => ExecuteBrowseContactsCommand());
			this.SendCommand = new DelegateCommand(async () => await ExecuteSendCommand(), () => this.CanTapSendCommand)
				.ObservesProperty(() => this.CanTapSendCommand);
		}

		private List<string> _contactNumbers = new List<string>();
		public List<string> ContactNumbers
		{
			get => _contactNumbers;
			set => SetProperty(ref _contactNumbers, value);
		}

		private string _apiKey = string.Empty;
		public string ApiKey
		{
			get => _apiKey;
			set => SetProperty(ref _apiKey, value);
		}

		private string _senderName = string.Empty;
		public string SenderName
		{
			get => _senderName;
			set => SetProperty(ref _senderName, value);
		}

		private bool _canTapSendCommand = true;
		public bool CanTapSendCommand
		{
			get => _canTapSendCommand;
			set => SetProperty(ref _canTapSendCommand, value);
		}


		public DelegateCommand BrowseContactsCommand { get; private set; }
		public DelegateCommand SendCommand { get; private set; }

		private void ExecuteBrowseContactsCommand()
		{
			bool canUpload = !this.ContactNumbers.Any();

			if (this.ContactNumbers.Any())
			{
				var resultDialog = MessageBox.Show("Are you want to replace the current contact numbers?", "Notice", MessageBoxButton.YesNo);

				if (resultDialog == MessageBoxResult.Yes)
				{
					canUpload = true;
				}
			}

			if (canUpload)
			{
				var openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "txt files (*.txt)|*.txt";
				if (openFileDialog.ShowDialog() == true)
				{
					string txtContent = File.ReadAllText(openFileDialog.FileName);

					this.ContactNumbers = txtContent.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
				}
			}
		}

		private async Task ExecuteSendCommand()
		{
			this.CanTapSendCommand = false;

			if (string.IsNullOrEmpty(this.ApiKey))
			{
				MessageBox.Show("Api Key is required.", "Required");
			}
			else if (!ContactNumbers.Any())
			{
				MessageBox.Show("Please import contact numbers.", "Required");
			}
			else
			{
				bool canSend = false;
				if (!string.IsNullOrEmpty(this.SenderName))
				{
					var getSenderNamesTask = _accountManager.GetSenderNames(this.ApiKey);
					await Task.WhenAll(getSenderNamesTask);

					var result =  getSenderNamesTask.Result;

					if (result.StatusCode == (int)GenericStatusValue.Success)
					{
						var senderName = result.SenderNames.FirstOrDefault(x => x.Status == "Approved" && x.Name == this.SenderName);
						if(senderName != null)
						{
							canSend = true;
						}
					}
					else if (result.StatusCode == (int)GenericStatusValue.NoInternetConnection)
					{
						MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
					}
					else if (result.StatusCode == (int)GenericStatusValue.HasErrorMessage)
					{
						MessageBox.Show(result.Message, "Unsuccessful");
					}
					else
					{
						MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
					}
				}
				else
				{
					canSend = true;
				}

				if (canSend)
				{

				}
			}

			this.CanTapSendCommand = true;
		}

	}
}
