using System.Collections.ObjectModel;
using System.ComponentModel;
using Core.TerrainGenerator;

namespace Fomore.UI.ViewModel.Data
{
    public class TerrainGeneratorVM : ViewModelBase
    {
        private ViewModelBase terrainGenerator;
        private TerrainGenerators terrainGeneratorType = Data.TerrainGenerators.Linear;

        public ObservableCollection<TerrainGenerators> TerrainGenerators { get; } = new ObservableCollection<TerrainGenerators>
        {
            Data.TerrainGenerators.Linear,
            Data.TerrainGenerators.Sine,
            Data.TerrainGenerators.Perlin,
            Data.TerrainGenerators.Power
        };

        public TerrainGenerators TerrainGeneratorType
        {
            get => terrainGeneratorType;
            set
            {
                if (value == terrainGeneratorType) return;
                terrainGeneratorType = value;
                switch (value)
                {
                    case Data.TerrainGenerators.Linear:
                        TerrainGenerator = new LinearGeneratorVM(new LinearGenerator { StepSize = StepSize });
                        break;
                    case Data.TerrainGenerators.Perlin:
                        TerrainGenerator = new PerlinGeneratorVM(new PerlinGenerator { StepSize = StepSize });
                        break;
                    case Data.TerrainGenerators.Sine:
                        TerrainGenerator = new SineGeneratorVM(new SineGenerator { StepSize = StepSize });
                        break;
                    case Data.TerrainGenerators.Power:
                        TerrainGenerator = new PowerGeneratorVM(new PowerGenerator() { StepSize = StepSize });
                        break;
                }
                OnPropertyChanged();
            }
        }

        public TerrainGenerator Model
        {
            get
            {
                switch (TerrainGenerator)
                {
                    case LinearGeneratorVM linearGenerator:
                        return linearGenerator.Model;
                    case SineGeneratorVM sineGenerator:
                        return sineGenerator.Model;
                    case PerlinGeneratorVM perlinGenerator:
                        return perlinGenerator.Model;
                    case PowerGeneratorVM powerGenerator:
                        return powerGenerator.Model;
                }

                return null;
            }
            set
            {
                switch (value)
                {
                    case SineGenerator sineGenerator:
                        TerrainGenerator = new SineGeneratorVM(sineGenerator);
                        terrainGeneratorType = Data.TerrainGenerators.Sine;
                        break;
                    case LinearGenerator linearGenerator:
                        TerrainGenerator = new LinearGeneratorVM(linearGenerator);
                        terrainGeneratorType = Data.TerrainGenerators.Linear;
                        break;
                    case PerlinGenerator perlinGenerator:
                        TerrainGenerator = new PerlinGeneratorVM(perlinGenerator);
                        terrainGeneratorType = Data.TerrainGenerators.Perlin;
                        break;
                    case PowerGenerator powerGenerator:
                        TerrainGenerator = new PowerGeneratorVM(powerGenerator);
                        terrainGeneratorType = Data.TerrainGenerators.Power;
                        break;
                }
            }
        }

        public double StepSize
        {
            get => Model.StepSize;
            set
            {
                Model.StepSize = value;
                OnPropertyChanged();
            }
        }

        public ViewModelBase TerrainGenerator
        {
            get => terrainGenerator;
            private set
            {
                if (Equals(value, terrainGenerator)) return;
                if (terrainGenerator != null)
                    terrainGenerator.PropertyChanged -= TerrainGeneratorPropertyChanged;
                terrainGenerator = value;
                if (terrainGenerator != null)
                    terrainGenerator.PropertyChanged += TerrainGeneratorPropertyChanged;
                OnPropertyChanged();
            }
        }

        public TerrainGeneratorVM()
        {
            TerrainGenerator = new LinearGeneratorVM(new LinearGenerator());
        }

        private void TerrainGeneratorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("");
        }
    }
}