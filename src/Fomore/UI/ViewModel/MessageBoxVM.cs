using System;
using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class MessageBoxVM : ViewModelBase
    {
        private bool isOpened;
        private MessageBoxParameters messageBoxParameters;

        public bool IsOpened
        {
            get => isOpened;
            private set
            {
                if (value == isOpened) return;
                isOpened = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseMessageBoxCommand { get; }
        public ICommand ShowMessageBoxCommand { get; }

        public MessageBoxParameters MessageBoxParameters
        {
            get => messageBoxParameters;
            private set
            {
                if (Equals(value, messageBoxParameters)) return;
                messageBoxParameters = value;
                OnPropertyChanged();
            }
        }

        public MessageBoxVM()
        {
            ShowMessageBoxCommand = new DelegateCommand(ShowMessageBox, o => true);
            CloseMessageBoxCommand = new DelegateCommand(CloseMessageBox, o => true);
        }

        private void CloseMessageBox(object obj)
        {
            IsOpened = false;
            MessageBoxParameters = new MessageBoxParameters();
        }

        private void ShowMessageBox(object parameter)
        {
            if (parameter is MessageBoxParameters parameters)
            {
                MessageBoxParameters = parameters;
                IsOpened = true;
            }
            else
            {
                throw new InvalidOperationException($"No {nameof(MessageBoxParameters)} supplied. MessageBox would be empty");
            }
        }
    }
}