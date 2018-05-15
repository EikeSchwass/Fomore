using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.Views.Controls;

namespace Fomore.UI.ViewModel
{
    public abstract class ViewViewModelBase : ViewModelBase
    {
        protected ViewViewModelBase(AppState appState)
        {
            AppState = appState;
            NewCreatureCommand = AppState.CreatureCollection.AddNewCreatureCommand;
            ShowMessageBoxCommand = new DelegateCommand(o =>
            {
                if (!(o is MessageBoxParameters messageBoxContent)) return;
                AppState.MessageBoxParameters = messageBoxContent;
                messageBoxContent.IsOpened = true;
            }, o => true);
        }

        public AppState AppState { get; }
        public ICommand NewCreatureCommand { get; protected set; }
        public ICommand NewBoneCollectionCommand { get; protected set; } = new StubCommand();
        public ICommand OpenCommand { get; protected set; } = new StubCommand();
        public ICommand QuitCommand { get; protected set; } = new StubCommand();
        public ICommand UndoCommand { get; protected set; } = new StubCommand();
        public ICommand RedoCommand { get; protected set; } = new StubCommand();
        public ICommand ShowMessageBoxCommand { get; protected set; }
    }
}