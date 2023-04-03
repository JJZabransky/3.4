using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _3._4
{
    public class Server
    {
        private TcpListener myServer;
        private bool isRunning;
        public string[] commands = { "exit", "help", "date", "ipconfig"};

        public Server(int port)
        {
            myServer = new TcpListener(System.Net.IPAddress.Any, port);
            myServer.Start();
            isRunning = true;
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server byl spusten");
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                ClientLoop(client);
            }
        }

        private void ClientLoop(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);

            writer.WriteLine("Byl jsi pripojen");
            writer.Flush();
            bool clientConnect = true;
            string? data = null;
            string? dataRecive = null;
            while (clientConnect)
            {
                data = reader.ReadLine();
                data = data.ToLower();
                if (data == "exit")
                {
                    clientConnect = false;
                }

                if(data == "date")
                {
                    writer.WriteLine(DateTime.Now.ToString());
                }
                
                if(data == "ipconfig")
                {
                    writer.WriteLine(System.Net.IPAddress.Any);
                }
                
                if(data == "help")
                {
                    writer.WriteLine("-------------------------");
                    foreach (string s in commands)
                    {
                        writer.WriteLine(s);
                    }
                    writer.WriteLine("-------------------------");
                }

                dataRecive = data + " prijato";
                writer.WriteLine(dataRecive);
                writer.Flush();
            }
            writer.WriteLine("Byl jsi odpojen");
            writer.Flush();
        }
    }
}
