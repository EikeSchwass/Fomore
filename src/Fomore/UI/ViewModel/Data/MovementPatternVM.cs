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
            }
        }

        public int Iterations => Model.Iterations;

        public DateTime CreationDate => Model.CreationDate;

        /// <inheritdoc />
        public MovementPatternVM(MovementPattern model) : base(model) { }

    }
}