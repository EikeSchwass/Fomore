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
            get => Model.Name;
            set
            {
                if (value == Model.Name) return;
                Model.Name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastAccess));
            }
        }
        public string Description
        {
            get => Model.Description;
            set
            {
                if (value == Model.Description) return;
                Model.Description = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastAccess));
            }
        }

        public DateTime LastAccess => Model.LastAccess;

        public EncapsulatingObservableCollection<MovementPatternVM, MovementPattern> MovementPatternCollectionVM { get; }

        public CreatureStructureVM CreatureStructureVM { get; }

        /// <inheritdoc />
        public CreatureVM(Creature creature) : base(creature)
        {
            MovementPatternCollectionVM = new EncapsulatingObservableCollection<MovementPatternVM, MovementPattern>(creature.MovementPatterns, m => new MovementPatternVM(m));
            CreatureStructureVM = new CreatureStructureVM(Model.CreatureStructure);
        }

        /// <inheritdoc />
        public CreatureVM Clone()
        {
            return new CreatureVM(Model.Clone());
        }
    }
}