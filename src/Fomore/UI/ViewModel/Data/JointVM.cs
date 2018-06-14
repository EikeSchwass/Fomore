using Core;

namespace Fomore.UI.ViewModel.Data
{
    public class JointVM : ViewModelBase<Joint>
    {
        public Vector2 Position
        {
            get => Model.Position;
            set
            {
                if (value.Equals(Model.Position)) return;
                Model.Position = value;
                OnPropertyChanged();
            }
        }

        public JointVM(Joint model) : base(model) { }
    }
}