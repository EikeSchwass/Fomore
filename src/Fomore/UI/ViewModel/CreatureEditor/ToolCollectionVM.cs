using System.Collections.ObjectModel;
using System.Linq;
using Fomore.UI.ViewModel.CreatureEditor.Tools;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class ToolCollectionVM : ViewModelBase
    {
        public InfoMessageCollection InfoMessageCollection { get; set; }
        private Tool selectedTool;

        public Tool SelectedTool
        {
            get => selectedTool;
            private set
            {
                if (Equals(value, selectedTool)) return;

                selectedTool?.Deselect();
                selectedTool = value;
                OnPropertyChanged();
                selectedTool?.Select(InfoMessageCollection);
            }
        }

        public ObservableCollection<Tool> Tools { get; }

        public ToolCollectionVM()
        {
            Tools = new ObservableCollection<Tool>();
            Tools.CollectionChanged += ToolsCollectionChanged;
        }

        private void ToolsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var newItem in e.NewItems?.OfType<Tool>() ?? Enumerable.Empty<Tool>())
            {
                if (SelectedTool == null)
                    SelectedTool = newItem;
                newItem.SelectionRequested += ToolSelectionRequested;
            }
            foreach (var oldItem in e.OldItems?.OfType<Tool>() ?? Enumerable.Empty<Tool>())
            {
                oldItem.SelectionRequested -= ToolSelectionRequested;
            }
        }

        private void ToolSelectionRequested(object sender, ToolEventArgs toolEventArgs)
        {
            SelectedTool = toolEventArgs.Tool;
        }

    }
}