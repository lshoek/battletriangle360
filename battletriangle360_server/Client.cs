using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiClientGameServer
{
    class Client
    {
        string color;
        int score = 0;
        StreamReader reader;
        StreamWriter writer;
        Server s;

        public Client(TcpClient c, Server s)
        {
            reader = new StreamReader(c.GetStream());
            writer = new StreamWriter(c.GetStream());
            writer.AutoFlush = true;
            this.s = s;

            Thread ReceiveClientInputThread = new Thread(ReceiveClientInput);
            ReceiveClientInputThread.Start();
        }

        public void ReceiveClientInput()
        {
            Console.Write("Assigning a color to the new client...");
            while (color == null)
            {
                Console.Write(".");
                Thread.Sleep(10);
            }
            Console.WriteLine(Environment.NewLine + "Done! Now sending the username (" + color + ") to the client...");
            writer.WriteLine("name#" + color);
            Console.WriteLine("Done!");
            Console.WriteLine("Started receiving input from " + color);
            foreach (Client client in s.getConnectedClients().getConnectedOnly())
            {
                if (client.color != color)
                {
                    writer.WriteLine("newp#" + client.color);
                    client.writer.WriteLine("newp#" + color);
                }
            }

            while (true)
            {
                string[] input;
                try
                {
                    input = reader.ReadLine().Split('#');
                }
                catch
                {
                    break;
                    //doe chit om alles af te sluiten!!
                }
                switch (input[0])
                {
                    case "mess": //message
                        Console.WriteLine(input[1] + ": " + input[2]);
                        foreach (Client c in s.getConnectedClients().getConnectedOnly())
                        {
                            c.writer.WriteLine("mess#" + input[1] + "#" + input[2]);
                        }
                        break;
                    case "hit":
                        Console.WriteLine(input[1] + " --> " + input[2] + "      " + color);
                        if (input[2] == color)
                        {
                            System.Diagnostics.Debug.WriteLine("AAAAAAa");
                            score++;
                            foreach (Client c in s.getConnectedClients().getConnectedOnly())
                                c.writer.WriteLine("score#" + color + "#" + score);
                        }
                        break;
                    case "posi": //data
                        if (input[1] != color)
                            continue;
                        SendClientPosition(color, input[2]);
                        break;
                    case "shoot":
                        foreach (Client c in s.getConnectedClients().getConnectedOnly())
                            if (c.color != color)
                                try
                                {
                                    c.writer.WriteLine("shoot#" + input[1] + "#" + input[2] + "#" + input[3]);
                                }
                                catch { }
                        break;
                    case "quit": //quit
                        Console.WriteLine(color + " disconnected from the server.");
                        s.getConnectedClients().Remove(color);
                        try
                        {
                            StringBuilder playerList = new StringBuilder();
                            foreach (Client c in s.getConnectedClients().getConnectedOnly())                           
                                playerList.Append(c.color + ":" + c.score + "$");                            
                            foreach (Client c in s.getConnectedClients().getConnectedOnly())                           
                                c.writer.WriteLine("quit#" + color + "#" + playerList);                            
                        }
                        catch (Exception) { }
                        break;
                    default:
                        break;
                }
            }
        }

        public void SendClientPosition(string color, string angle)
        {
            try
            {
                foreach (Client client in s.getConnectedClients().getConnectedOnly())
                {
                    if (client.color != color)
                        client.writer.WriteLine("posi#" + color + "#" + angle);
                }
            }
            catch { }
        }

        public void setColor(string colr)
        {
            this.color = colr;
        }

        public string getUserName()
        {
            return color;
        }


    }
}
