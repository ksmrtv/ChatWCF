using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatClient.ServiceMessageReference;
using System.ServiceModel;
using System.Threading;
using System.IO;

namespace ChatClient
{
    public partial class MainForm : Form
    {
        Form parentForm;
        int id;
        ServiceSendMessageClient serviceMess;
        CallBackClass callback;

        public MainForm(Form form, string login)
        {
            InitializeComponent();
            labelLogin.Text = login;
            parentForm = form;

            callback = new CallBackClass();
            InstanceContext context = new InstanceContext(callback);
            serviceMess = new ServiceSendMessageClient(context);

            id = serviceMess.Subscribe(login);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            serviceMess.Unsubscribe(id);
            serviceMess.Close();
            parentForm.Close();
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lstUsers = sender as ListBox;
            string loginTo = lstUsers.SelectedItem.ToString();
            textBoxMess.Text += loginTo + ", ";
            textBoxMess.Focus();
            textBoxMess.Select(textBoxMess.Text.Length+1, 1);
        }

        private void listBoxUsers_DoubleClick(object sender, EventArgs e)
        {
            ListBox lstUsers = sender as ListBox;
            string loginTo = lstUsers.SelectedItem.ToString();
            textBoxMess.Text = loginTo + ", ";
            textBoxMess.Focus();
            textBoxMess.Select(loginTo.Length + 2, 1);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            serviceMess.SendPublicMessage(id, textBoxMess.Text);
            textBoxMess.Text = "";
        }

        private void buttunSendPrivate_Click(object sender, EventArgs e)
        {
            if (!textBoxMess.Text.Contains(','))
            {
                MessageBox.Show("Не выбран получатель :(");
                return;
            }
            string to = textBoxMess.Text.Substring(0, textBoxMess.Text.IndexOf(','));
            string text = textBoxMess.Text.Substring(textBoxMess.Text.IndexOf(',') + 2);
            bool isSended = serviceMess.SendPrivateMessage(id, to, text);
            if (isSended)
                textBoxMess.Text = to + ", ";
            else
                MessageBox.Show("Получатель указан некорректно", "Error");
        }

        private void buttonNewGroup_Click(object sender, EventArgs e)
        {
            GroupForm groupForm = new GroupForm(serviceMess, labelLogin.Text, null);
            groupForm.Show();
        }


        private void listBoxGrps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox grps = sender as ListBox;
            string grpTo = grps.SelectedItem.ToString();
            textBoxMess.Text = grpTo + ", ";
            textBoxMess.Focus();
            textBoxMess.Select(grpTo.Length + 2, 1);
        }

        private void listBoxGrps_DoubleClick(object sender, EventArgs e)
        {
            string grpname = (sender as ListBox).SelectedItem.ToString();
            GroupForm groupForm = new GroupForm(serviceMess, labelLogin.Text, grpname);
            groupForm.Show();
        }

    }
}
