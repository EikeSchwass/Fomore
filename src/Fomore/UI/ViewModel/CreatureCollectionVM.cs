using System.Windows.Input;
using Core.Creatures;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class CreatureCollectionVM : ListVM<CreatureVM>
    {
        public ICommand AddNewCreatureCommand { get; }

        public CreatureCollectionVM()
        {
            AddNewCreatureCommand = new DelegateCommand(o => AddNewCreature((string)o), o => true);
            Add(new CreatureVM(new Creature()));
            Add(new CreatureVM(new Creature()));
            Add(new CreatureVM(new Creature()));
        }

        /// <inheritdoc />
        protected override void OnItemAdded(CreatureVM item)
        {
            base.OnItemAdded(item);
            item.CloneRequested += CloneCreature;
            item.EditViewRequested += EditCreature;
            item.RemoveRequested += RemoveCreature;
        }

        /// <inheritdoc />
        protected override void OnItemRemoved(CreatureVM item)
        {
            base.OnItemRemoved(item);
            item.CloneRequested -= CloneCreature;
            item.EditViewRequested -= EditCreature;
            item.RemoveRequested -= RemoveCreature;
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

        private void AddNewCreature(string name)
        {
            var creature = new Creature();
            creature.CreatureInformation.Name = name;
            Add(new CreatureVM(creature));
        }
    }
}