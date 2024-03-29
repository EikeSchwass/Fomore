﻿using Core.TerrainGenerator;

namespace Fomore.UI.ViewModel.Data
{
    public class SineGeneratorVM : ViewModelBase<SineGenerator>
    {

        public double Offset
        {
            get => Model.Offset;
            set
            {
                if (value.Equals(Model.Offset)) return;
                Model.Offset = value;
                OnPropertyChanged();
            }
        }

        public double Amplitude
        {
            get => Model.Amplitude;
            set
            {
                if (value.Equals(Model.Amplitude)) return;
                Model.Amplitude = value;
                OnPropertyChanged();
            }
        }

        public double Frequency
        {
            get => Model.Frequency;
            set
            {
                if (value.Equals(Model.Frequency)) return;
                Model.Frequency = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public SineGeneratorVM(SineGenerator model) : base(model) { }
    }
}