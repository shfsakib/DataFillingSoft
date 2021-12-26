using System;
using System.Collections;
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
    public partial class authentication : Form
    {
        private Point mouse_auth_offset;
        private Function function;
        private ApiDataModel apiDataModel;
        public authentication()
        {
            InitializeComponent();
            //place this code in your form constructor
            btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            btnClose.BackColorChanged += (s, e) =>
            {
                btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            };
            function = Function.GetInstance();
            apiDataModel = ApiDataModel.GetInstance();
        }

        private void authentication_Load(object sender, EventArgs e)
        {

        }

        private void authentication_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_auth_offset.X, mouse_auth_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void authentication_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_auth_offset = new Point(-e.X, -e.Y);
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            log_in login = new log_in();
            login.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_auth_offset = new Point(-e.X, -e.Y);

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_auth_offset.X, mouse_auth_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!function.IsConnected())
            {
                function.MessageBox("Please connect the internet", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // MessageBox.Show("Please connect the internet", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtAuthKey.Text == "")
                {
                    function.MessageBox("Authentication key can\'t be null", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    // MessageBox.Show("Authentication key can\'t be null", "Error", MessageBoxButtons.OK,
                    // MessageBoxIcon.Warning);
                    return;
                }

                HttpClient client = new HttpClient();
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri("http://api.transonictec.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                // Assuming http://localhost:4354/api/ as BaseAddress 

                var response = client.GetStringAsync("fetch/three/" + txtAuthKey.Text).Result;
                var api = JsonConvert.DeserializeObject<ApiDataModel>(response);
                apiDataModel = api; 
                if (api != null)
                {
                    if (api.mac_address == null)
                    {
                        registration registration = new registration();
                        DataTransferProperty.AuthKey = txtAuthKey.Text;
                        Properties.Settings.Default.AuthKey = txtAuthKey.Text;
                        Properties.Settings.Default.Save();
                        this.Hide();
                        registration.Show();
                    }
                    else
                    {
                        if (api.mac_address.ToString() == function.MacAddress())
                        {
                            registration registration = new registration();
                            DataTransferProperty.AuthKey = txtAuthKey.Text;
                            Properties.Settings.Default.AuthKey = txtAuthKey.Text;
                            Properties.Settings.Default.Save();
                            this.Hide();
                            registration.Show();
                        }
                        else
                        {
                            function.MessageBox("Your authentication key is invalid or already registered", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    function.MessageBox("Your authentication key is invalid or already registered", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
