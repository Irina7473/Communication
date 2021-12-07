using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketLibrary
{
    public class Server : NetworkParty
    {
        public bool Mode { get; set;}
        public Server() { }

        public new void Сonnection(IPAddress ip, int port)
        {            
            partySocket.Bind(new IPEndPoint(ip, port));
            partySocket.Listen(10);
        }        
    }
}