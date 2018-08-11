// Eike Stein: Fomore/UI/CreatureEditorVM.cs (2018/06/12)

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Fomore.UI.ViewModel.CreatureEditor.Behaviours;
using Fomore.UI.ViewModel.CreatureEditor.Changes;
using Fomore.UI.ViewModel.CreatureEditor.Tools;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorVM : ViewModelBase
    {
        public ChangeStackVM ChangeStack { get; }

        public CreatureVM Creature { get; }

        public SelectionVM Selection { get; }

        public ChangeManagementVM ChangeManagement { get; }
        

        public ToolCollectionVM ToolCollectionVM { get; }

        public ObservableCollection<BaseBehaviour> Behaviours { get; }

        public IEnumerable<IHasInputBinding> InputBindings => ToolCollectionVM.Tools.OfType<IHasInputBinding>().Concat(Behaviours);


        public CreatureStructureEditorCanvasVM CreatureStructureEditorCanvasVM { get; }


        public CreatureEditorVM(CreatureVM creatureVM)
        {
            ChangeStack = new ChangeStackVM();
            Creature = creatureVM;
            Selection = new SelectionVM();
            ChangeManagement = new ChangeManagementVM(ChangeStack, Selection, Creature);

            ToolCollectionVM = new ToolCollectionVM();
            ToolCollectionVM.Tools.Add(new SelectAllTool());
            ToolCollectionVM.Tools.Add(new PanTool());
            ToolCollectionVM.Tools.Add(new PlaceJointTool());
            ToolCollectionVM.Tools.Add(new PlaceBoneTool());

            Behaviours = new ObservableCollection<BaseBehaviour>
            {
                new UndoBehaviour(ChangeStack.UndoCommand),
                new RedoBehaviour(ChangeStack.RedoCommand),
                new CopyBehaviour(),
                new CutBehaviour(),
                new PasteBehaviour(),
                new RotateLeftBehaviour(),
                new RotateRightBehaviour(),
                new FlipHorizontalBehaviour(),
                new FlipVerticalBehaviour(),
                new SaveBehaviour(),
                new DeleteBehaviour(),
                new ClearBehaviour()
            };

            Behaviours.CollectionChanged += CollectionChanged;
            ToolCollectionVM.Tools.CollectionChanged += CollectionChanged;

            CreatureStructureEditorCanvasVM = new CreatureStructureEditorCanvasVM(ChangeStack, ToolCollectionVM, Behaviours);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(InputBindings));
    }
}