using System;
using Core;

namespace Fomore.UI.ViewModel.Data
{
    public class MovementPatternVM : ViewModelBase<MovementPattern>
    {
        private string name;
        private DateTime lastAccess;
        private int iterations;

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

        public int Iterations
        {
            get => iterations;
            set
            {
                if (value == iterations) return;
                iterations = value;
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

        /// <inheritdoc />
        public MovementPatternVM(MovementPattern model) : base(model) { }

}