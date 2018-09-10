using Core.TerrainGenerator;

namespace Fomore.UI.ViewModel.Data
{
    public class PowerGeneratorVM : ViewModelBase<PowerGenerator>
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

        public double Gradualness
        {
            get => Model.Gradualness;
            set
            {
                if (value.Equals(Model.Gradualness)) return;
                Model.Gradualness = value;
                OnPropertyChanged();
            }
        }

        public double Power
        {
            get => Model.Power;
            set
            {
                if (value.Equals(Model.Power)) return;
                Model.Power = value;
                OnPropertyChanged();
            }
        }

        public PowerGeneratorVM(PowerGenerator model) : base(model) { }
    }
}