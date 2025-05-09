using System.Configuration;
using System.Data;
using System.Windows;

namespace UNO
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.ShutdownMode = ShutdownMode.OnExplicitShutdown; // không tự shutdown khi 1 cửa sổ đóng

            var menu = new Views.Menu();
            this.MainWindow = menu;
            menu.Show();
        }
    }
}
