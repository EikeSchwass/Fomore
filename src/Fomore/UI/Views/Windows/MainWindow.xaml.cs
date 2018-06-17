using System.Windows;
using Fomore.UI.ViewModel.Application;

namespace Fomore.UI.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //var creatureStructureEditor = new CreatureStructureEditor { DataContext = new CreatureEditorVM(new CreatureVM(new Creature())) };
            //creatureStructureEditor.Show();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)sender;
            var mainWindowDataContext = (AppStateVM)mainWindow.DataContext;
            mainWindowDataContext.LoadCommand?.Execute(null);
        }
    }
}
