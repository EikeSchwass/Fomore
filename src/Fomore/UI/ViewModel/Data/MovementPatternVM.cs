using Core;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Data
{
    public class MovementPatternVM : ViewModelBase<MovementPattern>
    {
        private string name;
        private string description;
        private int iterations;

        /// <inheritdoc />
        public MovementPatternVM(MovementPattern model) : base(model) { }

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

        public int Iterations
        {
            get => iterations;
            set
            {
                if (value == iterations) return;
                iterations = value;
                OnPropertyChanged();
            }
        }
    }
}