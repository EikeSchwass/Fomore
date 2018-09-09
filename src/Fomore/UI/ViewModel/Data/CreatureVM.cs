using System;
using System.Linq;
using Core;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The Viewmodel that encapsulates the Creature class
    /// </summary>
    public class CreatureVM : ViewModelBase<Creature>, ICloneable<CreatureVM>
    {
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
            MovementPatternCollectionVM = new EncapsulatingObservableCollection<MovementPatternVM, MovementPattern>(creature.MovementPatterns, creature.MovementPatterns.Select(m => new MovementPatternVM(m)).ToList());
            CreatureStructureVM = new CreatureStructureVM(Model.CreatureStructure);
        }

        /// <inheritdoc />
        public CreatureVM Clone()
        {
            return new CreatureVM(Model.Clone());
        }
    }
}