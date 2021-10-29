using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.ui;

namespace DataFillingSoftDeskApp
{
    public partial class preloader : Form
    {
        public preloader()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressbar.Width += 10;
            if (progressbar.Width > 0 & progressbar.Width < 100)
            {
                lbltext.Text = "Welcome";
                lbltext.Location = new Point(212, 130);
            }
            else if (progressbar.Width > 100 & progressbar.Width < 200)
            {
                lbltext.Text = "Loading Database...";
                lbltext.Location = new Point(177, 130);
            }
            else if (progressbar.Width > 200 & progressbar.Width <= 370)
            {
                lbltext.Text = "Loading User Interface...";
                lbltext.Location = new Point(152, 130);
            }
            if (progressbar.Width == 370)
            {
                this.Hide();
                timer1.Enabled = false;
                log_in login = new log_in();
                login.Show();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
