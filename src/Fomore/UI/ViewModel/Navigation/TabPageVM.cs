namespace Fomore.UI.ViewModel.Navigation
{
    /// <summary>
    /// The base class for the tab pages in the main window.
    /// </summary>
    public abstract class TabPageVM : ViewModelBase
    {
        /// <summary>
        /// The Header of the Tab Page.
        /// </summary>
        public abstract string Header { get; }

        public virtual void OnSelect(object obj)
        {

        }
    }
}