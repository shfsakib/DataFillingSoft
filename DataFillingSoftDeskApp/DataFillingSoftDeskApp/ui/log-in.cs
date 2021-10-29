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
        }
        private void log_in_Load(object sender, EventArgs e)
        {
            lblName.Text = String.Format("Universal Education{0} And Research", "\r\n");
            panelUserName.SendToBack();
            panelPass.SendToBack();
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
            string pass =
                function.IsExist(
                    $"SELECT DesktopPassword FROM Users WHERE UserName='{txtUserName.Text}' AND DesktopPassword='{txtPassword.Text}' AND UserStatus='A' AND MacAddress='{function.MacAddress()}' COLLATE Latin1_General_CS_AI");
            if (pass == txtPassword.Text.Trim())
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
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
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
