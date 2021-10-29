using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.Class;

namespace DataFillingSoftDeskApp.ui
{
    public partial class authentication : Form
    {
        private Point mouse_auth_offset;
        private Function function;
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
                string isExist = function.IsExist($"SELECT AuthenticationKey FROM USERS WHERE AuthenticationKey='{txtAuthKey.Text}' AND MacAddress=''");
                if (isExist != "")
                {
                    //bool ans = function.Execute(
                    //    $"UPDATE USERS SET MacAddress='{function.MacAddress()}' WHERE AuthenticationKey='{txtAuthKey.Text}'");
                    //if (ans)
                    //{
                        registration registration = new registration();
                        DataTransferProperty.AuthKey = txtAuthKey.Text;
                        Properties.Settings.Default.AuthKey = txtAuthKey.Text;
                        Properties.Settings.Default.Save();
                        this.Hide();
                        registration.Show();
                    //}
                    //else
                    //{
                    //    function.MessageBox("Failed to get mac address, please update you network drive", "Error",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}

                }
                else
                {
                    function.MessageBox("Your authentication key is invalid or already registered", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Your authentication key is invalid or already registered", "Error",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
