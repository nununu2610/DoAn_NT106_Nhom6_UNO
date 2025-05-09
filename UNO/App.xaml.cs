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

            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var menu = new Views.Menu();
            this.MainWindow = menu; // giữ tham chiếu chính
            menu.Show();
        }
    }
}
