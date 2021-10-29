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
    public partial class dashboard : Form
    {
        private Point mouse_offset;
        public dashboard()
        {
            InitializeComponent();
            //place this code in your form constructor
            btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            btnClose.BackColorChanged += (s, e) =>
            {
                btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            };
            btnMinimize.FlatAppearance.MouseOverBackColor = btnMinimize.BackColor;
            btnMinimize.BackColorChanged += (s, e) =>
            {
                btnMinimize.FlatAppearance.MouseOverBackColor = btnMinimize.BackColor;
            };
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void dashboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void dashboard_MouseDown(object sender, MouseEventArgs e)
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult =
                MessageBox.Show("Are you sure want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnGroup1_Click(object sender, EventArgs e)
        {
            panelGrp1.Enabled = true;
            panelGrp2.Enabled = panelGrp3.Enabled = false;
        }

        private void btnGroup2_Click(object sender, EventArgs e)
        {
            panelGrp2.Enabled = true;
            panelGrp1.Enabled = panelGrp3.Enabled = false;
        }

        private void btnGroup3_Click(object sender, EventArgs e)
        {
            panelGrp3.Enabled = true;
            panelGrp1.Enabled = panelGrp2.Enabled = false;
        }
    }
}
