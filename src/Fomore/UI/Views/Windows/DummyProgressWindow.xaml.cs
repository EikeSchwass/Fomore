using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fomore.UI.Views.Windows
{
    /// <summary>
    /// Interaction logic for DummyProgressWindow.xaml
    /// </summary>
    public partial class DummyProgressWindow : Window
    {
        public DummyProgressWindow()
        {
            InitializeComponent();
        }

        private async void DummyProgressWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Started");
            // Parallel.ForEach(creatures, creature => { creature.Train(); });
            // Task[] tasks = new Task[]{Task.Delay(50),Task.Delay(500),Task.Delay(50)};
            // await Task.WhenAll(tasks);
            // double a = await Task.Run(() =>
            //                           {
            //                               Thread.Sleep(2000);
            //                               return 0;
            //                           });
            await Task.Delay(2000);
            Console.WriteLine("Stopped");
            Close();
        }

        private void continued()
        {

        }
    }
}
