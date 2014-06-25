using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultiClientGame
{
    public partial class GameForm : Form
    {
        const int REFRESH_RATE = 1000/60;
        int diameter = 350;
        Point center;
        Ship player;
        List<Ship> ships = new List<Ship>();
        internal BulletList bullets = new BulletList(); 
        string color;
        bool connected = false;
        bool chatEnabled = false;

        bool LEFT_ISDOWN = false;
        bool RIGHT_ISDOWN = false;
        bool SPACE_ISDOWN = false;
        bool BULLET_ALLOWED = true;

        TcpClient client;
        Thread communicateWithServerThread;
        internal StreamWriter writer;
        StreamReader reader;
        delegate void ChatBoxMessageDelegate(string s);
        delegate void UpdateStatusStripDelegate(string s);

        public GameForm()
        {
            InitializeComponent();
            disconnectButton.Enabled = false;

            center = new Point(GamePanel.Width / 2, GamePanel.Height / 2);
            KeyPreview = true;
            KeyUp += new KeyEventHandler(OnKeyUp);
            KeyDown += new KeyEventHandler(OnKeyDown);
            
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = REFRESH_RATE;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (player != null)
            {
                try
                {
                    checkKeys();
                    writer.WriteLine("posi#" + color + "#" + player.getAngle());
                }
                catch (IOException)
                {
                    UpdateStatusStrip("A server error occured. Please try to reconnect.");
                    disconnect();
                }
            }
            foreach (Bullet b in bullets.getList())
            {
                b.update();
            }
                //foreach (Bullet b in kogels)
                //{
                //    Point a = b.getloction();
                //    if (a.X > 0 && a.X < 1000 && a.Y > 0 && a.Y < 1000)
                //        b.update();
                //    else
                //        bullets.Add(b);
                //}
            
            //foreach (Bullet b in bullets)
            //{
            //    kogels.TryTake(out b,2);
            //}
            
            GamePanel.Invalidate();
            GamePanel.Update();
            if (player!=null)
            player.checkhit();
        }

        public void openConnectForm()
        {
            new ConnectForm(this).Show();
        }

        public void connect(IPAddress ip, int port)
        {
            client = new TcpClient();
            UpdateStatusStrip("Attempting to connect to the server...");
            try
            {
                client.Connect(ip, port);
            }
            catch
            {
                UpdateStatusStrip("Failed to connect to the server!");
                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
                return;
            }
            UpdateStatusStrip("Succesfully connected to the server!");
            disconnectButton.Enabled = true;
            communicateWithServerThread = new Thread(CommunicateWithServer);
            communicateWithServerThread.Start(client);
        }

        public void disconnect()
        {
            connected = false;
            try { writer.WriteLine("quit#" + color); } catch (IOException) { }
            communicateWithServerThread.Abort();
            client.Close();
            player = null;
            ships.Clear();
            PlayerListBox.Items.Clear();
            connectButton.Enabled = true;
            disconnectButton.Enabled = false;
            UpdateStatusStrip("Succesfully disconnected to the server!");
        }

        public void CommunicateWithServer(Object o)
        {
            TcpClient c = (TcpClient)o;
            writer = new StreamWriter(c.GetStream());
            reader = new StreamReader(c.GetStream());
            writer.AutoFlush = true;
            connected = true;

            UpdateStatusStrip("Waiting for the server to send your username...");
            while (connected)
            {
                try
                {
                    string message = reader.ReadLine();
                    string[] serverData = message.Split('#');
                    switch (serverData[0])
                    {
                        case "name":
                            color = serverData[1];
                            UpdateStatusStrip("Welcome " + color + "! You can now play and start messaging!");
                            chatBoxMessage("Your color is " + color);
                            player = new Ship(color, center, diameter / 2, this);
                            ships.Add(player);
                            PlayerListBox.Invoke(new MethodInvoker(delegate
                            {
                                PlayerListBox.Items.Add(color + ":0");
                            }));
                            chatEnabled = true;
                            break;
                        case "mess":
                            chatBoxMessage(serverData[1] + ": " + serverData[2]);
                            break;
                        case "posi":
                            if (serverData[1] == color)
                                continue;
                            foreach (Ship s in ships)
                            {
                                if (s.ColorStr == serverData[1])
                                {
                                    double a = s.Angle;
                                    try
                                    {
                                        s.Angle = double.Parse(serverData[2]);
                                    }
                                    catch (Exception)
                                    {
                                        s.Angle = a;
                                    }
                                }
                            }
                            break;
                        case "shoot":
                            bullets.Add(new Bullet(player.getPlayerColor(serverData[1]), Convert.ToInt32(serverData[2]), Convert.ToInt32(serverData[3]), center));
                            break;
                        case "newp":
                            ships.Add(new Ship(serverData[1], center, diameter / 2, this));
                            PlayerListBox.Invoke(new MethodInvoker(delegate
                            {
                                PlayerListBox.Items.Add(serverData[1] + ":0");
                            }));
                            break;
                        case "score":
                            int i = 0;
                            string[] line = null;
                            foreach (string s in PlayerListBox.Items)
                            {
                                line = s.Split(':');
                                if (line[0] == serverData[1])
                                {
                                    line[1] = serverData[2];
                                    break;
                                }
                                i++;
                            }
                            PlayerListBox.Invoke(new MethodInvoker(delegate
                            {
                                PlayerListBox.Items[i] = line[0] + ":" + line[1];
                            }));
                            break;
                        case "quit":
                            foreach (Ship s in ships)
                            {
                                if (s.ColorStr == serverData[1])
                                {
                                    s.setActivity(false);
                                }
                            }
                            string[] pList = serverData[2].Split('$');
                            PlayerListBox.Invoke(new MethodInvoker(delegate
                            {
                                PlayerListBox.Items.Clear();
                            }));
                            foreach (string s in pList)
                            {
                                PlayerListBox.Invoke(new MethodInvoker(delegate
                                {
                                    PlayerListBox.Items.Add(s);
                                }));
                            }
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine(color + ":" + " default case in CommunicateWithServer()");
                            break;
                    }
                }
                catch (IOException)
                {
                    UpdateStatusStrip("A server error occured. Please try to reconnect.");
                    disconnect();
                }
            }
        }

        public void chatBoxMessage(string s)
        {
            if (InvokeRequired)
            {
                this.Invoke(new ChatBoxMessageDelegate(chatBoxMessage), new object[] {s});
                return;
            }
            try
            {
                ChatBox.AppendText(s + Environment.NewLine);
            }
            catch (Exception)
            {}
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pn = new Pen(Color.Black);
            g.DrawEllipse(pn, center.X - diameter / 2, center.Y - diameter / 2, diameter, diameter);


            if (!(ships.Count <= 0))
            {
                foreach (Ship s in ships)
                {
                    if (s.isActive)
                    {
                        s.draw(e.Graphics);
                    }
                }

                if (bullets.getList().Count != 0)
                    foreach (Bullet b in bullets.getList())
                        b.draw(e.Graphics);
            }
        }

        private void InputChatBox_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //send chat message
                if (chatEnabled)
                    writer.WriteLine("mess#" + color + "#" + InputChatBox.Text);
                else
                    UpdateStatusStrip("Connect to the server to send messages!");
                InputChatBox.Clear();
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            openConnectForm();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            disconnectButton.Enabled = false;
            disconnect();
        }

        public void EnableConnectButton(bool b)
        {
            connectButton.Enabled = b;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (connected)
                disconnect();
            Environment.Exit(0);
        }

        private void checkKeys()
        {
            if (LEFT_ISDOWN)
                player.Move('L');
            else if (RIGHT_ISDOWN)
                player.Move('R');
            else
                player.speedToZero();

            if (SPACE_ISDOWN && BULLET_ALLOWED)
            {
                player.Shoot();
                SPACE_ISDOWN = false;
                BULLET_ALLOWED = false;
        }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (player != null)
            {
                if (e.KeyData == Keys.Left)
                    LEFT_ISDOWN = false;
                else if (e.KeyData == Keys.Right)
                    RIGHT_ISDOWN = false;
                else if (e.KeyData == Keys.Space)
                {
                    SPACE_ISDOWN = false;
                    BULLET_ALLOWED = true;
            }
        }
        }

        private void OnKeyDown(object sender, KeyEventArgs e) 
        {
            if (player != null)
            {
                if (e.KeyData == Keys.Left)
                    LEFT_ISDOWN = true;
                else if (e.KeyData == Keys.Right)
                    RIGHT_ISDOWN = true;
                else if (e.KeyData == Keys.Space)
                    SPACE_ISDOWN = true;                       
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            UpdateStatusStrip("This game was created by Lesley van Hoek and Jens Uuldriks for Avans Hogeschool Breda.");
    }

        private void UpdateStatusStrip(string s)
        {
            if (statusStrip.InvokeRequired)
                Invoke(new UpdateStatusStripDelegate(UpdateStatusStrip), new object[] { s });
            else
                statusLabel.Text = s;
            statusStrip.Invalidate();
        }

        private void SaveChatlog_Click(object sender, EventArgs e)
        {
            try
            {          
                string date = string.Format("{0:yyyy-MM-dd hh-mm-ss-tt}", DateTime.Now);
                string filename = @"Logs/chatlog(" + date + ").txt";
                FileStream fs = File.Create(filename);
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine(ChatBox.Text);
                writer.Flush();
                UpdateStatusStrip("Saved file: " + filename);
            }
            catch (Exception)
            {
                UpdateStatusStrip("Failed to save file!");
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (connected)
                disconnect();
        }
    }
}
