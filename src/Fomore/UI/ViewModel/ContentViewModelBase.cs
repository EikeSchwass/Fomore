using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public abstract class ContentViewModelBase : ViewModelBase
    {
        public ICommand NewCreatureCommand { get; protected set; } = new StubCommand();
        public ICommand NewBoneCollectionCommand { get; protected set; } = new StubCommand();
        public ICommand OpenCommand { get; protected set; } = new StubCommand();
        public ICommand QuitCommand { get; protected set; } = new StubCommand();
        public ICommand UndoCommand { get; protected set; } = new StubCommand();
        public ICommand RedoCommand { get; protected set; } = new StubCommand();
        public ICommand AboutCommand { get; protected set; } = new StubCommand();
    }
}