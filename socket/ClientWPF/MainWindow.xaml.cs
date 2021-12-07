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
        public MainWindow()
        {
            InitializeComponent();
            Button_send.IsEnabled = false;
            client = new Client();
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
            client.Сonnection(ip, port);
            Button_connect.IsEnabled = false;
            Button_send.IsEnabled = true;
            var message = client.PartyReceive(client.partySocket);
            AppendFormattedText("server", message);
        }

        private void Button_send_Click(object sender, RoutedEventArgs e)
        {
            if (client.partySocket.Connected)
            {
                client.PartySend(client.partySocket, TextBox_outgoing.Text);
                AppendFormattedText("client", TextBox_outgoing.Text);
                TextBox_outgoing.Text = "";
                var message = client.PartyReceive(client.partySocket);
                AppendFormattedText("server", message);
            }
            else
            {
                MessageBox.Show("Соединение с сервером не установлено");
                Button_connect.IsEnabled = true;
            }
        }

        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_outgoing.Text = "";
        }

        private void Button_clearChat_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox_incoming.Document.Blocks.Clear();
        }

        private void human_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void computer_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AppendFormattedText(string type, string text)
        {
            TextRange rangeOfWord = new TextRange(RichTextBox_incoming.Document.ContentEnd, RichTextBox_incoming.Document.ContentEnd);
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
            if (type == "server")
            {
                rangeOfWord.Text = text + "\r";
                rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
            }
            if (type == "client")
            {
                rangeOfWord.Text = "\t" + text + "\r";
                rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green); 
            }
        }
    }
}
