using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Application
{
    public class AppStateVM : ViewModelBase
    {
        public NavigationVM NavigationVM { get; }
        public EntityStorageVM EntitiesStorageVM { get; }
        public ICommand LoadCommand { get; }

        public AppStateVM()
        {
            LoadCommand = new DelegateCommand(Load, o => true);
            EntitiesStorageVM = new EntityStorageVM(new EntitiyStorage());
            NavigationVM=new NavigationVM(EntitiesStorageVM);
            //NavigationVM.SelectedTab = NavigationVM.TabCollection[3];
            ((SimulationTabVM) NavigationVM.TabCollection[3]).CreateStuffCommand.Execute(null);
        }

        private void Load(object obj)
        {
            EntitiesStorageVM.Load();
        }
    }
}
