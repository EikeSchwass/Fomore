using System.Windows.Input;

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
    }
}
