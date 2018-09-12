using System.ComponentModel;
using System.Windows.Input;
using Fomore.UI.ViewModel.CreatureEditor;

namespace Fomore.UI.Views.Windows
{
    /// <summary>
    /// Interaction logic for CreatureStructureEditor.xaml
    /// </summary>
    public partial class CreatureStructureEditor
    {
        public CreatureStructureEditor()
        {
            InitializeComponent();
        }

        private void CreatureStructureEditor_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System)
                e.Handled = true;
        }

        /// <inheritdoc />
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (DataContext is CreatureEditorVM creatureEditor)
                creatureEditor.CreatureEditorPanelVM.CreatureStructureEditorCanvasVM.InfoMessageCollection.CancelAll();
        }
    }
}
