using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using Semaphore.Common.Constants;
using Semaphore.Common.Enums;
using Semaphore.Manager;
using Semaphore.Manager.Entities;
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
        private readonly IMessageManager _messageManager;
        public MainPageViewModel(IRegionManager regionManager, IAccountManager accountManager, IMessageManager messageManager) : base(regionManager)
        {
            _accountManager = accountManager;
            _messageManager = messageManager;

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

        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
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
                    this.ContactNumbers = txtContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
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
            else if (!this.ContactNumbers.Any())
            {
                MessageBox.Show("Please import contact numbers.", "Required");
            }
            else if (string.IsNullOrEmpty(this.Message))
            {
                MessageBox.Show("Message is required.", "Required");
            }
            else
            {
                bool canSend = false;
                if (!string.IsNullOrEmpty(this.SenderName))
                {
                    var getSenderNamesTask = _accountManager.GetSenderNames(this.ApiKey);
                    await Task.WhenAll(getSenderNamesTask);

                    var result = getSenderNamesTask.Result;

                    if (result.StatusCode == (int)GenericStatusValue.Success)
                    {
                        var senderName = result.SenderNames.FirstOrDefault(x => x.Status == "Active" && x.Name == this.SenderName);
                        if (senderName != null)
                        {
                            canSend = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not find the sender name in the specified API key.");
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
                    var numberGroups = new List<string>();
                    var numbers = new List<string>(this.ContactNumbers);
                    int maxCount = 40;
                    while (numbers.Count > 0)
                    {
                        string numberStrs = string.Join(",", numbers.Take(maxCount));
                        numbers = numbers.Skip(maxCount).ToList();
                        numberGroups.Add(numberStrs);
                    }

                    for (int i = 0; i < numberGroups.Count; i++)
                    {
                        this.StatusMessage = $"Sending ({i + 1}/{numberGroups.Count})";

                        string strNumbers = string.Join(",", numberGroups[i]);
                        var sendMessagesTask = _messageManager.SendBulkMessage(new SendMessageRequestEntity()
                        {
                            ApiKey = ApiKey,
                            Message = Message,
                            Numbers = strNumbers,
                            SenderName = SenderName
                        });

                        await Task.WhenAll(sendMessagesTask);
                        var sendMessagesResult = sendMessagesTask.Result;

                        if (sendMessagesResult.StatusCode == (int)GenericStatusValue.Success)
                        {
                            this.StatusMessage = $"Sent ({i + 1}/{numberGroups.Count})";
                        }
                    }

                    this.StatusMessage = "All Sent.";

                }
                else
                {
                    this.StatusMessage = "Sending Failed.";
                }
            }

            this.CanTapSendCommand = true;
        }

    }
}
