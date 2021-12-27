using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.Class;

namespace DataFillingSoftDeskApp.ui
{
    public partial class log_in : Form
    {
        private Point mouse_offset;
        private Function function;
        public log_in()
        {
            InitializeComponent();
            //place this code in your form constructor
            btnloginClose.FlatAppearance.MouseOverBackColor = btnloginClose.BackColor;
            btnloginClose.BackColorChanged += (s, e) =>
            {
                btnloginClose.FlatAppearance.MouseOverBackColor = btnloginClose.BackColor;
            };
            function = Function.GetInstance();
            txtUserName.Focus();
        }
        private void log_in_Load(object sender, EventArgs e)
        {
            panelUserName.SendToBack();
            panelPass.SendToBack();
            txtUserName.Focus();
            lblName.Text = String.Format("Transonic Data{0} Technology Corp.{0}USA", "\r\n");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void log_in_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void log_in_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }
        private void txtUserName_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = null;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            authentication authForm = new authentication();
            authForm.Show();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);

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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.password.ToString() == txtPassword.Text.Trim() && Properties.Settings.Default.password.ToString() != "")
            {
                DataTransferProperty.UserId = txtUserName.Text;
                DataTransferProperty.AuthKey = Properties.Settings.Default.AuthKey;
                this.Hide();
                dashboard dashboard = new dashboard();
                dashboard.Show();
            }
            else
            {
                function.MessageBox("Incorrect Email or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!function.IsConnected())
            {
                function.MessageBox("Please connect the internet", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dialogResult = MessageBox.Show(
                "Resetting project will delete all form filled by you and also registration details\r\n\r\nBefore resetting project please make a copy of Installation folder\r\n\r\nAre you sure want to reset project?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                warning warning = new warning();
                this.Hide();
                warning.Show();
            }
        }
    }
}
