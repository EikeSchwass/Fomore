using Core.TerrainGenerator;

namespace Fomore.UI.ViewModel.Data
{
    public class PerlinGeneratorVM : ViewModelBase<PerlinGenerator>
    {
        public double Phase
        {
            get => Model.Phase;
            set
            {
                if (value.Equals(Model.Phase)) return;
                Model.Phase = value;
                OnPropertyChanged();
            }
        }

        public double Smoothness
        {
            get => Model.Smoothness;
            set
            {
                if (value.Equals(Model.Smoothness)) return;
                Model.Smoothness = value;
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


        /// <inheritdoc />
        public PerlinGeneratorVM(PerlinGenerator model) : base(model) { }
    }
}