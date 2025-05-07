using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UNO.Server.Services
{
    public class SocketServer
    {
        private TcpListener _listener;
        private Thread _listenThread;
        private bool _isRunning = false;

        public void Start(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;

            _listenThread = new Thread(ListenForClients);
            _listenThread.Start();

            Console.WriteLine($"Server started on port {port}");
        }

        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
            _listenThread.Join(); // đợi thread kết thúc
            Console.WriteLine("Server stopped.");
        }

        private void ListenForClients()
        {
            while (_isRunning)
            {
                try
                {
                    TcpClient client = _listener.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    Thread clientThread = new Thread(HandleClientComm);
                    clientThread.Start(client);
                }
                catch (SocketException)
                {
                    // Thường xảy ra khi Stop() gọi _listener.Stop()
                    break;
                }
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {received}");
            }

            client.Close();
        }
    }
}
