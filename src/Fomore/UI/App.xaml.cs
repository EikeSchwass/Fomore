using System.Globalization;
using System.Threading;
using System.Windows;

namespace Fomore.UI
{
    public partial class App
    {
        public static App Instance { get; private set; }

        public App()
        {
            Instance = Instance ?? this;
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