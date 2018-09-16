using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Fomore.UI.ViewModel.Commands;
using Microsoft.Win32;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class BackgroundImageVM : ViewModelBase
    {
        private FileInfo fileInfo;
        private double imageSize = 500;
        private ImageSource imageSource;
        private double offsetX;
        private double offsetY;
        private double rotationAngle;

        public FileInfo FileInfo
        {
            get => fileInfo;
            private set
            {
                if (value == fileInfo) return;
                fileInfo = value;
                OnPropertyChanged();
            }
        }

        private string FilePath { get; set; }

        public ImageSource ImageSource
        {
            get => imageSource;
            private set
            {
                if (Equals(value, imageSource)) return;
                imageSource = value;
                OnPropertyChanged();
                UpdateCommandsCanExecute();
            }
        }

        public DelegateCommand BrowseImageCommand { get; }
        public DelegateCommand ResetImageCommand { get; }
        public DelegateCommand OffsetRightCommand { get; }
        public DelegateCommand OffsetLeftCommand { get; }
        public DelegateCommand OffsetUpCommand { get; }
        public DelegateCommand OffsetDownCommand { get; }
        public DelegateCommand ScaleUpCommand { get; }
        public DelegateCommand ScaleDownCommand { get; }
        public DelegateCommand RotateClockwiseCommand { get; }
        public DelegateCommand RotateCounterClockwiseCommand { get; }

        public double OffsetX
        {
            get => offsetX;
            private set
            {
                if (value.Equals(offsetX)) return;
                offsetX = value;
                OnPropertyChanged();
                UpdateCommandsCanExecute();
            }
        }

        public double OffsetY
        {
            get => offsetY;
            private set
            {
                if (value.Equals(offsetY)) return;
                offsetY = value;
                OnPropertyChanged();
                UpdateCommandsCanExecute();
            }
        }

        public double ImageSize
        {
            get => imageSize;
            set
            {
                if (value > MaxImageSize)
                    value = MaxImageSize;
                else if (value < MinImageSize)
                    value = MinImageSize;
                if (value.Equals(imageSize)) return;
                imageSize = value;
                UpdateCommandsCanExecute();
                OnPropertyChanged();
                CreateImageSource();
            }
        }

        public double RotationAngle
        {
            get => rotationAngle;
            private set
            {
                if (value.Equals(rotationAngle)) return;
                rotationAngle = value;
                OnPropertyChanged();
            }
        }

        public double MinImageSize { get; } = 50;

        public double MaxImageSize { get; } = 1000;

        public BackgroundImageVM()
        {
            BrowseImageCommand = new DelegateCommand(o => BrowseImage(), o => true);
            ResetImageCommand = new DelegateCommand(o => ResetImage(), o => ImageSource != null);
            OffsetRightCommand = new DelegateCommand(o => OffsetX += 5, o => OffsetX + ImageSize / 2 < MaxImageSize);
            OffsetLeftCommand = new DelegateCommand(o => OffsetX -= 5, o => OffsetX + ImageSize / 2 > 0);
            OffsetDownCommand = new DelegateCommand(o => OffsetY += 5, o => OffsetY + ImageSize / 2 < MaxImageSize);
            OffsetUpCommand = new DelegateCommand(o => OffsetY -= 5, o => OffsetY + ImageSize / 2 > 0);
            ScaleUpCommand = new DelegateCommand(o => ImageSize += 10, o => ImageSize < MaxImageSize);
            ScaleDownCommand = new DelegateCommand(o => ImageSize -= 10, o => ImageSize > MinImageSize);
            RotateClockwiseCommand = new DelegateCommand(o => RotationAngle += 2.5, o => true);
            RotateCounterClockwiseCommand = new DelegateCommand(o => RotationAngle -= 2.5, o => true);
        }

        private void UpdateCommandsCanExecute()
        {
            BrowseImageCommand.OnCanExecuteChanged();
            ResetImageCommand.OnCanExecuteChanged();
            OffsetRightCommand.OnCanExecuteChanged();
            OffsetLeftCommand.OnCanExecuteChanged();
            ScaleDownCommand.OnCanExecuteChanged();
            ScaleUpCommand.OnCanExecuteChanged();
        }

        private void ResetImage()
        {
            FileInfo = null;
            FilePath = null;
            ImageSource = null;
            ResetImageCommand?.OnCanExecuteChanged();
        }

        private void BrowseImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select Background Image",
                Filter = "Images (*.png, *.jpg, *.bmp)|*.png;*.bmp;*.jpg;*.jpeg"
            };
            var showDialog = openFileDialog.ShowDialog();
            if (showDialog == true)
            {
                FilePath = openFileDialog.FileName;
                FileInfo = new FileInfo(FilePath);
                CreateImageSource();
                ResetImageCommand?.OnCanExecuteChanged();
            }
        }

        private void CreateImageSource()
        {
            if (FileInfo == null)
                return;
            string path = FilePath;
            double size = ImageSize;

            bool setHeight = false;

            var uri = new Uri(path, UriKind.Absolute);

            {
                var dummyImage = new BitmapImage(uri);
                if (dummyImage.Width < dummyImage.Height)
                    setHeight = true;
            }

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            if (setHeight)
                bitmapImage.DecodePixelHeight = (int)size;
            else
                bitmapImage.DecodePixelWidth = (int)size;
            bitmapImage.EndInit();
            ImageSource = bitmapImage;
        }
    }
}