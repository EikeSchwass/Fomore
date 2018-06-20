using System.Collections.ObjectModel;
using System.Windows;
using Core;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class PreviewBoneVM : ViewModelBase
    {
        private Vector2 from;
        private Vector2 to;
        private Visibility visibility;

        public ObservableCollection<JointVM> HighlightedJoints { get; } = new ObservableCollection<JointVM>();

        public PreviewBoneVM()
        {
            HighlightedJoints.CollectionChanged += (o, e) => OnPropertyChanged(nameof(HighlightedJoints));
        }

        public Vector2 From
        {
            get => from;
            set
            {
                if (value.Equals(from)) return;
                from = value;
                OnPropertyChanged();
            }
        }

        public Vector2 To
        {
            get => to;
            set
            {
                if (value.Equals(to)) return;
                to = value;
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