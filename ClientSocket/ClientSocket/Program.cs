using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ClientSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            int BUFFER_SIZE = 2048;
            IPAddress server_ip = IPAddress.Parse("10.0.0.30");
            IPEndPoint ipe = new IPEndPoint(server_ip, 1234);
            Socket cs = new Socket(server_ip.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            cs.Connect(ipe);

            string msg;
            byte[] b = new byte[BUFFER_SIZE];

            //abcdefgh
            //BB
            //BBcdefgh
            x
            Array.Clear(b, 0, b.Length);
            cs.Receive(b);

            msg = Encoding.ASCII.GetString(b).TrimEnd('\0');
            string result;
            while (msg != "quit")
            {
                Console.WriteLine("[+] Received from server: {0}", msg);

                result = msg;

                cs.Send(Encoding.ASCII.GetBytes(result));

                Array.Clear(b, 0, b.Length);
                cs.Receive(b);
                msg = Encoding.ASCII.GetString(b).TrimEnd('\0');
            }
            cs.Close();

            Console.ReadKey();
        }
    }
}
