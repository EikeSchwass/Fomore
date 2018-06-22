using System;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;


namespace Fomore.UI.ViewModel.Navigation
{
    public class TrainingTabVM : TabPageVM
    {
        private CreatureVM selectCreature;
        private EnvironmentVM selectEnvironment;
        private MovementPatternVM selectMovementPattern;
        private float speed;
        private bool show;


        public EntityStorageVM EntityStorageVM { get; }

        public CreatureVM SelectCreature
        {
            get => selectCreature;
            set
            {
                if (Equals(value, selectCreature)) return;
                selectCreature = value;
                OnPropertyChanged();
                StartTrainingCommand.OnCanExecuteChanged();
                ResetCommand.OnCanExecuteChanged();
            }

        }

        public MovementPatternVM SelecMovementPattern
        {
            get => selectMovementPattern;
            set
            {
                if (Equals(value, selectMovementPattern)) return;
                selectMovementPattern = value;
                OnPropertyChanged();
                StartTrainingCommand.OnCanExecuteChanged();
                ResetCommand.OnCanExecuteChanged();
            }
        }
        public EnvironmentVM SelectEnvironment
        {
            get => selectEnvironment;
            set
            {
                if (Equals(value, selectEnvironment)) return;
                selectEnvironment = value;
                OnPropertyChanged();
                StartTrainingCommand.OnCanExecuteChanged();
                ResetCommand.OnCanExecuteChanged();
            }
        }

        public bool SelectShow
        {
            get => show;
            set
            {
                if(Equals(value,show)) return;
                show = value;
                StartTrainingCommand.OnCanExecuteChanged();
                ResetCommand.OnCanExecuteChanged();
            }
        }

        public float SelectSpeed
        {
            get => speed;
            set
            {
                if(Equals(value,speed)) return;
                speed = value;
                ResetCommand.OnCanExecuteChanged();
            }
        }
        public TrainingTabVM(EntityStorageVM entitiesStorage)
        {
            EntityStorageVM = entitiesStorage;
            StartTrainingCommand = new DelegateCommand(StartTraining, o => SelectCreature != null && SelectEnvironment != null && speed!=0);

            ResetCommand = new DelegateCommand(Reset, o => SelectCreature != null || SelecMovementPattern != null || SelectEnvironment != null || speed!=0 || show == true );
        }

        public DelegateCommand StartTrainingCommand { get; }
        public DelegateCommand ResetCommand { get; }

        //Train
        private void StartTraining(object obj)
        {
        }

        // Reset
        private void Reset(object obj)
        {
            SelectCreature = null;
            SelecMovementPattern = null;
            SelectEnvironment = null;
            speed = 0;
            show = false;
        }

        /// <inheritdoc />
        public override string Header => "Training";
    }
}
