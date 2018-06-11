namespace Fomore.UI.ViewModel.Navigation
{
    public class SimulationTabVM : TabPageVM
    {
        public TabNavigationVM TabNavigationVM { get; }

        /// <inheritdoc />
        public override string Header => "Simulation";

        public SimulationTabVM(TabNavigationVM tabNavigationVM)
        {
            TabNavigationVM = tabNavigationVM;
        }
    }
}