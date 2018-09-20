using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Training;
using Core.Training.Evolution;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class TrainingTabVM : TabPageVM
    {
        private int iterationProgress;
        private int iterations;
        private bool newMovementPattern;
        private CreatureVM selectedCreature;

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        private EnvironmentVM selectedEnvironment;
        private MovementPatternVM selectedMovementPattern;
        private bool showTraining;
        private double targetSpeed;
        private double trainingProgress;
        private string trainingProgressString;
        private bool trainingRunning;

        /// <inheritdoc />
        public override string Header => "Training";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

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
                if (value != null) NewMovementPattern = false;
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

        public bool NewMovementPattern
        {
            get => newMovementPattern;
            set
            {
                if (value == newMovementPattern) return;
                newMovementPattern = value;
                if (value)
                {
                    SelectedMovementPattern = null;
                    OnPropertyChanged(nameof(SelectedMovementPattern));
                }

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

        public int Iterations
        {
            get => iterations;
            set
            {
                if (value == iterations) return;
                iterations = value;
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

        public bool TrainingRunning
        {
            get => trainingRunning;
            set
            {
                if (value == trainingRunning) return;
                trainingRunning = value;
                OnPropertyChanged();
            }
        }

        public double TrainingProgress
        {
            get => trainingProgress;
            set
            {
                if (Equals(value, trainingProgress)) return;
                trainingProgress = value;
                OnPropertyChanged();
            }
        }

        public int IterationProgress
        {
            get => iterationProgress;
            set
            {
                if (value == iterationProgress) return;
                iterationProgress = value;
                OnPropertyChanged();
            }
        }

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------

        public DelegateCommand ResetSelectionCommand { get; }
        public DelegateCommand StartTrainingCommand { get; }
        public DelegateCommand StopTrainingCommand { get; }

        public string TrainingProgressString
        {
            get => trainingProgressString;
            set
            {
                if (value == trainingProgressString) return;
                trainingProgressString = value;
                OnPropertyChanged();
            }
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
                                                            (SelectedMovementPattern != null || NewMovementPattern) &&
                                                            SelectedEnvironment != null);
            StopTrainingCommand = new DelegateCommand(StopTrainingAction, o => true);

            EntitiesStorage.Creatures.CollectionChanged += CreatureStorageChanged;
            EntitiesStorage.Environments.CollectionChanged += EnvironmentStorageChanged;

            Iterations = 1;
        }

        private void CreatureStorageChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (EntitiesStorage.Creatures.Contains(SelectedCreature))
                return;
            SelectedCreature = null;
            SelectedMovementPattern = null;
        }

        private void EnvironmentStorageChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (EntitiesStorage.Environments.Contains(SelectedEnvironment))
                return;
            SelectedEnvironment = null;
        }

        private void ResetSelectionAction(object obj)
        {
            NewMovementPattern = false;
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
            TargetSpeed = 0.0;
            ShowTraining = false;
        }

        private async void StartTrainingAction(object obj)
        {
            var movementPattern = SelectedMovementPattern;
            if (movementPattern == null)
            {
                movementPattern = new MovementPatternVM(MovementPattern.CreateFromCreature(SelectedCreature.Model))
                {
                    Name = SelectedCreature.Name + " on " + SelectedEnvironment.Name
                };
            }

            int index = 0;
            Individual<MovementPattern> best = null;
            var traningSession =
                new TraningSession(new TrainingSettings(SelectedCreature.Model,
                                                        movementPattern.Model,
                                                        SelectedEnvironment.Model,
                                                        Iterations));
            IsEnabled = false;
            TrainingRunning = true;
            TrainingProgress = 0.25 / Iterations;
            TrainingProgressString = "Training starting...";
            await traningSession.RunTrainingSessionAsync((t, r) =>
                                                         {
                                                             var individuals = r.OrderByDescending(k => k.Fitness).ToList();
                                                             var max = individuals.First().Fitness;
                                                             var min = individuals.Last().Fitness;
                                                             var average = individuals.Average(k => k.Fitness);
                                                             var mean = individuals[individuals.Count / 2].Fitness;
                                                             index++;
                                                             TrainingProgress = index * 100.0 / Iterations;
                                                             string s =
                                                                 $"Generation #{index}: MAX:{max:F3}, MIN:{min:F3}, AVG:{average:F3}, MEAN:{mean:F3}";
                                                             TrainingProgressString = s;
                                                             Debug.WriteLine(s);
                                                             if ((best?.Fitness ?? 0) <= max || best == null)
                                                                 best = individuals.First();
                                                             return false;
                                                         });
            IsEnabled = true;
            TrainingProgress = 0;
            TrainingRunning = false;
            TrainingProgressString = "";
            SystemSounds.Beep.Play();
            var newPattern = new MovementPatternVM(best.Phenotype);

            SelectedCreature.MovementPatternCollectionVM.Add(newPattern);
            SelectedMovementPattern = newPattern;
        }

        private void StopTrainingAction(object obj)
        {
            TrainingRunning = false;
        }

        private async void RunVisualTraining()
        {
            TrainingProgress = 0;
            TrainingRunning = true;
            await Task.Run(() =>
                           {
                               for (int it = 0; it < Iterations; it++)
                               {
                                   IterationProgress = 0;
                                   for (int i = 1; i <= 1000; i++)
                                   {
                                       IterationProgress = i;
                                       TrainingProgress = (int)((double)it * 1000 / Iterations + (double)i / Iterations);
                                       Thread.Sleep(1);
                                   }
                               }
                           });
            TrainingRunning = false;
        }

        public override void OnSelect(object obj)
        {
            if (obj is CreatureVM vm)
                SelectedCreature = vm;

            if (obj is CreatureTabVM.CreatureMovementPattern cmp)
            {
                if (cmp.Creature != null)
                    SelectedCreature = cmp.Creature;
                if (cmp.MovementPattern != null)
                    SelectedMovementPattern = cmp.MovementPattern;
            }
        }
    }
}