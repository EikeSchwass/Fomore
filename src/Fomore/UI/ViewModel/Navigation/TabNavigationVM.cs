using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Navigation
{
    public class TabNavigationVM : ViewModelBase
    {
        public DelegateCommand SwitchToCreatureTabCommand { get; }
        public DelegateCommand SwitchToEnvironmentTabCommand { get; }
        public DelegateCommand SwitchToTrainingTabCommand { get; }
        public DelegateCommand SwitchToSimulationTabCommand { get; }

        /// <inheritdoc />
        public TabNavigationVM(DelegateCommand switchToCreatureTabCommand, DelegateCommand switchToEnvironmentTabCommand, DelegateCommand switchToTrainingTabCommand, DelegateCommand switchToSimulationTabCommand)
        {
            SwitchToCreatureTabCommand = switchToCreatureTabCommand;
            SwitchToEnvironmentTabCommand = switchToEnvironmentTabCommand;
            SwitchToTrainingTabCommand = switchToTrainingTabCommand;
            SwitchToSimulationTabCommand = switchToSimulationTabCommand;
        }

        public void OnCanExecuteChanged()
        {
            SwitchToCreatureTabCommand.OnCanExecuteChanged();
            SwitchToEnvironmentTabCommand.OnCanExecuteChanged();
            SwitchToTrainingTabCommand.OnCanExecuteChanged();
            SwitchToSimulationTabCommand.OnCanExecuteChanged();
        }
    }
}