using System.Threading.Tasks;
using System.Windows;

namespace Fomore.UI.Views.Windows
{
    /// <summary>
    /// Interaction logic for DummyProgressWindow.xaml
    /// </summary>
    public partial class DummyProgressWindow
    {
        public DummyProgressWindow(Window owner)
        {
            Owner = owner;
            InitializeComponent();
        }

        private async void DummyProgressWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Parallel.ForEach(creatures, creature => { creature.Train(); });
            // Task[] tasks = new Task[]{Task.Delay(50),Task.Delay(500),Task.Delay(50)};
            // await Task.WhenAll(tasks);
            // double a = await Task.Run(() =>
            //                           {
            //                               Thread.Sleep(2000);
            //                               return 0;
            //                           });
            await Task.Delay(2000);
            Close();
        }

        // private void continued() { }
    }
}