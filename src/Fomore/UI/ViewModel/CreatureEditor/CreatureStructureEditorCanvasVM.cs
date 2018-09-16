using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor.Tools;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureStructureEditorCanvasVM : ViewModelBase
    {

        public static double CanvasWidth => 1000;
        public static double CanvasHeight => 1000;

        public PreviewJointVM PreviewJoint { get; } = new PreviewJointVM();
        public PreviewBoneVM PreviewBone { get; } = new PreviewBoneVM();
        public SelectionVM SelectionVM { get; } = new SelectionVM();
        public InfoMessageCollection InfoMessageCollection { get; } = new InfoMessageCollection();

        public CameraVM CameraVM { get; }
        public ToolCollectionVM ToolCollectionVM { get; }
        public CreatureVM Creature { get; }

        public ObservableCollection<JointVM> SelectedJoints { get; } = new ObservableCollection<JointVM>();
        public ObservableCollection<BoneVM> SelectedBones { get; } = new ObservableCollection<BoneVM>();
        
        public BackgroundImageVM BackgroundImage { get; } = new BackgroundImageVM();

        public DelegateHandleCommand<MouseInfo> CanvasMouseDownCommand { get; }
        public DelegateHandleCommand<MouseInfo> CanvasMouseUpCommand { get; }
        public DelegateHandleCommand<MouseInfo> CanvasMouseMoveCommand { get; }
        public DelegateHandleCommand<MouseWheelInfo> CanvasMouseWheelCommand { get; }
        public DelegateCommand CanvasMouseEnterCommand { get; }
        public DelegateCommand CanvasMouseLeaveCommand { get; }

        public DelegateHandleCommand<SizeChange> CanvasSizeChangedCommand { get; }

        public HistoryStackVM<CreatureStructureEditorCanvasVM> HistoryStack { get; }

        public CreatureStructureEditorCanvasVM(CreatureVM creature, ToolCollectionVM toolCollectionVM)
        {
            HistoryStack = new HistoryStackVM<CreatureStructureEditorCanvasVM>(this);
            HistoryStack.PropertyChanged += (o, e) => Reset();
            Creature = creature;
            CameraVM = new CameraVM { OffsetX = -CanvasWidth / 2, OffsetY = -CanvasHeight / 2 };
            ToolCollectionVM = toolCollectionVM;

            CanvasSizeChangedCommand = new DelegateHandleCommand<SizeChange>(CanvasSizeChanged, o => true);
            CanvasMouseDownCommand =
                new DelegateHandleCommand<MouseInfo>(mouseInfo =>
                                                         ToolCollectionVM.SelectedTool?.OnCanvasMouseDown(mouseInfo,
                                                                                                          this,
                                                                                                          Keyboard.Modifiers) ==
                                                         true,
                                                     o => true);
            CanvasMouseUpCommand =
                new DelegateHandleCommand<MouseInfo>(mouseInfo =>
                                                         ToolCollectionVM.SelectedTool?.OnCanvasMouseUp(mouseInfo,
                                                                                                        this,
                                                                                                        Keyboard.Modifiers) ==
                                                         true,
                                                     o => true);
            CanvasMouseMoveCommand =
                new DelegateHandleCommand<MouseInfo>(mouseInfo =>
                                                         ToolCollectionVM.SelectedTool?.OnCanvasMouseMove(mouseInfo,
                                                                                                          this,
                                                                                                          Keyboard.Modifiers) ==
                                                         true,
                                                     o => true);
            CanvasMouseWheelCommand = new DelegateHandleCommand<MouseWheelInfo>(mouseWheelInfo =>
                                                                                    ToolCollectionVM
                                                                                       .SelectedTool?.OnCanvasMouseWheel(mouseWheelInfo,
                                                                                                                         this,
                                                                                                                         Keyboard
                                                                                                                            .Modifiers) ==
                                                                                    true,
                                                                                o => true);
            CanvasMouseEnterCommand =
                new DelegateCommand(o => ToolCollectionVM.SelectedTool?.OnCanvasMouseEnter(this, Keyboard.Modifiers), o => true);
            CanvasMouseLeaveCommand =
                new DelegateCommand(o => ToolCollectionVM.SelectedTool?.OnCanvasMouseLeave(this, Keyboard.Modifiers), o => true);
        }

        public void Reset()
        {
            foreach (var selectedBone in SelectedBones.ToList())
            {
                if (!Creature.CreatureStructureVM.BoneCollectionVM.Contains(selectedBone))
                    SelectedBones.Remove(selectedBone);
            }
            foreach (var selectedJoint in SelectedJoints.ToList())
            {
                if (!Creature.CreatureStructureVM.JointCollectionVM.Contains(selectedJoint))
                    SelectedJoints.Remove(selectedJoint);
            }


            PreviewBone.HighlightedJoints.Clear();
            PreviewBone.Visibility = Visibility.Hidden;
            ToolCollectionVM.Tools.OfType<PlaceBoneTool>().First().Reset();
        }

        private bool CanvasSizeChanged(SizeChange sizeChange)
        {
            CameraVM.UpdateBoundaries();
            return true;
        }
    }
}