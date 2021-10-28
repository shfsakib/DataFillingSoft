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
    public partial class authentication : Form
    {
        private Point mouse_auth_offset;

        public authentication()
        {
            InitializeComponent();
            //place this code in your form constructor
            btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            btnClose.BackColorChanged += (s, e) => {
                btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            };
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
    }
}
