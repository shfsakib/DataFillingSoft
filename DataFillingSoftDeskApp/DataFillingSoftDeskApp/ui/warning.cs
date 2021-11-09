using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.Class;
using Newtonsoft.Json;

namespace DataFillingSoftDeskApp.ui
{
    public partial class warning : Form
    {
        private Point mouse_offset;
        private Function function;
        public warning()
        {
            InitializeComponent();
            function = Function.GetInstance();
        }

        private void warning_Load(object sender, EventArgs e)
        {
            lblAsk.Text = "This Will Delete All Form Entered By You As Well As Registration Details";
        }

        private void warning_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void warning_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            log_in logIn = new log_in();
            logIn.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!function.IsConnected())
            {
                function.MessageBox("Please connect the internet", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            else if (Properties.Settings.Default.AuthKey.ToString() == "")
            {
                DialogResult dialogResult = MessageBox.Show("You are not registered yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    this.Hide();
                    log_in logIn = new log_in();
                    logIn.Show();
                }
                return;
            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();
                    // It can be the static constructor or a one-time initializer
                    client.BaseAddress = new Uri("http://api.plumitnetwork.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    // Assuming http://localhost:4354/api/ as BaseAddress 

                    var response = client.GetStringAsync("remove/authKey/" + Properties.Settings.Default.AuthKey).Result;
                        if (response == "1")
                    {
                        if (File.Exists(Path.GetFullPath("users.txt")))
                        {
                            File.Delete(Path.GetFullPath("users.txt"));
                        }

                        if (File.Exists(Path.GetFullPath("form-data.txt")))
                        {
                            File.Delete(Path.GetFullPath("form-data.txt"));
                        }
                        DataTransferProperty.AuthKey = "";
                        Properties.Settings.Default.AuthKey = "";
                        Properties.Settings.Default.userid = "";
                        Properties.Settings.Default.password = "";
                        Properties.Settings.Default.email = "";
                        Properties.Settings.Default.filetaken = "";
                        Properties.Settings.Default.firstName = "";
                        Properties.Settings.Default.lastName = "";
                        Properties.Settings.Default.filedone = "0";
                        Properties.Settings.Default.formserial = "0";
                        Properties.Settings.Default.registrationdate = DateTime.Now.ToString("MM/dd/yyyy_hh:mm_tt");
                        Properties.Settings.Default.Save();
                        DialogResult dialogResult = MessageBox.Show("Your project is reset successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.OK)
                        {
                            this.Hide();
                            log_in logIn = new log_in();
                            logIn.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    function.MessageBox("Failed to reset project, You are not registered to system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
