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

        private DateTime lastAccess;
        private double gravity;
        private double friction;


        public double Gravity
        {
            get => gravity;
            set
            {
                if (value.Equals(gravity)) return;
                gravity = value;
                OnPropertyChanged();
                OnAccess();
            }
        }

        public double Friction
        {
            get => friction;
            set
            {
                if (value.Equals(friction)) return;
                friction = value;
                OnPropertyChanged();
                OnAccess();
            }
        }

        public DateTime LastAccess
        {
            get => lastAccess;
            private set
            {
                if (value.Equals(lastAccess)) return;
                lastAccess = value;
                OnPropertyChanged();
            }
        }

        private void OnAccess()
        {
            LastAccess = DateTime.Now;
        }

        public EnvironmentVM(Environment model) : base(model) { }
    }
}