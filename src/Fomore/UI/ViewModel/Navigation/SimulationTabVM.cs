namespace Fomore.UI.ViewModel.Navigation
{
    public class SimulationTabVM : TabPageVM
    {
        private TabNavigationVM tabNavigationVM;

        public TabNavigationVM TabNavigationVM
        {
            get => tabNavigationVM;
            set
            {
                if (Equals(value, tabNavigationVM)) return;
                tabNavigationVM = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public override string Header => "Simulation";

        public SimulationTabVM()
        {
        }
    }
}