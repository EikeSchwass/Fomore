// Eike Stein: Fomore/UI/Tool.cs (2018/06/12)

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public abstract class Tool : ViewModelBase
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

        protected Tool()
        {
            if (!(this is PanTool))
                PanTool = new PanTool();
            PressedCommand =
                new DelegateCommand<CreatureEditorPanelVM>(o => SelectionRequested?.Invoke(this,
                                                                                           new ToolEventArgs(this,
                                                                                                             o.CreatureStructureEditorCanvasVM)),
                                                           o => CanBeSelected());
        }

        public event ToolEventHandler SelectionRequested;

        public void Select()
        {
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

        public virtual bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool?.OnCanvasMouseDown(mouseInfo, canvasVM) == true;
            return false;
        }

        public virtual bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool?.OnCanvasMouseMove(mouseInfo, canvasVM) == true;
            return false;
        }
        public virtual bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool?.OnCanvasMouseUp(mouseInfo, canvasVM) == true;
            return false;
        }

        public virtual void OnCanvasMouseEnter(CreatureStructureEditorCanvasVM canvasVM)
        {
            PanTool?.OnCanvasMouseEnter(canvasVM);
        }

        public virtual void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM)
        {
            PanTool?.OnCanvasMouseLeave(canvasVM);
        }

        public virtual bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            canvasVM.CameraVM.ZoomFactor += mouseWheelInfo.MouseWheelDelta / 1200;
            return true;
        }

        public virtual bool CanBeSelected() => true;
        public virtual void OnSelected() { }
        public virtual void OnDeselected() { }
        public void OnCanBeSelectedChanged(CreatureStructureEditorCanvasVM canvasVM) => PressedCommand.OnCanExecuteChanged();
    }
}