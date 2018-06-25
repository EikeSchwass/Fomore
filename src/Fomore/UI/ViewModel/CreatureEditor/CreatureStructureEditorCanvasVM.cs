using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor.Tools;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using Microsoft.Win32;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureStructureEditorCanvasVM : ViewModelBase
    {
        private ImageSource backgroundImageSource;

        public static double CanvasWidth => 1000;
        public static double CanvasHeight => 1000;

        public PreviewJointVM PreviewJoint { get; } = new PreviewJointVM();
        public PreviewBoneVM PreviewBone { get; } = new PreviewBoneVM();
        public SelectionVM SelectionVM { get; } = new SelectionVM();

        public CameraVM CameraVM { get; }
        public ToolCollectionVM ToolCollectionVM { get; }
        public HistoryStackVM<CreatureVM> HistoryStack { get; }

        public ObservableCollection<JointVM> SelectedJoints { get; } = new ObservableCollection<JointVM>();
        public ObservableCollection<BoneVM> SelectedBones { get; } = new ObservableCollection<BoneVM>();

        public ImageSource BackgroundImageSource
        {
            get => backgroundImageSource;
            set
            {
                if (Equals(value, backgroundImageSource)) return;
                backgroundImageSource = value;
                OnPropertyChanged();
                SetBackgroundImageCommand?.OnCanExecuteChanged();
                RemoveBackgroundImageCommand?.OnCanExecuteChanged();
            }
        }

        public DelegateCommand SetBackgroundImageCommand { get; }
        public DelegateCommand RemoveBackgroundImageCommand { get; }

        public DelegateHandleCommand<MouseInfo> CanvasMouseDownCommand { get; }
        public DelegateHandleCommand<MouseInfo> CanvasMouseUpCommand { get; }
        public DelegateHandleCommand<MouseInfo> CanvasMouseMoveCommand { get; }
        public DelegateHandleCommand<MouseWheelInfo> CanvasMouseWheelCommand { get; }
        public DelegateCommand CanvasMouseEnterCommand { get; }
        public DelegateCommand CanvasMouseLeaveCommand { get; }

        public DelegateHandleCommand<SizeChange> CanvasSizeChangedCommand { get; }

        public CreatureStructureEditorCanvasVM(HistoryStackVM<CreatureVM> historyStack, ToolCollectionVM toolCollectionVM)
        {
            HistoryStack = historyStack;
            HistoryStack.PropertyChanged += HistoryStackChanged;
            CameraVM = new CameraVM {OffsetX = -CanvasWidth / 2, OffsetY = -CanvasHeight / 2};
            ToolCollectionVM = toolCollectionVM;
            SetBackgroundImageCommand = new DelegateCommand(o => SetBackgroundImage(), o => true);
            RemoveBackgroundImageCommand = new DelegateCommand(o => RemoveBackgroundImage(), o => BackgroundImageSource != null);

            CanvasSizeChangedCommand = new DelegateHandleCommand<SizeChange>(CanvasSizeChanged, o => true);
            CanvasMouseDownCommand =
                new DelegateHandleCommand<MouseInfo>(mouseInfo => ToolCollectionVM.SelectedTool?.OnCanvasMouseDown(mouseInfo, this,Keyboard.Modifiers) == true,
                                                     o => true);
            CanvasMouseUpCommand =
                new DelegateHandleCommand<MouseInfo>(mouseInfo => ToolCollectionVM.SelectedTool?.OnCanvasMouseUp(mouseInfo, this,Keyboard.Modifiers) == true,
                                                     o => true);
            CanvasMouseMoveCommand =
                new DelegateHandleCommand<MouseInfo>(mouseInfo => ToolCollectionVM.SelectedTool?.OnCanvasMouseMove(mouseInfo, this, Keyboard.Modifiers) == true,
                                                     o => true);
            CanvasMouseWheelCommand =
                new DelegateHandleCommand<MouseWheelInfo>(mouseWheelInfo =>
                                                              ToolCollectionVM.SelectedTool?.OnCanvasMouseWheel(mouseWheelInfo, this, Keyboard.Modifiers) ==
                                                              true,
                                                          o => true);
            CanvasMouseEnterCommand = new DelegateCommand(o => ToolCollectionVM.SelectedTool?.OnCanvasMouseEnter(this, Keyboard.Modifiers), o => true);
            CanvasMouseLeaveCommand = new DelegateCommand(o => ToolCollectionVM.SelectedTool?.OnCanvasMouseLeave(this, Keyboard.Modifiers), o => true);
        }

        private void HistoryStackChanged(object sender, PropertyChangedEventArgs e)
        {
            SelectedJoints.Clear();
            SelectedBones.Clear();
            PreviewBone.HighlightedJoints.Clear();
            PreviewBone.Visibility = Visibility.Hidden;
            ToolCollectionVM.Tools.OfType<PlaceBoneTool>().First().Reset();
        }

        private bool CanvasSizeChanged(SizeChange sizeChange)
        {
            CameraVM.UpdateBoundaries();
            return true;
        }

        private void SetBackgroundImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select Background Image",
                CheckFileExists = true,
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() != true) return;
            string fileName = openFileDialog.FileName;
            try
            {
                var bitmapImage = new BitmapImage(new Uri(fileName, UriKind.Absolute));
                BackgroundImageSource = bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured when trying to open the selected image. If you think this is this programs fault give your Administrator the following information:\n{ex}",
                                "Error loading the image",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void RemoveBackgroundImage()
        {
            BackgroundImageSource = null;
        }
    }
}