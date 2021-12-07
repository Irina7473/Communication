using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketLibrary
{
    public class Client: NetworkParty
    {
        public static new event Action<string> Notify;
        public Client() { }
        public new void Сonnection(IPAddress ip, int port)
        {           
            partySocket.Connect(new IPEndPoint(ip, port));
            Notify?.Invoke("Connect to Server");
        }       
    }
}