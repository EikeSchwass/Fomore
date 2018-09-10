using Core.TerrainGenerator;

namespace Fomore.UI.ViewModel.Data {
    public class LinearGeneratorVM : ViewModelBase<LinearGenerator>
    {
        private double inclination;

        /// <inheritdoc />
        public LinearGeneratorVM(LinearGenerator model) : base(model) { }

        public double Inclination
        {
            get => inclination;
            set
            {
                if (value.Equals(inclination)) return;
                inclination = value;
                Model.Inclination = value;
                OnPropertyChanged();
            }
        }
    }
}