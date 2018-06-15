using System.Windows;
using Core;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class PreviewJointVM : ViewModelBase
    {
        private Vector2 position;
        private Visibility visibility = Visibility.Hidden;
        public double JointSize => 8;

        public Vector2 Position
        {
            get => position;
            set
            {
                if (value.Equals(position)) return;
                position = value;
                OnPropertyChanged();
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                if (value == visibility) return;
                visibility = value;
                OnPropertyChanged();
            }
        }
    }
}