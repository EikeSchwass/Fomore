// Eike Stein: Fomore/UI/Tool.cs (2018/06/12)

using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public abstract class Tool : ViewModelBase, IHasInputBinding
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Tool), new PropertyMetadata(default(bool)));

        private Cursor currentCursor;
        public abstract ImageSource Image { get; }

        public abstract ToolType ToolType { get; }

        public Cursor CurrentCursor
        {
            get => currentCursor;
            protected set
            {
                if (Equals(value, currentCursor)) return;
                currentCursor = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            private set => SetValue(IsSelectedProperty, value);
        }

        private PanTool PanTool { get; }

        public DelegateCommand<CreatureEditorPanelVM> PressedCommand { get; }

        public string ToolTip =>
            $"{GetType().Name} ({(InputGesture as KeyGesture)?.GetDisplayStringForCulture(CultureInfo.CurrentCulture)})";

        public abstract InputGesture InputGesture { get; }
        protected InfoMessageCollection InfoMessageCollection { get; private set; }

        protected Tool()
        {
            if (!(this is PanTool))
                PanTool = new PanTool();
            PressedCommand =
                new DelegateCommand<CreatureEditorPanelVM>(o => SelectionRequested?.Invoke(this, new ToolEventArgs(this, null)),
                                                           o => CanBeSelected());
        }

        public event ToolEventHandler SelectionRequested;

        public void Select(InfoMessageCollection infoMessageCollection)
        {
            InfoMessageCollection = infoMessageCollection;
            if (IsSelected) return;
            IsSelected = true;
            OnSelected();
        }

        public void Deselect()
        {
            if (!IsSelected) return;
            IsSelected = false;
            OnDeselected();
        }

        public virtual bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool?.OnCanvasMouseDown(mouseInfo, canvasVM, modifierKeys) == true;
            return false;
        }

        public virtual bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool?.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys) == true;
            return false;
        }

        public virtual bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool?.OnCanvasMouseUp(mouseInfo, canvasVM, modifierKeys) == true;
            return false;
        }

        public virtual void OnCanvasMouseEnter(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            PanTool?.OnCanvasMouseEnter(canvasVM, modifierKeys);
        }

        public virtual void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            PanTool?.OnCanvasMouseLeave(canvasVM, modifierKeys);
        }

        public virtual bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo,
                                               CreatureStructureEditorCanvasVM canvasVM,
                                               ModifierKeys modifierKeys)
        {
            canvasVM.CameraVM.ZoomFactor += mouseWheelInfo.MouseWheelDelta / 1200;
            return true;
        }

        public virtual bool CanBeSelected() => true;
        public virtual void OnSelected() { }
        public virtual void OnDeselected() { }
        public void OnCanBeSelectedChanged(CreatureStructureEditorCanvasVM canvasVM) => PressedCommand.OnCanExecuteChanged();

        /// <inheritdoc />
        public InputBinding GetInputBinding() => new InputBinding(PressedCommand, InputGesture ?? new KeyGesture(Key.None));
    }
}