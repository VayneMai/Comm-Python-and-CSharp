using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Linq.Expressions;

namespace CommTCPIP_Python
{


	class Program
	{
		private const uint BUFFER_SIZE = 1024;
		private const int PORT_NUMBER = 8000;

		static ASCIIEncoding enconding = new ASCIIEncoding();
		static IPAddress address;
		static TcpListener listener;
		static Socket socket;

        static void Main(string[] args)
		{
			try
			{
				address = IPAddress.Parse("127.0.0.1");
				listener = new TcpListener(address, PORT_NUMBER);
				listener.Start();

				Console.WriteLine("Server start on " + listener.LocalEndpoint);
				Console.WriteLine("Waiting for a connection ...");

				socket = listener.AcceptSocket();
				Console.WriteLine("Connection recevied from " + socket.RemoteEndPoint);

				Console.Write("Data Sent : ");
				string temp = Console.ReadLine();
				socket.Send(enconding.GetBytes(temp));
				while (true)
				{
					byte[] data = new byte[BUFFER_SIZE];
					socket.Receive(data);
					string str = enconding.GetString(data);
					string strReceive = str.Substring(0, 6);
					if (strReceive=="Finish")
					{
						socket.Send(enconding.GetBytes("read"));
					}
				}
			}
			catch (Exception e)
			{
				socket.Close();
				listener.Stop();
				Console.WriteLine("Error : "+e.Message.ToString());
			}
            Console.ReadKey();
        }
	}
}
