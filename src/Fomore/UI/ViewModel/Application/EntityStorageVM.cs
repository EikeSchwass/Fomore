using System.Linq;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Application
{
    public class EntityStorageVM : ViewModelBase<EntitiyStorage>
    {
        public ReadOnlyObservableCollection<CreatureVM> Creatures { get; }
        public ReadOnlyObservableCollection<EnvironmentVM> Environments { get; }

        /// <inheritdoc />
        public EntityStorageVM(EntitiyStorage model) : base(model)
        {
            CreatureCollectionAccess = ReadOnlyObservableCollection<CreatureVM>.Create();
            EnvironmentCollectionAccess = ReadOnlyObservableCollection<EnvironmentVM>.Create();
            Creatures = CreatureCollectionAccess.Collection;
            Environments = EnvironmentCollectionAccess.Collection;

            AddCreatureCommand = new DelegateCommand<CreatureVM>(AddCreature, o => true);
            RemoveCreatureCommand = new DelegateCommand<CreatureVM>(RemoveCreature, o => Creatures.Any());
            ClearCreaturesCommand = new DelegateCommand<CreatureVM>(ClearCreatures, o => Creatures.Any());
            AddEnvironmentCommand = new DelegateCommand<EnvironmentVM>(AddEnvironment, o => true);
            RemoveEnvironmentCommand = new DelegateCommand<EnvironmentVM>(RemoveEnvironment, o => Environments.Any());
            ClearEnvironmentsCommand = new DelegateCommand<EnvironmentVM>(ClearEnvironments, o => Environments.Any());
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