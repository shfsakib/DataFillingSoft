using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFillingSoftDeskApp.ui
{
    public partial class log_in : Form
    {
        private Point mouse_offset;
        public log_in()
        {
            InitializeComponent();
            //place this code in your form constructor
            btnloginClose.FlatAppearance.MouseOverBackColor = btnloginClose.BackColor;
            btnloginClose.BackColorChanged += (s, e) => {
                btnloginClose.FlatAppearance.MouseOverBackColor = btnloginClose.BackColor; 
            };
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
            authentication authForm=new authentication();
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
    }
}
