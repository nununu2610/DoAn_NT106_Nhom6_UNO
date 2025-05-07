using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UNO.Server.Services
{
    /// <summary>
    /// Interaction logic for GameServer.xaml
    /// </summary>
    public partial class GameServer : Window
    {

        private SocketServer server;
        public GameServer()
        {
            server = new SocketServer();
            server.Start(5000);
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            server?.Stop(); // dừng server khi đóng form
            base.OnClosed(e);
        }
    }
}
