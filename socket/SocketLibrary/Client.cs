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
        
        public Client(IPAddress ip, int port)
        {
            IP = ip;
            Port = port;
            partySocket.Connect(new IPEndPoint(IP, Port));
            Notify?.Invoke("Connect to Server");
        }       
    }
}