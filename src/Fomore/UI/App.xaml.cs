using System;
using System.Threading.Tasks;
using System.Windows;

namespace Fomore.UI
{
    public partial class App
    {
        public static App Instance { get; private set; }

        public AppState AppState { get; } = new AppState();

        public App()
        {
            Instance = Instance ?? this;
        }

        private void ApplicationStartUp(object sender, StartupEventArgs e)
        {
            var splashScreen = new SplashScreen("assets/images/splash.png");
            splashScreen.Show(false);
            Task.Delay(3500).Wait();
            splashScreen.Close(TimeSpan.Zero);
        }
    }
}
