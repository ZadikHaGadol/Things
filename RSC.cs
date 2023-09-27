using System;
using System.IO;
using System.Net.Sockets;

namespace ReverseShellClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("localhost", 1234);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            while (true)
            {
                Console.Write(">> ");
                string command = Console.ReadLine();
                writer.WriteLine(command);

                if (command == "exit")
                    break;

                string output = reader.ReadLine();
                Console.WriteLine(output);
            }

            client.Close();
        }
    }
}
