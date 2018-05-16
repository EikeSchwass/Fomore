using System.Windows.Media;
using Core.Creatures;
using Core.Simulations;

namespace Fomore.UI.ViewModel
{
    public class EnvironmentVM : ViewModelBase, ICloneable<EnvironmentVM>
    {
        private Environment Environment { get; }

        public string Name
        {
            get => Environment.Name;
            set
            {
                if (value == Environment.Name) return;
                Environment.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description => $"Gravity: {Environment.Gravity}";

        public ImageSource PreviewImage => null;

        public EnvironmentVM(Environment environment)
        {
            Environment = environment;
        }

        public EnvironmentVM Clone() => new EnvironmentVM(Environment.Clone());
    }
}