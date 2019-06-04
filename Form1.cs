using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 22000);
            TcpClient client = new TcpClient();
            //Console.WriteLine("Нажмите кнопку чтобы подключиться");
            //Console.ReadKey();
            client.Connect(endPoint);
            BinaryFormatter formatter = new BinaryFormatter();

            while (true)
            {
                string text = Console.ReadLine();
                DataLayer.Message message = new DataLayer.Message
                {
                    Text = text,
                    Date = DateTime.Now
                };
                NetworkStream networkStream = client.GetStream();
                formatter.Serialize(networkStream, message);
                if (text == "exit")
                    break;
            }
            client.Close();
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            lbChat.Items.Add(tbMessage.Text);
            tbMessage.Clear();
        }
        
        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                lbChat.Items.Add(tbMessage.Text);
                tbMessage.Clear();
            }
        }
    }
}
