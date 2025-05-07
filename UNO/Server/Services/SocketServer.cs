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
        private bool _isRunning;

        public void Start(int port)
        {
            if (_isRunning) return;

            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;

            _listenThread = new Thread(ListenForClients)
            {
                IsBackground = true
            };
            _listenThread.Start();

            Console.WriteLine($"Server started on port {port}");
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;
            _listener.Stop();

            try
            {
                _listenThread?.Join(); // đợi thread kết thúc
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error stopping thread: " + ex.Message);
            }

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

                    Thread clientThread = new Thread(HandleClientComm)
                    {
                        IsBackground = true
                    };
                    clientThread.Start(client);
                }
                catch (SocketException ex)
                {
                    if (_isRunning)
                        Console.WriteLine("Socket exception: " + ex.Message);
                    break;
                }
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient client = clientObj as TcpClient;

            try
            {
                using NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {received}");

                    // Example: Echo back
                    byte[] response = Encoding.UTF8.GetBytes("Server: " + received);
                    stream.Write(response, 0, response.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client error: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
