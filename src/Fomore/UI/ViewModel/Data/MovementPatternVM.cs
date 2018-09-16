using System;
using Core;

namespace Fomore.UI.ViewModel.Data
{
    public class MovementPatternVM : ViewModelBase<MovementPattern>
    {
        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastAccess));
            }
        }

        public int Iterations
        {
            get => Model.Iterations;
            set
            {
                Model.Iterations = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastAccess));
            }
        }

        public DateTime LastAccess => Model.CreationDate;

        /// <inheritdoc />
        public MovementPatternVM(MovementPattern model) : base(model) { }

    }
}