﻿using System;
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

        public int Iterations => Model.Iterations;

        public DateTime LastAccess => Model.CreationDate;

        /// <inheritdoc />
        public MovementPatternVM(MovementPattern model) : base(model) { }

    }
}