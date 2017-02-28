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

namespace ChatClient
{
    public partial class GroupForm : Form
    {
        ServiceSendMessageClient serviceMess;
        string grpname;
        bool isNew;
        public GroupForm(ServiceSendMessageClient client, string login, string grpname)
        { 
            InitializeComponent();
            labelValidation.Text = "";
            serviceMess = client;
            this.grpname = grpname;
            textBoxName.Text = grpname;
            if(grpname == null)
            {
                isNew = true;
                listBoxUsers.Items.Add(login);
            }
            else
            {
                isNew = false;
                textBoxName.Enabled = false;
                string[] users = serviceMess.GetUsersFromGroup(grpname);
                for (int i = 0; i < users.Count(); i++)
                    listBoxUsers.Items.Add(users[i]);
            }

            string[] allUsers = serviceMess.GetAllUsers();
            for (int i = 0; i < allUsers.Count(); i++)
                if(!listBoxUsers.Items.Contains(allUsers[i]))
                    listBoxAllUsers.Items.Add(allUsers[i]);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            for(int i=0; listBoxAllUsers.SelectedItems.Count > 0; )
            {
                string user = listBoxAllUsers.SelectedItems[i].ToString();
                listBoxUsers.Items.Add(user);
                listBoxAllUsers.Items.Remove(user);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; listBoxUsers.SelectedItems.Count > 0; )
            {
                string user = listBoxUsers.SelectedItems[i].ToString();
                listBoxUsers.Items.Remove(user);
                listBoxAllUsers.Items.Add(user);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "") // || listBoxUsers.Items.Count < 1)
            {
                labelValidation.Text = Validation.FillAllFields;
                return;
            }
            string[] users = new string[listBoxUsers.Items.Count];
            int i = 0;
            foreach (var item in listBoxUsers.Items)
                users[i++] = item.ToString();

            GroupValidation response;
            if (isNew)
                response = serviceMess.CreateGroup(textBoxName.Text, users);
            else
                response = serviceMess.EditGroup(grpname, textBoxName.Text, users);

            if (response == GroupValidation.GroupNameIsExists)
                labelValidation.Text = Validation.dictionaryGroupValidation[response];
            else if (response == GroupValidation.GroupCreateSuccess || response == GroupValidation.GroupEditSuccess)
                this.Close();
        }
    }
}
