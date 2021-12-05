using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SocketLibrary;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        Client client;
        IPAddress ip;
        int port;
        Socket connect;
        public MainWindow()
        {
            InitializeComponent();
            Button_send.IsEnabled = false;
        }

        private void Button_connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ip = IPAddress.Parse(TextBox_IPaddress.Text);
            }
            catch
            { MessageBox.Show("Неверный формат IP адреса"); }
            try
            {
                port = Int32.Parse(TextBox_portNumber.Text);
            }
            catch
            { MessageBox.Show("Неверный формат порта"); }
            client = new Client(ip, port);
            Button_send.IsEnabled = true;
            while (true)
            {
                connect = client.chatSocket.Accept();
                TextBox_incoming.Text = client.Receive(connect);
            }
        }

        private void Button_send_Click(object sender, RoutedEventArgs e)
        {
            client.Send(connect, TextBox_outgoing.Text);
        }
        private void human_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void computer_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
