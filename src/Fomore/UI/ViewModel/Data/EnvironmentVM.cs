using Core;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Data
{
    public class EnvironmentVM : ViewModelBase<Environment>
    {
        private string name;
        private string description;
        private double gravity;

        /// <inheritdoc />
        public EnvironmentVM(Environment model) : base(model) { }

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (value == description) return;
                description = value;
                OnPropertyChanged();
            }
        }

        public double Gravity
        {
            get => gravity;
            set
            {
                if (value.Equals(gravity)) return;
                gravity = value;
                OnPropertyChanged();
            }
        }
    }
}