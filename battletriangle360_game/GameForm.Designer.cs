namespace MultiClientGame
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PlayerListBox = new System.Windows.Forms.ListBox();
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.InputChatBox = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.GamePanel = new MultiClientGame.MyPanel();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveChatlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayerListBox
            // 
            this.PlayerListBox.FormattingEnabled = true;
            this.PlayerListBox.Location = new System.Drawing.Point(730, 40);
            this.PlayerListBox.Margin = new System.Windows.Forms.Padding(5);
            this.PlayerListBox.Name = "PlayerListBox";
            this.PlayerListBox.ScrollAlwaysVisible = true;
            this.PlayerListBox.Size = new System.Drawing.Size(200, 173);
            this.PlayerListBox.TabIndex = 0;
            // 
            // ChatBox
            // 
            this.ChatBox.BackColor = System.Drawing.SystemColors.Window;
            this.ChatBox.Location = new System.Drawing.Point(730, 223);
            this.ChatBox.Margin = new System.Windows.Forms.Padding(5);
            this.ChatBox.Multiline = true;
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatBox.ShortcutsEnabled = false;
            this.ChatBox.Size = new System.Drawing.Size(200, 211);
            this.ChatBox.TabIndex = 1;
            // 
            // InputChatBox
            // 
            this.InputChatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputChatBox.Location = new System.Drawing.Point(730, 444);
            this.InputChatBox.Margin = new System.Windows.Forms.Padding(5);
            this.InputChatBox.Name = "InputChatBox";
            this.InputChatBox.Size = new System.Drawing.Size(200, 20);
            this.InputChatBox.TabIndex = 2;
            this.InputChatBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputChatBox_Enter);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 480);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(944, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(192, 17);
            this.statusLabel.Text = "Welcome to BattleTriangle360 v1.0!";
            // 
            // GamePanel
            // 
            this.GamePanel.BackColor = System.Drawing.Color.White;
            this.GamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GamePanel.Location = new System.Drawing.Point(12, 40);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(710, 424);
            this.GamePanel.TabIndex = 5;
            this.GamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePanel_Paint);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectButton,
            this.disconnectButton,
            this.saveChatlogToolStripMenuItem,
            this.aboutButton,
            this.exitButton});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.connectToolStripMenuItem.Text = "Start";
            // 
            // connectButton
            // 
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(143, 22);
            this.connectButton.Text = "Connect";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(143, 22);
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // saveChatlogToolStripMenuItem
            // 
            this.saveChatlogToolStripMenuItem.Name = "saveChatlogToolStripMenuItem";
            this.saveChatlogToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveChatlogToolStripMenuItem.Text = "Save Chatlog";
            this.saveChatlogToolStripMenuItem.Click += new System.EventHandler(this.SaveChatlog_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(143, 22);
            this.aboutButton.Text = "About";
            this.aboutButton.Click += new System.EventHandler(this.About_Click);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(143, 22);
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 502);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.InputChatBox);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.PlayerListBox);
            this.Controls.Add(this.GamePanel);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(960, 540);
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "GameForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "BattleTriangle360 v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox PlayerListBox;
        private System.Windows.Forms.TextBox ChatBox;
        private System.Windows.Forms.TextBox InputChatBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private MyPanel GamePanel;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectButton;
        private System.Windows.Forms.ToolStripMenuItem disconnectButton;
        private System.Windows.Forms.ToolStripMenuItem aboutButton;
        private System.Windows.Forms.ToolStripMenuItem exitButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveChatlogToolStripMenuItem;
    }
}

