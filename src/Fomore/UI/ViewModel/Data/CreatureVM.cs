using System;
using Core;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The Viewmodel that encapsulates the Creature class
    /// </summary>
    public class CreatureVM : ViewModelBase<Creature>
    {
        private string name;
        private string description;
        private DateTime lastAccess;

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
                OnAccess();
            }
        }
        public string Description
        {
            get => description;
            set
            {
                if (value == description) return;
                description = value;
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
    }
}