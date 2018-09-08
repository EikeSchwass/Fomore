﻿using System.Windows;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;


namespace Fomore.UI.ViewModel.Navigation
{
    public class TrainingTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Training";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        private EnvironmentVM selectedEnvironment;
        private MovementPatternVM selectedMovementPattern;
        private CreatureVM selectedCreature;
        private bool showTraining;
        private double targetSpeed;

        public CreatureVM SelectedCreature
        {
            get => selectedCreature;
            set
            {
                if (Equals(value, selectedCreature)) return;
                selectedCreature = value;
                OnPropertyChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
                StartTrainingCommand.OnCanExecuteChanged();
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
                ResetSelectionCommand.OnCanExecuteChanged();
                StartTrainingCommand.OnCanExecuteChanged();
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
                ResetSelectionCommand.OnCanExecuteChanged();
                StartTrainingCommand.OnCanExecuteChanged();
            }
        }

        public double TargetSpeed
        {
            get => targetSpeed;
            set
            {
                if (value.Equals(targetSpeed)) return;
                targetSpeed = value;
                OnPropertyChanged();
            }
        }

        public bool ShowTraining
        {
            get => showTraining;
            set
            {
                if (value == showTraining) return;
                showTraining = value;
                OnPropertyChanged();
            }
        }

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------
        public DelegateCommand ResetSelectionCommand { get; }
        public DelegateCommand StartTrainingCommand { get; }

        private void ResetSelectionAction(object obj)
        {
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
            TargetSpeed = 0.0;
            ShowTraining = false;
        }

        private void StartTrainingAction(object obj)
        {
            SelectedMovementPattern.Iterations++;
            MessageBox.Show("The training process has started...\n\nParameters:\nCreature:\t\t\t" + SelectedCreature.Name + "\nMovement Pattern:\t" + SelectedMovementPattern.Name + "\nEnvironment:\t\t" + SelectedEnvironment.Name + "\nShow Progress:\t\t" + (ShowTraining ? "Yes" : "No"), "Training", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------

        public TrainingTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;

            // Init commands
            ResetSelectionCommand = new DelegateCommand(ResetSelectionAction,
                                                        o => SelectedCreature != null ||
                                                             SelectedMovementPattern != null ||
                                                             SelectedEnvironment != null);
            StartTrainingCommand = new DelegateCommand(StartTrainingAction,
                                                         o => SelectedCreature != null &&
                                                              SelectedMovementPattern != null &&
                                                              SelectedEnvironment != null);
        }

        public override void OnSelect(object obj)
        {
            if (obj is CreatureVM vm)
                SelectedCreature = vm;
        }
    }
}
