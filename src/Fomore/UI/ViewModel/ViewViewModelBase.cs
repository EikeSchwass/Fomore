using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public abstract class ViewViewModelBase : ViewModelBase
    {
        protected ViewViewModelBase(AppState appState)
        {
            AppState = appState;
        }

        public AppState AppState { get; }
        public ICommand NewCreatureCommand { get; protected set; } = new StubCommand();
        public ICommand NewBoneCollectionCommand { get; protected set; } = new StubCommand();
        public ICommand OpenCommand { get; protected set; } = new StubCommand();
        public ICommand QuitCommand { get; protected set; } = new StubCommand();
        public ICommand UndoCommand { get; protected set; } = new StubCommand();
        public ICommand RedoCommand { get; protected set; } = new StubCommand();
        public ICommand AboutCommand { get; protected set; } = new StubCommand();
    }
}