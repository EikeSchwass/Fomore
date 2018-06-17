using System.Linq;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Helper;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The View Model that encapsulates the Environment Class
    /// </summary>
    public class EnvironmentVM : ViewModelBase<Environment>
    {
        private string description;
        private string name;

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

        /// <inheritdoc />
        public EnvironmentVM(Environment model) : base(model) { }
    }
}