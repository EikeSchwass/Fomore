using System;
using System.Threading.Tasks;
using System.Windows;

namespace Fomore.UI
{
    public partial class App
    {
        private void ApplicationStartUp(object sender, StartupEventArgs e)
        {
            var splashScreen = new SplashScreen("assets/images/splash.png");
            splashScreen.Show(false);
            Task.Delay(2500).Wait();
            splashScreen.Close(TimeSpan.Zero);
        }
    }
}
