// Eike Stein: Fomore/UI/SimulationVM.cs (2018/09/19)

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Physics;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Traning
{
    public class SimulationVM : ViewModelBase<Simulation>
    {
        public DelegateCommand StartSimulationCommand { get; }
        public DelegateCommand StopSimulationCommand { get; }

        private Task SimulationTask { get; set; }
        private CancellationTokenSource CancellationTokenSource { get; set; }

        public ObservableCollection<SimulationEntityVM> SimulationEntities { get; } = new ObservableCollection<SimulationEntityVM>();

        /// <inheritdoc />
        public SimulationVM(Simulation model) : base(model)
        {
            model.TimeStepCompleted += SimulationTimeStepCompleted;
            model.SimulationReset += SimulationReset;

            StartSimulationCommand = new DelegateCommand(o => Start(), o => !IsRunning);
            StopSimulationCommand = new DelegateCommand(o => Stop(), o => IsRunning);

            var simulationEntities = model.SimulationEntities.Select(s => new SimulationEntityVM(s));
            foreach (var simulationEntity in simulationEntities)
            {
                SimulationEntities.Add(simulationEntity);
            }
        }

        public bool IsRunning { get; set; }

        private void Stop()
        {
            CancellationTokenSource?.Cancel();
            IsRunning = false;
            SimulationTask?.Wait();
        }

        private void Start()
        {
            IsRunning = true;
            CancellationTokenSource = new CancellationTokenSource();
            SimulationTask = Task.Run(() => { RunSimulation(); });
        }

        private void RunSimulation()
        {
            var stopwatch = Stopwatch.StartNew();
            while (!CancellationTokenSource.Token.IsCancellationRequested)
            {
                Model.Tick(stopwatch.ElapsedMilliseconds / 1000.0f);
            }
        }

        private void SimulationTimeStepCompleted()
        {
            foreach (var simulationEntityVM in SimulationEntities)
            {
                simulationEntityVM.OnUpdated();
            }
        }

        private void SimulationReset()
        {
            SimulationEntities.Clear();
            var simulationEntities = Model.SimulationEntities.Select(s => new SimulationEntityVM(s));
            foreach (var simulationEntity in simulationEntities)
            {
                SimulationEntities.Add(simulationEntity);
            }
        }

    }
}