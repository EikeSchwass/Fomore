using System;
using System.Windows;
using Core;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class SelectionVM : ViewModelBase
    {
        private double height;
        private Vector2 startPosition = new Vector2(200, 200);
        private Visibility visibility = Visibility.Hidden;
        private double width;

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

        public Vector2 StartPosition
        {
            get => startPosition;
            set
            {
                if (value.Equals(startPosition)) return;
                startPosition = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Rectangle));
            }
        }

        public Rect Rectangle
        {
            get
            {
                double minX = Math.Min(StartPosition.X, StartPosition.X + Width);
                double minY = Math.Min(StartPosition.Y, StartPosition.Y + Height);
                return new Rect(minX, minY, Math.Abs(Width), Math.Abs(Height));
            }
        }

        public double Width
        {
            get => width;
            set
            {
                if (value.Equals(width)) return;
                width = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Rectangle));
            }
        }

        public double Height
        {
            get => height;
            set
            {
                if (value.Equals(height)) return;
                height = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Rectangle));
            }
        }
    }
}