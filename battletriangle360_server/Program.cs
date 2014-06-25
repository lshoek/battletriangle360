using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultiClientGameServer;

namespace MultiClientGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server();
        }
    }

    class Server
    {
        TcpListener listener;
        Boolean connected = false;
        Thread clientAssignmentThread;
        const int MAXCLIENTS = 8;
        ClientArray connectedClients = new ClientArray(MAXCLIENTS);

        public Server()
        {
            Console.WriteLine("Server started!");
            listener = new TcpListener(IPAddress.Parse(GetLocalIPAddress()), 8888);
            while (!connected)
            {
                try
                {
                    listener.Start();
                    Console.WriteLine("Succesfully set up a connection.");
                    Console.WriteLine("SERVER ADDRESS: " + listener.LocalEndpoint);
                    connected = true;
                }
                catch
                {
                    Console.WriteLine("Failed to set up a connection.");
                    Thread.Sleep(2000);
                }
            }
            clientAssignmentThread = new Thread(AssignClients);
            clientAssignmentThread.Start();
            Console.ReadLine();
        }

        public void AssignClients()
        {
            Console.WriteLine("Waiting for clients to connect...");
            while(true)
            {
                while (!(connectedClients.IsFull()))
                {
                    TcpClient newClient;
                    newClient = listener.AcceptTcpClient();

                    if (newClient != null)
                    {
                        Console.WriteLine("A new client has connected to the server!");
                        connectedClients.Add(new Client(newClient, this));
                    }
                    Thread.Sleep(100);
                }
                Thread.Sleep(100);
            }
        }

        public string GetLocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        public ClientArray getConnectedClients()
        {
            return connectedClients;
        }
    }
}
