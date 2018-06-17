using System;
using Environment = Core.Environment;

namespace Fomore.UI.ViewModel.Data
{
    public class EnvironmentVM : ViewModelBase<Environment>
    {
        private string name;
        private string description;
        private DateTime lastAccess;
        private double gravity;
        private double friction;

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
                OnAccess();
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
                OnAccess();
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