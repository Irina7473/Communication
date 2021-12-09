using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using SocketLibrary;
namespace ServerConsApp
{
    static class Program
    {
        public static event Action<string> Notify;        
        static void Main()
        {
            NetworkParty.Notify += Notify;
            Notify += Output;
            string message;
            Notify?.Invoke("Start...");
            var ip = "127.0.0.1";
            var port = 8005;
            var server = new Server();
            server.Mode = true;
            server.Сonnection(IPAddress.Parse(ip), port);
            Notify?.Invoke("Listen...");

            while (true)
            {
                var connect = server.partySocket.Accept();
                Notify?.Invoke("Accept...");
                server.PartySend(connect, "Соединение с сервером установлено");
                do
                {
                    message = server.PartyReceive(connect);
                    Output(message);
                    if (message == "bye" || message == "Bye")
                    {
                        server.PartyClose(connect);
                        return;
                    }
                    if (server.Mode == true)
                    {
                        message = Console.ReadLine();
                        server.PartySend(connect, message);
                    }
                    else
                    {
                        server.PartySend(connect, $"Принято сообщение: {message}");
                    }
                    Notify?.Invoke("Send...");
                }
                while (connect.Connected);
            }
        }

        static void Output (string message)
        {
            Console.WriteLine(message);
        }
    }
}