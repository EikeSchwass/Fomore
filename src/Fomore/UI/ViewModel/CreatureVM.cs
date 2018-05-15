using System;
using System.Windows.Input;
using System.Windows.Media;
using Core.Creatures;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class CreatureVM : ViewModelBase
    {
        private Creature Creature { get; }

        public string Name
        {
            get => Creature.CreatureInformation.Name;
            set
            {
                if (value == Creature.CreatureInformation.Name) return;
                Creature.CreatureInformation.Name = value;
                OnPropertyChanged();
            }
        }

        public ImageSource PreviewImage => null;

        public ICommand CloneCreatureCommand { get; }
        public ICommand EditCreatureCommand { get; }
        public ICommand RemoveCreatureCommand { get; }

        public string Description => $"Created on: {Creature.CreatureInformation.CreateDateTime.ToShortDateString()} {Creature.CreatureInformation.CreateDateTime.ToShortTimeString()}";

        public CreatureVM(Creature creature)
        {
            Creature = creature ?? throw new ArgumentException(nameof(creature));
            CloneCreatureCommand = new DelegateCommand(o => OnCloneRequested(), o => true);
            EditCreatureCommand = new DelegateCommand(o => OnEditViewRequested(), o => true);
            RemoveCreatureCommand = new DelegateCommand(o => OnRemoveRequested(), o => true);
        }

        public event Action<CreatureVM> CloneRequested;
        public event Action<CreatureVM> EditViewRequested;
        public event Action<CreatureVM> RemoveRequested;

        public void OnCloneRequested()
        {
            CloneRequested?.Invoke(new CreatureVM(Creature.Clone()));
        }

        public void OnEditViewRequested() => EditViewRequested?.Invoke(this);
        public void OnRemoveRequested() => RemoveRequested?.Invoke(this);

        public CreatureVM Clone() => new CreatureVM(Creature.Clone());
    }
}