using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class WelcomeScreenVM : ViewViewModelBase
    {
        public WelcomeScreenVM(AppState appState) : base(appState)
        {
            NewCreatureCommand =
                new DelegateCommand(parameter => AppState.CurrentViewModel = new CreatureEditorVM(AppState),
                                    parameter => true);
        }
    }
}