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
        public Socket partySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void Сonnection(IPAddress ip, int port) { }

        public string PartyReceive(Socket connect)
        {
            if (connect.Connected)
            {
                var buffer = new byte[256];
                var data = new List<byte>();
                do
                {
                    try
                    {
                        connect.Receive(buffer);
                        data.AddRange(buffer);
                    }
                    catch (Exception exc)
                    {
                        Notify?.Invoke (exc.Message);
                        break;
                    }
                } while (connect.Available > 0);

                var t = data.ToArray();
                var message = Encoding.Unicode.GetString(t, 0, t.Length);
                if (message == "bye" || message == "Bye") PartyClose(connect);
                //Notify?.Invoke("Received");
                return message;
            }
            else
            {
                Notify?.Invoke("No connection");
                return "Отсутствует соединение";
            }
        }

        public void PartySend(Socket connect, string message)
        {
            if (connect.Connected)
            {
                connect.Send(Encoding.Unicode.GetBytes(message));
                //Notify?.Invoke("Send...");
                if (message == "bye" || message == "Bye") PartyClose(connect);
            }
            else Notify?.Invoke("No connection");
        }

        public void PartyClose(Socket connect)
        {
           Notify?.Invoke("Close");
           connect.Shutdown(SocketShutdown.Both);
           connect.Close();
        }
    }
}