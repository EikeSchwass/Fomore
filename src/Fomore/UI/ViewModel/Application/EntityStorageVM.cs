using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.Application
{
    public class EntityStorageVM : ViewModelBase<EntitiyStorage>
    {
        private CreatureVM selectedCreature;
        private MovementPatternVM selectedMovementPattern;
        private EnvironmentVM selectedEnvironment;
        public Helper.ReadOnlyObservableCollection<CreatureVM> Creatures { get; }
        public Helper.ReadOnlyObservableCollection<EnvironmentVM> Environments { get; }

        public Collection<DelegateCommand> EntityDependentCommands { get; set; }

        public CreatureVM SelectedCreature
        {
            get => selectedCreature;
            set
            {
                if (Equals(value, selectedCreature)) return;
                selectedCreature = value;
                OnPropertyChanged();
                foreach (var command in EntityDependentCommands)
                {
                    command.OnCanExecuteChanged();
                }
            }
        }

        public MovementPatternVM SelectedMovementPattern
        {
            get => selectedMovementPattern;
            set
            {
                if (Equals(value, selectedMovementPattern)) return;
                selectedMovementPattern = value;
                OnPropertyChanged();
                foreach (var command in EntityDependentCommands)
                {
                    command.OnCanExecuteChanged();
                }
            }
        }

        public EnvironmentVM SelectedEnvironment
        {
            get => selectedEnvironment;
            set
            {
                if (Equals(value, selectedEnvironment)) return;
                selectedEnvironment = value;
                OnPropertyChanged();
                foreach (var command in EntityDependentCommands)
                {
                    command.OnCanExecuteChanged();
                }
            }
        }

        public DelegateCommand ResetSelectionCommand { get; }

        private void ResetSelectionAction(object obj)
        {
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
        }

        /// <inheritdoc />
        public EntityStorageVM(EntitiyStorage model) : base(model)
        {
            CreatureCollectionAccess = Helper.ReadOnlyObservableCollection<CreatureVM>.Create();
            EnvironmentCollectionAccess = Helper.ReadOnlyObservableCollection<EnvironmentVM>.Create();
            Creatures = CreatureCollectionAccess.Collection;
            Environments = EnvironmentCollectionAccess.Collection;
            EntityDependentCommands = new Collection<DelegateCommand>();

            AddCreatureCommand = new DelegateCommand<CreatureVM>(AddCreature, o => true);
            RemoveCreatureCommand = new DelegateCommand<CreatureVM>(RemoveCreature, o => Creatures.Any());
            ClearCreaturesCommand = new DelegateCommand<CreatureVM>(ClearCreatures, o => Creatures.Any());
            AddEnvironmentCommand = new DelegateCommand<EnvironmentVM>(AddEnvironment, o => true);
            RemoveEnvironmentCommand = new DelegateCommand<EnvironmentVM>(RemoveEnvironment, o => Environments.Any());
            ClearEnvironmentsCommand = new DelegateCommand<EnvironmentVM>(ClearEnvironments, o => Environments.Any());

            ResetSelectionCommand = new DelegateCommand(ResetSelectionAction, o => SelectedCreature != null || SelectedMovementPattern != null || SelectedEnvironment != null);
            EntityDependentCommands.Add(ResetSelectionCommand);
        }

        private void AddCreature(CreatureVM obj)
        {
            CreatureCollectionAccess.Add(obj);
            Model.Creatures.Add(obj.Model);
            RemoveCreatureCommand.OnCanExecuteChanged();
            ClearCreaturesCommand.OnCanExecuteChanged();
        }

        private void RemoveCreature(CreatureVM obj)
        {
            CreatureCollectionAccess.Remove(obj);
            Model.Creatures.Remove(obj.Model);
            RemoveCreatureCommand.OnCanExecuteChanged();
            ClearCreaturesCommand.OnCanExecuteChanged();
        }

        private void ClearCreatures(CreatureVM obj)
        {
            CreatureCollectionAccess.Clear();
            Model.Creatures.Clear();
            RemoveCreatureCommand.OnCanExecuteChanged();
            ClearCreaturesCommand.OnCanExecuteChanged();
        }

        private void AddEnvironment(EnvironmentVM obj)
        {
            EnvironmentCollectionAccess.Add(obj);
            Model.Environments.Add(obj.Model);
            RemoveEnvironmentCommand.OnCanExecuteChanged();
            ClearEnvironmentsCommand.OnCanExecuteChanged();
        }

        private void RemoveEnvironment(EnvironmentVM obj)
        {
            EnvironmentCollectionAccess.Remove(obj);
            Model.Environments.Remove(obj.Model);
            RemoveEnvironmentCommand.OnCanExecuteChanged();
            ClearEnvironmentsCommand.OnCanExecuteChanged();
        }

        private void ClearEnvironments(EnvironmentVM obj)
        {
            EnvironmentCollectionAccess.Clear();
            Model.Environments.Clear();
            RemoveEnvironmentCommand.OnCanExecuteChanged();
            ClearEnvironmentsCommand.OnCanExecuteChanged();
        }

        public CollectionAccess<EnvironmentVM> EnvironmentCollectionAccess { get; }
        public CollectionAccess<CreatureVM> CreatureCollectionAccess { get; }

        public DelegateCommand<CreatureVM> AddCreatureCommand { get; }
        public DelegateCommand<CreatureVM> RemoveCreatureCommand { get; }
        public DelegateCommand<CreatureVM> ClearCreaturesCommand { get; }
        public DelegateCommand<EnvironmentVM> AddEnvironmentCommand { get; }
        public DelegateCommand<EnvironmentVM> RemoveEnvironmentCommand { get; }
        public DelegateCommand<EnvironmentVM> ClearEnvironmentsCommand { get; }

        public void Load()
        {
            // Todo implement Model EntityStorage.Load call
            // Model.LoadEntities();
            CreatureCollectionAccess.AddRange(Model.Creatures.Select(c => new CreatureVM(c)));
            EnvironmentCollectionAccess.AddRange(Model.Environments.Select(e => new EnvironmentVM(e)));
        }
    }
}