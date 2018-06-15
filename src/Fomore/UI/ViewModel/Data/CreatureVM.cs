using System.Linq;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Helper;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The Viewmodel that encapsulates the Creature class
    /// </summary>
    public class CreatureVM : ViewModelBase<Creature>
    {
        private string description;
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
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
            }
        }

        public ReadOnlyObservableCollection<MovementPatternVM> MovementPatterns { get; }

        public CollectionAccess<MovementPatternVM> MovementPatternCollectionAccess { get; }

        public DelegateCommand<MovementPatternVM> AddMovementPatternCommand { get; }
        public DelegateCommand<MovementPatternVM> RemoveMovementPatternCommand { get; }
        public DelegateCommand<MovementPatternVM> ClearMovementPatternsCommand { get; }

        /// <inheritdoc />
        public CreatureVM(Creature model) : base(model)
        {
            MovementPatternCollectionAccess = ReadOnlyObservableCollection<MovementPatternVM>.Create();
            MovementPatterns = MovementPatternCollectionAccess.Collection;
            AddMovementPatternCommand = new DelegateCommand<MovementPatternVM>(AddMovementPattern, o => true);
            RemoveMovementPatternCommand = new DelegateCommand<MovementPatternVM>(RemoveMovementPattern, o => MovementPatterns.Any());
            ClearMovementPatternsCommand = new DelegateCommand<MovementPatternVM>(ClearMovementPattern, o => MovementPatterns.Any());
        }

        private void AddMovementPattern(MovementPatternVM movementPattern)
        {
            MovementPatternCollectionAccess.Add(movementPattern);
            // Todo add moventmentpattern to creature
            RemoveMovementPatternCommand.OnCanExecuteChanged();
            ClearMovementPatternsCommand.OnCanExecuteChanged();
        }

        private void RemoveMovementPattern(MovementPatternVM movementPattern)
        {
            MovementPatternCollectionAccess.Remove(movementPattern);
            // Todo add moventmentpattern to creature
            RemoveMovementPatternCommand.OnCanExecuteChanged();
            ClearMovementPatternsCommand.OnCanExecuteChanged();
        }

        private void ClearMovementPattern(MovementPatternVM movementPattern)
        {

            MovementPatternCollectionAccess.Clear();
            // Todo add moventmentpattern to creature
            RemoveMovementPatternCommand.OnCanExecuteChanged();
            ClearMovementPatternsCommand.OnCanExecuteChanged();
        }
    }
}