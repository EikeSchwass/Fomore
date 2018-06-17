using Core;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The View Model that encapsulates the Environment Class
    /// </summary>
    public class EnvironmentVM : ViewModelBase<Environment>
    {
        public string Name
        {
            get => Model.Name;
            set
            {
                if (value == Model.Name) return;
                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => Model.Description;
            set
            {
                if (value == Model.Description) return;
                Model.Description = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public EnvironmentVM(Environment model) : base(model) { }
    }
}