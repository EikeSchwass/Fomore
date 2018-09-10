using System;
using System.Windows;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.CreatureEditor;
using Fomore.UI.ViewModel.Data;

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
