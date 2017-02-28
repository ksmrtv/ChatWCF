using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatClient.ServiceAuthenticationReference;

namespace ChatClient
{
    public partial class LoginForm : Form
    {
        ServiceAuthenticationClient sc;
        public LoginForm()
        {
            InitializeComponent();
            labelValidation.Text = "";
            textBoxPass.PasswordChar = '*';
            sc = new ServiceAuthenticationClient();
        }

        private void buttonRegistry_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "" || textBoxPass.Text == "")
                labelValidation.Text = Validation.FillAllFields;

            UserValidation response = sc.RegisterUser(textBoxLogin.Text, textBoxPass.Text);
            this.labelValidation.Text = Validation.dictionaryUserValidation[response];
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            UserValidation response = sc.Enter(textBoxLogin.Text, textBoxPass.Text);
            if (!response.Equals(UserValidation.EnterSuccess))
                labelValidation.Text = Validation.dictionaryUserValidation[response];

            else
            {
                MainForm mainForm = new MainForm(this, textBoxLogin.Text);
                mainForm.Show();
                this.Hide();
                textBoxPass.Text = "";
            }
        }
    }
}
