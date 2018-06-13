using Core;
using Fomore.UI.ViewModel.CreatureEditor;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var creatureStructureEditor = new CreatureStructureEditor { DataContext = new CreatureEditorVM(new CreatureVM(new Creature())) };
            creatureStructureEditor.Show();
        }
    }
}
