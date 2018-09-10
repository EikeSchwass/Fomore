using System.Globalization;
using System.Threading;
using System.Windows;
using Fomore.UI.ViewModel.Application;

namespace Fomore.UI
{
    public partial class App
    {
        public static App Instance { get; private set; }

        public App()
        {
            Instance = Instance ?? this;
            DispatcherUnhandledException += (o, e) =>
                                            {
                                                if (
                                                    MessageBox
                                                       .Show($"An error occured ({e.Exception.Message}). Do you want to save everything, although data may be corrupted?",
                                                             "Error",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Error) ==
                                                    MessageBoxResult.Yes)
                                                {
                                                    Cleanup();
                                                }

                                                e.Handled = true;
                                                Shutdown(-1);
                                            };
        }

        /// <inheritdoc />
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Cleanup();
        }

        private void Cleanup()
        {
            var appStateVM = (AppStateVM)Views.Windows.MainWindow.Instance.DataContext;
            appStateVM?.EntitiesStorageVM.Save();
        }

        private void ApplicationStartUp(object sender, StartupEventArgs e)
        {
#if DEBUG
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
#else
            var splashScreen = new SplashScreen("assets/images/splash.png");
            splashScreen.Show(false);
            Task.Delay(3500).Wait();
            splashScreen.Close(TimeSpan.Zero);
#endif
        }
    }
}