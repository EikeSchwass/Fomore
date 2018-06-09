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

        /// <summary>
        /// Holds a reference to the <see cref="NavigationVM"/> instance the view currently uses.
        /// </summary>
        public NavigationVM NavigationVM { get; set; }
    }
}