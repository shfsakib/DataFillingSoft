using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            apiDataModel=ApiDataModel.GetInstance();

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
            client.BaseAddress = new Uri("http://api.etnyzfood.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            // Assuming http://localhost:4354/api/ as BaseAddress 

            var response = client.GetStringAsync("fetch/two/" + DataTransferProperty.AuthKey).Result;
            var api = JsonConvert.DeserializeObject<ApiDataModel>(response);

            apiDataModel = api;
            //txtDate.Text = DataTransferProperty.AuthKey;
            //DateTime date = Convert.ToDateTime(function.IsExist($"SELECT RegistrationDate FROM USERS WHERE AuthenticationKey='{DataTransferProperty.AuthKey}'"));
            //txtDate.Text = date.ToString("yyyy-MM-dd");
            //txtFirstName.Text = function.IsExist($"SELECT FirstName FROM USERS WHERE AuthenticationKey='{DataTransferProperty.AuthKey}'");
            //txtLastName.Text = function.IsExist($"SELECT LastName FROM USERS WHERE AuthenticationKey='{DataTransferProperty.AuthKey}'");
            //txtEmail.Text = function.IsExist($"SELECT MobileNo FROM USERS WHERE AuthenticationKey='{DataTransferProperty.AuthKey}'");
            //txtContactNo.Text = function.IsExist($"SELECT Email FROM USERS WHERE AuthenticationKey='{DataTransferProperty.AuthKey}'");
            txtDate.Text = apiDataModel.created_at;
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

        private bool IsUserExist()
        {
            bool result = false;
            string x = function.IsExist($@"SELECT UserName FROM Users WHERE UserName='{txtUserId.Text}'");
            if (x != "")
            {
                result = true;
            }

            return result;
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
            else if (IsUserExist())
            {
                function.MessageBox("User id already exist please use another user id", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string gender = "Female";
                if (radioMale.Checked)
                {
                    gender = "Male";
                }
                bool ans = function.Execute(
                    $"INSERT INTO Users(FirstName,LastName,Email,MobileNo,Address,Gender,Age,FormNo,UserName,DesktopPassword,MacAddress,AuthenticationKey,RegistrationDate,UserStatus) VALUES('{txtFirstName.Text}','{txtLastName.Text}','{txtEmail.Text}','{txtContactNo.Text}','{richAddress.Text}','{gender}','{txtAge.Text}','{txtNoForms.Text}','{txtUserId.Text}','{txtPass.Text}','{function.MacAddress()}','{DataTransferProperty.AuthKey}','{txtDate.Text}','A')");

                //bool ans = function.Execute(
                //    $"UPDATE Users SET Address='{richAddress.Text}',Gender='{gender}',Age='{txtAge.Text}',FormNo='{txtNoForms.Text}',UserName='{txtUserId.Text}',DesktopPassword='{txtPass.Text}',MacAddress='{function.MacAddress()}' WHERE AuthenticationKey='{DataTransferProperty.AuthKey}'");
                if (ans)
                {
                    DialogResult dialogResult = MessageBox.Show($"Message \r\nRegistration Successfull\r\nRemember This For Login Details\r\n\r\nUser Id: {txtUserId.Text}\r\nPassword: {txtPass.Text}\r\n\r\nThis Form Will Automatically Close and Login Window Will Open", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        log_in login = new log_in();
                        this.Hide();
                        login.Show();
                    }
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
