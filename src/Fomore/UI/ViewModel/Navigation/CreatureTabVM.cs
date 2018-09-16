using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.Views.Windows;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.Point;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Creature";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        

        private CreatureVM selectedCreature;
        private MovementPatternVM selectedMovementPattern;

        public CreatureVM SelectedCreature
        {
            get => selectedCreature;
            set
            {
                if (Equals(value, selectedCreature)) return;
                if (selectedCreature != null)
                {
                    selectedCreature.MovementPatternCollectionVM.CollectionChanged -= MovementPatternCollectionChanged;
                }

                selectedCreature = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCreatureMovementPattern));
                if (selectedCreature != null)
                {
                    selectedCreature.MovementPatternCollectionVM.CollectionChanged += MovementPatternCollectionChanged;
                }
                ClearMovementPatternsCommand.OnCanExecuteChanged();
            }
        }
        
        private void MovementPatternCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ClearMovementPatternsCommand.OnCanExecuteChanged();
        }

        public MovementPatternVM SelectedMovementPattern
        {
            get => selectedMovementPattern;
            set
            {
                if (Equals(value, selectedMovementPattern)) return;
                selectedMovementPattern = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCreatureMovementPattern));
            }
        }

        public CreatureMovementPattern SelectedCreatureMovementPattern =>
            new CreatureMovementPattern(SelectedCreature, SelectedMovementPattern);

        public struct CreatureMovementPattern
        {
            public CreatureVM Creature { get; }
            public MovementPatternVM MovementPattern { get; }

            public CreatureMovementPattern(CreatureVM creature, MovementPatternVM movementPattern)
            {
                Creature = creature;
                MovementPattern = movementPattern;
            }
        }

       

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------

        public DelegateCommand NewCreatureCommand { get; }
        public DelegateCommand TrainCommand { get; }
        public DelegateCommand SimulateCommand { get; }
        public DelegateCommand EditCreatureCommand { get; }
        public DelegateCommand CloneCommand { get; }
        public DelegateCommand ClearMovementPatternsCommand { get; }

        private void NewCreatureAction(object obj)
        {
            var creature = new Creature() {Name = "New Creature", Description = "No description available..."};
            var creatureVM = new CreatureVM(creature);
            EntitiesStorage.AddCreatureCommand.Execute(creatureVM);
            SelectedCreature = creatureVM;
        }

        private void TrainAction(object obj)
        {
            TabNavigationVM.SwitchToTrainingTabCommand.Execute(obj);
        }

        private void SimulateAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(obj);
        }

        private void EditAction(object obj)
        {
            if (obj is CreatureVM creature)
            {
                var creatureStructureEditor = new CreatureStructureEditor {DataContext = new CreatureEditorVM(creature)};
                creatureStructureEditor.Show();
            }
        }

        private void CloneAction(object obj)
        {
            if (obj is CreatureVM creature)
            {
                var clone = creature.Clone();
                clone.Name = "Clone of " + clone.Name;
                EntitiesStorage.AddCreatureCommand.Execute(clone);
                SelectedCreature = clone;
            }
        }

        private void ClearMovementPatternsAction(object obj)
        {
            SelectedCreature?.MovementPatternCollectionVM.Clear();
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------
        public CreatureTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;
            NewCreatureCommand = new DelegateCommand(NewCreatureAction, o => true);
            TrainCommand = new DelegateCommand(TrainAction, o => true);
            SimulateCommand = new DelegateCommand(SimulateAction, o => true);
            EditCreatureCommand = new DelegateCommand(EditAction, o => true);
            CloneCommand = new DelegateCommand(CloneAction, o => true);
            ClearMovementPatternsCommand = new DelegateCommand(ClearMovementPatternsAction, o => SelectedCreature?.MovementPatternCollectionVM.Count > 0);
        }
    }
}