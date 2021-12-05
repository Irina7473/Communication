using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketLibrary
{
    public class Server: NetworkParty
    {
       // public static new event Action<string> Notify;        
        public Server(IPAddress ip, int port)
        {
            IP = ip;
            Port = port;
            partySocket.Bind(new IPEndPoint(IP, Port));
            partySocket.Listen(10);
            //Notify?.Invoke("Listen...");
        }        
    }
}