using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketLibrary
{
    public class NetworkParty
    {
        public static event Action<string> Notify;
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public Socket partySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        public string Receive(Socket connect)
        {
            var buffer = new byte[256];
            var data = new List<byte>();
            do
            {
                connect.Receive(buffer);
                data.AddRange(buffer);
            } while (connect.Available > 0);

            var t = data.ToArray();
            var message = Encoding.Unicode.GetString(t, 0, t.Length);
            Close(connect, message);
            Notify?.Invoke("Received");
            return message;
        }

        public void Send(Socket connect, string message)
        {
            connect.Send(Encoding.Unicode.GetBytes(message));
            Notify?.Invoke("Send...");
            Close(connect, message);
        }

        private void Close(Socket connect, string message)
        {
            if (message == "bye" || message == "Bye")
            {
                Notify?.Invoke("Close");
                connect.Shutdown(SocketShutdown.Both);
                connect.Close();
            }
        }
    }
}