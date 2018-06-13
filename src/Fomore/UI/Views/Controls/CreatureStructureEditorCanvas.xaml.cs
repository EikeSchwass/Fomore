using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for CreatureStructureEditorCanvas.xaml
    /// </summary>
    public partial class CreatureStructureEditorCanvas
    {
        public static readonly DependencyProperty SizeChangedCommandProperty =
            DependencyProperty.Register("SizeChangedCommand",
                                        typeof(DelegateHandleCommand<SizeChange>),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateHandleCommand<SizeChange>)));

        public static readonly DependencyProperty MouseDownCommandProperty =
            DependencyProperty.Register("MouseDownCommand",
                                        typeof(DelegateHandleCommand<MouseInfo>),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateHandleCommand<MouseInfo>)));

        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.Register("MouseUpCommand",
                                        typeof(DelegateHandleCommand<MouseInfo>),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateHandleCommand<MouseInfo>)));

        public static readonly DependencyProperty MouseMoveCommandProperty =
            DependencyProperty.Register("MouseMoveCommand",
                                        typeof(DelegateHandleCommand<MouseInfo>),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateHandleCommand<MouseInfo>)));

        public static readonly DependencyProperty MouseWheelCommandProperty =
            DependencyProperty.Register("MouseWheelCommand",
                                        typeof(DelegateHandleCommand<MouseWheelInfo>),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateHandleCommand<MouseWheelInfo>)));

        public static readonly DependencyProperty MouseEnterCommandProperty =
            DependencyProperty.Register("MouseEnterCommand",
                                        typeof(DelegateCommand),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateCommand)));

        public static readonly DependencyProperty MouseLeaveCommandProperty =
            DependencyProperty.Register("MouseLeaveCommand",
                                        typeof(DelegateCommand),
                                        typeof(CreatureStructureEditorCanvas),
                                        new PropertyMetadata(default(DelegateCommand)));

        public DelegateHandleCommand<SizeChange> SizeChangedCommand
        {
            get => (DelegateHandleCommand<SizeChange>)GetValue(SizeChangedCommandProperty);
            set => SetValue(SizeChangedCommandProperty, value);
        }

        public DelegateHandleCommand<MouseInfo> MouseDownCommand
        {
            get => (DelegateHandleCommand<MouseInfo>)GetValue(MouseDownCommandProperty);
            set => SetValue(MouseDownCommandProperty, value);
        }

        public DelegateHandleCommand<MouseInfo> MouseUpCommand
        {
            get => (DelegateHandleCommand<MouseInfo>)GetValue(MouseUpCommandProperty);
            set => SetValue(MouseUpCommandProperty, value);
        }

        public DelegateHandleCommand<MouseInfo> MouseMoveCommand
        {
            get => (DelegateHandleCommand<MouseInfo>)GetValue(MouseMoveCommandProperty);
            set => SetValue(MouseMoveCommandProperty, value);
        }

        public DelegateHandleCommand<MouseWheelInfo> MouseWheelCommand
        {
            get => (DelegateHandleCommand<MouseWheelInfo>)GetValue(MouseWheelCommandProperty);
            set => SetValue(MouseWheelCommandProperty, value);
        }

        public DelegateCommand MouseEnterCommand
        {
            get => (DelegateCommand)GetValue(MouseEnterCommandProperty);
            set => SetValue(MouseEnterCommandProperty, value);
        }

        public DelegateCommand MouseLeaveCommand
        {
            get => (DelegateCommand)GetValue(MouseLeaveCommandProperty);
            set => SetValue(MouseLeaveCommandProperty, value);
        }

        public CreatureStructureEditorCanvas()
        {
            InitializeComponent();
        }

        private void CreatureStructureEditorCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var inputElement = (IInputElement)sender;
            var absolutePosition = PointToScreen(e.GetPosition(this));
            var relativePosition = e.GetPosition(inputElement);
            int timestamp = e.Timestamp;
            bool leftMouseDown = e.LeftButton == MouseButtonState.Pressed;
            bool middleMouseDown = e.MiddleButton == MouseButtonState.Pressed;
            bool rightMouseDown = e.RightButton == MouseButtonState.Pressed;
            var mouseInfo = new MouseInfo(absolutePosition, relativePosition, leftMouseDown, middleMouseDown, rightMouseDown, timestamp);

            if (MouseDownCommand?.CanExecute(mouseInfo) == true)
                e.Handled = MouseDownCommand?.Execute(mouseInfo) == true;
        }

        private void CreatureStructureEditorCanvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var inputElement = (IInputElement)sender;
            var absolutePosition = PointToScreen(e.GetPosition(this));
            var relativePosition = e.GetPosition(inputElement);

            int timestamp = e.Timestamp;
            bool leftMouseDown = e.ChangedButton == MouseButton.Left;
            bool middleMouseDown = e.ChangedButton == MouseButton.Middle;
            bool rightMouseDown = e.ChangedButton == MouseButton.Right;
            var mouseInfo = new MouseInfo(absolutePosition, relativePosition, leftMouseDown, middleMouseDown, rightMouseDown, timestamp);
            if (MouseUpCommand?.CanExecute(mouseInfo) == true)
                e.Handled = MouseUpCommand?.Execute(mouseInfo) == true;
        }

        private void CreatureStructureEditorCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            var inputElement = (IInputElement)sender;

            var absolutePosition = PointToScreen(e.GetPosition(this));
            var relativePosition = e.GetPosition(inputElement);

            int timestamp = e.Timestamp;
            bool leftMouseDown = e.LeftButton == MouseButtonState.Pressed;
            bool middleMouseDown = e.MiddleButton == MouseButtonState.Pressed;
            bool rightMouseDown = e.RightButton == MouseButtonState.Pressed;
            var mouseInfo = new MouseInfo(absolutePosition, relativePosition, leftMouseDown, middleMouseDown, rightMouseDown, timestamp);
            if (MouseMoveCommand?.CanExecute(mouseInfo) == true)
                e.Handled = MouseMoveCommand?.Execute(mouseInfo) == true;
        }

        private void CreatureStructureEditorCanvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var inputElement = (IInputElement)sender;

            var absolutePosition = PointToScreen(e.GetPosition(this));
            var relativePosition = e.GetPosition(inputElement);

            int delta = e.Delta;
            int timestamp = e.Timestamp;
            bool leftMouseDown = e.LeftButton == MouseButtonState.Pressed;
            bool middleMouseDown = e.MiddleButton == MouseButtonState.Pressed;
            bool rightMouseDown = e.RightButton == MouseButtonState.Pressed;
            var mouseWheelInfo = new MouseWheelInfo(absolutePosition,
                                                    relativePosition,
                                                    delta,
                                                    leftMouseDown,
                                                    middleMouseDown,
                                                    rightMouseDown,
                                                    timestamp);
            if (MouseWheelCommand?.CanExecute(mouseWheelInfo) == true)
                e.Handled = MouseWheelCommand?.Execute(mouseWheelInfo) == true;
        }

        private void CreatureStructureEditorCanvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is CreatureStructureEditorCanvasVM creatureStructureEditorCanvasVM)
            {
                creatureStructureEditorCanvasVM.CameraVM.MaxOffsetX = e.NewSize.Width;
                creatureStructureEditorCanvasVM.CameraVM.MaxOffsetY = e.NewSize.Height;
            }

            var oldSize = e.PreviousSize;
            var newSize = e.NewSize;
            var sizeChange = new SizeChange(oldSize, newSize);
            if (SizeChangedCommand?.CanExecute(sizeChange) == true)
                e.Handled = SizeChangedCommand?.Execute(sizeChange) == true;
        }

        private void CreatureStructureEditorCanvas_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (MouseEnterCommand?.CanExecute(null) == true)
                MouseEnterCommand?.Execute(null);
        }

        private void CreatureStructureEditorCanvas_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (MouseLeaveCommand?.CanExecute(null) == true)
                MouseLeaveCommand?.Execute(null);
        }
    }
}