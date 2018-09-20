namespace Fomore.UI.ViewModel.Navigation
{
    /// <summary>
    /// The base class for the tab pages in the main window.
    /// </summary>
    public abstract class TabPageVM : ViewModelBase
    {
        private bool isEnabled = true;

        /// <summary>
        /// The Header of the Tab Page.
        /// </summary>
        public abstract string Header { get; }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (value == isEnabled) return;
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public virtual void OnSelect(object obj)
        {

        }
    }
}