using System.Windows.Input;
using Core.Creatures;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class CreatureCollectionVM : ListVM<CreatureVM>
    {
        public ICommand AddNewCreatureCommand { get; }
        public ICommand CloneCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public CreatureCollectionVM()
        {
            AddNewCreatureCommand = new DelegateCommand(o => AddNewCreature(), o => true);
            CloneCommand = new DelegateCommand(o => CloneCreature((CreatureVM)o), o => true);
            EditCommand = new DelegateCommand(o => EditCreature((CreatureVM)o), o => true);
            RemoveCommand = new DelegateCommand(o => RemoveCreature((CreatureVM)o), o => true);
            Add(new CreatureVM(new Creature()));
        }

        private void RemoveCreature(CreatureVM creature)
        {
            Remove(creature);
        }

        private void EditCreature(CreatureVM creature)
        {

        }

        private void CloneCreature(CreatureVM creatureVM)
        {
            Add(creatureVM.Clone());
        }

        private void AddNewCreature()
        {
            var creature = new Creature();
            Add(new CreatureVM(creature));
            AppState.ViewModelNavigator.PushView(new CreatureEditorVM());
        }
    }
}