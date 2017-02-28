namespace ChatClient
{
    partial class MainForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.groupBoxUsers = new System.Windows.Forms.GroupBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.richTextBoxUsers = new System.Windows.Forms.RichTextBox();
            this.textBoxMess = new System.Windows.Forms.TextBox();
            this.groupBoxGroups = new System.Windows.Forms.GroupBox();
            this.listBoxGrps = new System.Windows.Forms.ListBox();
            this.buttonNewGroup = new System.Windows.Forms.Button();
            this.buttunSendPrivate = new System.Windows.Forms.Button();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.richTextBoxPrivate = new System.Windows.Forms.RichTextBox();
            this.groupBoxUsers.SuspendLayout();
            this.groupBoxGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Hello,";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Rockwell", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelLogin.Location = new System.Drawing.Point(53, 9);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(68, 16);
            this.labelLogin.TabIndex = 22;
            this.labelLogin.Text = "Username";
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonSend.Font = new System.Drawing.Font("Rockwell", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(343, 445);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(147, 21);
            this.buttonSend.TabIndex = 20;
            this.buttonSend.Text = "Отправить всем";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // groupBoxUsers
            // 
            this.groupBoxUsers.Controls.Add(this.listBoxUsers);
            this.groupBoxUsers.Font = new System.Drawing.Font("Rockwell", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxUsers.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBoxUsers.Location = new System.Drawing.Point(496, 9);
            this.groupBoxUsers.Name = "groupBoxUsers";
            this.groupBoxUsers.Size = new System.Drawing.Size(154, 284);
            this.groupBoxUsers.TabIndex = 24;
            this.groupBoxUsers.TabStop = false;
            this.groupBoxUsers.Text = "Пользователи";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.ItemHeight = 14;
            this.listBoxUsers.Location = new System.Drawing.Point(9, 19);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(139, 256);
            this.listBoxUsers.TabIndex = 0;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBoxUsers_SelectedIndexChanged);
            this.listBoxUsers.DoubleClick += new System.EventHandler(this.listBoxUsers_DoubleClick);
            // 
            // richTextBoxUsers
            // 
            this.richTextBoxUsers.AcceptsTab = true;
            this.richTextBoxUsers.Location = new System.Drawing.Point(7, 102);
            this.richTextBoxUsers.Name = "richTextBoxUsers";
            this.richTextBoxUsers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxUsers.Size = new System.Drawing.Size(141, 36);
            this.richTextBoxUsers.TabIndex = 33;
            this.richTextBoxUsers.Text = "";
            // 
            // textBoxMess
            // 
            this.textBoxMess.Location = new System.Drawing.Point(12, 445);
            this.textBoxMess.Multiline = true;
            this.textBoxMess.Name = "textBoxMess";
            this.textBoxMess.Size = new System.Drawing.Size(325, 48);
            this.textBoxMess.TabIndex = 27;
            // 
            // groupBoxGroups
            // 
            this.groupBoxGroups.Controls.Add(this.listBoxGrps);
            this.groupBoxGroups.Controls.Add(this.buttonNewGroup);
            this.groupBoxGroups.Font = new System.Drawing.Font("Rockwell", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxGroups.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBoxGroups.Location = new System.Drawing.Point(496, 299);
            this.groupBoxGroups.Name = "groupBoxGroups";
            this.groupBoxGroups.Size = new System.Drawing.Size(154, 194);
            this.groupBoxGroups.TabIndex = 30;
            this.groupBoxGroups.TabStop = false;
            this.groupBoxGroups.Text = "Группы";
            // 
            // listBoxGrps
            // 
            this.listBoxGrps.FormattingEnabled = true;
            this.listBoxGrps.ItemHeight = 14;
            this.listBoxGrps.Location = new System.Drawing.Point(7, 22);
            this.listBoxGrps.Name = "listBoxGrps";
            this.listBoxGrps.Size = new System.Drawing.Size(141, 144);
            this.listBoxGrps.TabIndex = 6;
            this.listBoxGrps.SelectedIndexChanged += new System.EventHandler(this.listBoxGrps_SelectedIndexChanged);
            this.listBoxGrps.DoubleClick += new System.EventHandler(this.listBoxGrps_DoubleClick);
            // 
            // buttonNewGroup
            // 
            this.buttonNewGroup.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonNewGroup.Font = new System.Drawing.Font("Rockwell", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonNewGroup.Location = new System.Drawing.Point(9, 173);
            this.buttonNewGroup.Name = "buttonNewGroup";
            this.buttonNewGroup.Size = new System.Drawing.Size(136, 21);
            this.buttonNewGroup.TabIndex = 5;
            this.buttonNewGroup.Text = "Новая группа";
            this.buttonNewGroup.UseVisualStyleBackColor = false;
            this.buttonNewGroup.Click += new System.EventHandler(this.buttonNewGroup_Click);
            // 
            // buttunSendPrivate
            // 
            this.buttunSendPrivate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttunSendPrivate.Font = new System.Drawing.Font("Rockwell", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttunSendPrivate.Location = new System.Drawing.Point(343, 472);
            this.buttunSendPrivate.Name = "buttunSendPrivate";
            this.buttunSendPrivate.Size = new System.Drawing.Size(149, 21);
            this.buttunSendPrivate.TabIndex = 31;
            this.buttunSendPrivate.Text = "Приватное сообщение";
            this.buttunSendPrivate.UseVisualStyleBackColor = false;
            this.buttunSendPrivate.Click += new System.EventHandler(this.buttunSendPrivate_Click);
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.AcceptsTab = true;
            this.richTextBoxChat.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxChat.Location = new System.Drawing.Point(12, 28);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(478, 248);
            this.richTextBoxChat.TabIndex = 32;
            this.richTextBoxChat.Text = "";
            // 
            // richTextBoxPrivate
            // 
            this.richTextBoxPrivate.AcceptsTab = true;
            this.richTextBoxPrivate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxPrivate.Location = new System.Drawing.Point(12, 282);
            this.richTextBoxPrivate.MaximumSize = new System.Drawing.Size(800, 200);
            this.richTextBoxPrivate.MinimumSize = new System.Drawing.Size(120, 60);
            this.richTextBoxPrivate.Name = "richTextBoxPrivate";
            this.richTextBoxPrivate.ReadOnly = true;
            this.richTextBoxPrivate.Size = new System.Drawing.Size(478, 157);
            this.richTextBoxPrivate.TabIndex = 33;
            this.richTextBoxPrivate.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 499);
            this.Controls.Add(this.richTextBoxPrivate);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.buttunSendPrivate);
            this.Controls.Add(this.groupBoxGroups);
            this.Controls.Add(this.textBoxMess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.groupBoxUsers);
            this.Name = "MainForm";
            this.Text = "Chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.groupBoxUsers.ResumeLayout(false);
            this.groupBoxGroups.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.GroupBox groupBoxUsers;
        private System.Windows.Forms.TextBox textBoxMess;
        private System.Windows.Forms.GroupBox groupBoxGroups;
        private System.Windows.Forms.Button buttonNewGroup;
        private System.Windows.Forms.ListBox listBoxGrps;
        private System.Windows.Forms.Button buttunSendPrivate;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.RichTextBox richTextBoxUsers;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.RichTextBox richTextBoxPrivate;
    }
}