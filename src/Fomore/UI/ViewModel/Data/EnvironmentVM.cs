using System;
using Environment = Core.Environment;

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
                OnPropertyChanged(nameof(LastAccess));
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
                OnPropertyChanged(nameof(LastAccess));
            }
        }


        public double Gravity
        {
            get => Model.Gravity;
            set
            {
                Model.Gravity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastAccess));
            }
        }

        public double Friction
        {
            get => Model.Friction;
            set
            {
                Model.Friction = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastAccess));
            }
        }

        public DateTime LastAccess => Model.LastAccess;

        public EnvironmentVM(Environment model) : base(model) { }
    }
}