using System;
using Core;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The Viewmodel that encapsulates the Creature class
    /// </summary>
    public class CreatureVM : ViewModelBase<Creature>, ICloneable<CreatureVM>
    {
        private DateTime lastAccess;

        public string Name
        {
            get => Model.CreatureName;
            set
            {
                if (value == Model.CreatureName) return;
                Model.CreatureName = value;
                OnPropertyChanged();
                OnAccess();
            }
        }
        public string Description
        {
            get => Model.CreatureDescription;
            set
            {
                if (value == Model.CreatureDescription) return;
                Model.CreatureDescription = value;
                OnPropertyChanged();
                OnAccess();
            }
        }

        public DateTime LastAccess
        {
            get => lastAccess;
            private set
            {
                if (value.Equals(lastAccess)) return;
                lastAccess = value;
                OnPropertyChanged();
            }
        }

        public EncapsulatingObservableCollection<MovementPatternVM, MovementPattern> MovementPatternCollectionVM { get; }

        public CreatureStructureVM CreatureStructureVM { get; }

        /// <inheritdoc />
        public CreatureVM(Creature creature) : base(creature)
        {
            MovementPatternCollectionVM = new EncapsulatingObservableCollection<MovementPatternVM, MovementPattern>(creature.MovementPatterns, m => new MovementPatternVM(m));
            CreatureStructureVM = new CreatureStructureVM(Model.CreatureStructure);
        }

        // Todo update on Structure updates as well
        private void OnAccess()
        {
            LastAccess = DateTime.Now;
        }

        /// <inheritdoc />
        public CreatureVM Clone()
        {
            return new CreatureVM(Model.Clone());
        }
    }
}