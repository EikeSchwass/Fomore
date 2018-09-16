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
        private ImageSource imageSource;
        private double imageSize = 500;

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
            }
        }

        public DelegateCommand BrowseImageCommand { get; }
        public DelegateCommand ResetImageCommand { get; }

        public double ImageSize
        {
            get => imageSize;
            set
            {
                if (value.Equals(imageSize)) return;
                imageSize = value;
                OnPropertyChanged();
                CreateImageSource();
            }
        }

        public double MinImageSize { get; } = 50;

        public double MaxImageSize { get; } = 1000;

        public BackgroundImageVM()
        {
            BrowseImageCommand = new DelegateCommand(o => BrowseImage(), o => true);
            ResetImageCommand = new DelegateCommand(o => ResetImage(), o => ImageSource != null);
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
            if(FileInfo==null)
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