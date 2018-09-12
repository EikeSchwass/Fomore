using System;
using System.Windows;
using Fomore.UI.ViewModel.Application;

namespace Fomore.UI.Views.Windows
{
    public partial class MainWindow
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            if(Instance!=null)
                throw new NotSupportedException("Only one main window can exist at a time");
            Instance = this;
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)sender;
            var mainWindowDataContext = (AppStateVM)mainWindow.DataContext;
            mainWindowDataContext.LoadCommand?.Execute(null);
        }
    }
}
