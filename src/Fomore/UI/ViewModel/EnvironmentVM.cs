using Core.Simulations;

namespace Fomore.UI.ViewModel
{
    public class EnvironmentVM : ViewModelBase
    {
        private Environment Environment { get; }

        public string Name
        {
            get => Environment.Name;
            set
            {
                if (value == Environment.Name) return;
                Environment.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description => $"Gravity: {Environment.Gravity}";

        public EnvironmentVM(Environment environment)
        {
            Environment = environment;
        }
    }
}