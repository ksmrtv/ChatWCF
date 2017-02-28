using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.ServiceMessageReference;
using System.ServiceModel;
using System.Windows.Forms;

namespace ChatClient
{
    public class CallBackClass : IServiceSendMessageCallback
    {
        public void ReceiveUsers(string[] users)
        {
            Control cntr = GetControl("MainForm", "listBoxUsers", "groupBoxUsers");

            if (cntr == null)
                return;
            (cntr as ListBox).Items.Clear();
            for (int i = 0; i < users.Count(); i++)
                (cntr as ListBox).Items.Add(users[i]);
        }

        public void ReceivePublicMessage(string from, string mess, DateTime date)
        {
            string time = date.Hour.ToString("00") + ":" + date.Minute.ToString("00");
            if (date.DayOfYear != DateTime.Now.DayOfYear)
                time = date.Day.ToString("00") + "." + date.Month.ToString("00") + "." + date.Year + " " +time;

            string message = time + " " + from + ": " + mess + "\n";
            Control cntr = GetControl("MainForm", "richTextBoxChat", null);

            (cntr as RichTextBox).AppendText(message);
            (cntr as RichTextBox).ScrollToCaret();
        }

        public bool ReceivePrivateMessage(string from, string to, string mess, DateTime date)
        {
            string time = date.Hour.ToString("00") + ":" + date.Minute.ToString("00");
            if (date.DayOfYear != DateTime.Now.DayOfYear)
                time = date.Day.ToString("00") + "." + date.Month.ToString("00") + "." + date.Year + " " + time;

            string message = time + " " + from + ": " + to + ", " + mess + "\n";
            Control cntr = GetControl("MainForm", "richTextBoxPrivate", null);

            (cntr as RichTextBox).AppendText(message);
            (cntr as RichTextBox).ScrollToCaret();
            return true;
        }

        public void ReceiveGroups(string[] groups)
        {
            Control cntr = GetControl("MainForm", "listBoxGrps", "groupBoxGroups");

            if (cntr == null)
                return;
            (cntr as ListBox).Items.Clear();
            for (int i = 0; i < groups.Count(); i++)
                (cntr as ListBox).Items.Add(groups[i]);
        }

        private Control GetControl(string formName, string controlName, string parentControlName)
        {
            FormCollection fc = Application.OpenForms;
            Control form = null;
            Control parentControl = null;

            for (int i = 0; i < fc.Count; i++)
                if (fc[i].Name == formName)
                    form = fc[i];

            if(parentControlName == null)
            {
                for (int i = 0; i < form.Controls.Count; i++)
                    if (form.Controls[i].Name == controlName)
                        return form.Controls[i];
            }

            else
                for (int k = 0; k < form.Controls.Count; k++)
                    if (form.Controls[k].Name == parentControlName)
                    {
                        parentControl = form.Controls[k];
                        for (int j = 0; j < parentControl.Controls.Count; j++)
                            if (parentControl.Controls[j].Name == controlName)
                                return parentControl.Controls[j];
                    }

            return null;
        }
    }
}
