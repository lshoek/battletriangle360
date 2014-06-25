using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MultiClientGame
{
    public partial class ConnectForm : Form
    {
        GameForm _form;
        IPAddress ip;
        int port;

        public ConnectForm(GameForm f)
        {
            InitializeComponent();
            _form = f;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ipLabel != null && portLabel != null)
            {
                try
                {
                    ip = IPAddress.Parse(ipLabel.Text);
                    port = int.Parse(portLabel.Text);
                    _form.connect(ip, port);
                    Dispose();
                }
                catch (Exception)
                { }
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            _form.EnableConnectButton(true);
        }
    }
}