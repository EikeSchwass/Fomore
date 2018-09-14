using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Fomore.UI.ViewModel.CreatureEditor.Behaviours;
using Fomore.UI.ViewModel.CreatureEditor.Tools;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorPanelVM : ViewModelBase
    {
        public CreatureVM Creature { get; }
        public ToolCollectionVM ToolCollectionVM { get; }
        public ObservableCollection<BaseBehaviour> Behaviours { get; }

        public IEnumerable<IHasInputBinding> InputBindings => ToolCollectionVM.Tools.OfType<IHasInputBinding>().Concat(Behaviours);

        public CreatureStructureEditorCanvasVM CreatureStructureEditorCanvasVM { get; }

        public CreatureEditorPanelVM(CreatureVM creature)
        {
            Creature = creature;
            ToolCollectionVM = new ToolCollectionVM();
            CreatureStructureEditorCanvasVM = new CreatureStructureEditorCanvasVM(Creature, ToolCollectionVM);
            ToolCollectionVM.InfoMessageCollection = CreatureStructureEditorCanvasVM.InfoMessageCollection;
            ToolCollectionVM.Tools.Add(new SelectAllTool());
            ToolCollectionVM.Tools.Add(new MoveTool());
            ToolCollectionVM.Tools.Add(new PanTool());
            ToolCollectionVM.Tools.Add(new PlaceJointTool());
            ToolCollectionVM.Tools.Add(new PlaceBoneTool());

            Behaviours = new ObservableCollection<BaseBehaviour>
            {
                new UndoBehaviour(CreatureStructureEditorCanvasVM.HistoryStack),
                new RedoBehaviour(CreatureStructureEditorCanvasVM.HistoryStack),
                new RotateLeftBehaviour(),
                new RotateRightBehaviour(),
                new FlipHorizontalBehaviour(),
                new FlipVeticalBehaviour(),
                new SaveBehaviour(),
                new DeleteBehaviour(),
                new ClearBehaviour()
            };

            Behaviours.CollectionChanged += CollectionChanged;
            ToolCollectionVM.Tools.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(InputBindings));
    }
}