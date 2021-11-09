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
    public partial class registration : Form
    {
        private Point mouse_offset;
        private Function function;
        private ApiDataModel apiDataModel;
        public registration()
        {
            InitializeComponent();
            function = Function.GetInstance();
            apiDataModel = ApiDataModel.GetInstance();

        }
        private void registration_Load(object sender, EventArgs e)
        {
            LoadData();
            richAddress.Focus();
        }
        private void LoadData()
        {
            HttpClient client = new HttpClient();
            // It can be the static constructor or a one-time initializer
            client.BaseAddress = new Uri("http://api.plumitnetwork.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            // Assuming http://localhost:4354/api/ as BaseAddress 

            var response = client.GetStringAsync("fetch/three/" + DataTransferProperty.AuthKey).Result;
            var api = JsonConvert.DeserializeObject<ApiDataModel>(response);

            apiDataModel = api;
           
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtFirstName.Text = apiDataModel.fname;
            txtLastName.Text = apiDataModel.lname;
            txtEmail.Text = apiDataModel.email;
            txtContactNo.Text = apiDataModel.mobile;
            richAddress.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtNoForms_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            log_in login = new log_in();
            login.Show();
        }

        private void registration_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void registration_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);

        }
         
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (richAddress.Text == "")
            {
                function.MessageBox("Address is required", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!radioMale.Checked && !radioFemale.Checked)
            {
                function.MessageBox("Gender is required", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAge.Text == "")
            {
                function.MessageBox("Age is required", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtNoForms.Text == "" || txtUserId.Text == "" || txtPass.Text == "")
            {
                function.MessageBox("All project related information is required", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPass.Text != txtConfirmPass.Text)
            {
                function.MessageBox("Password mismatch", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string gender = "Female";
                if (radioMale.Checked)
                {
                    gender = "Male";
                }
                try
                {

                    HttpClient client = new HttpClient();
                    // It can be the static constructor or a one-time initializer
                    client.BaseAddress = new Uri("http://api.plumitnetwork.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    // Assuming http://localhost:4354/api/ as BaseAddress 

                    //var response = client.GetStringAsync("onMacP""ost/" + DataTransferProperty.AuthKey + "/" + function.MacAddress()).Result;
                    var response = client.GetStringAsync("onMacPost/" + DataTransferProperty.AuthKey + "/" + function.MacAddress()).Result;

                    bool ans = false;
                    StreamWriter streamWriter = new StreamWriter(@"users.txt"); streamWriter.WriteLine(txtFirstName.Text + "\t/" + txtLastName.Text + "\t/" + txtEmail.Text + "\t/" + txtContactNo.Text + "\t/" + richAddress.Text + "\t/" + gender + "\t/" + txtAge.Text + "\t/" + txtNoForms.Text + "\t/" + txtUserId.Text + "\t/" + txtPass.Text + "\t/" + function.MacAddress() + "\t/" + DataTransferProperty.AuthKey + "\t/" + DateTime.Now.ToString("MM/dd/yyyy_hh:mm_tt") + "\t/" + "A");
                    ans = true;
                    streamWriter.Close();
                   
                    if (ans)
                    {
                        if (response == "1")
                        {
                            Properties.Settings.Default.userid = txtUserId.Text;
                            Properties.Settings.Default.password = txtPass.Text;
                            Properties.Settings.Default.email = txtEmail.Text;
                            Properties.Settings.Default.filetaken = txtNoForms.Text;
                            Properties.Settings.Default.firstName = txtFirstName.Text;
                            Properties.Settings.Default.lastName = txtLastName.Text;
                            Properties.Settings.Default.filedone = "0";
                            Properties.Settings.Default.formserial = "0";
                            Properties.Settings.Default.registrationdate = DateTime.Now.ToString("MM/dd/yyyy_hh:mm_tt");
                            Properties.Settings.Default.Save();
                            DialogResult dialogResult = MessageBox.Show($"Message \r\nRegistration Successfull\r\nRemember This For Login Details\r\n\r\nUser Id: {txtUserId.Text}\r\nPassword: {txtPass.Text}\r\n\r\nThis Form Will Automatically Close and Login Window Will Open", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (dialogResult == DialogResult.OK)
                            {
                                log_in login = new log_in();
                                this.Hide();
                                login.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please check your internet connection and try again.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to access database. Please run this application as administrator.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please check your internet connection", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Aro you sure want to clear all field?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                richAddress.Text = txtAge.Text = txtNoForms.Text = txtUserId.Text = txtPass.Text = txtConfirmPass.Text = null;
                radioMale.Checked = radioFemale.Checked = false;
            }

        }
    }
}
