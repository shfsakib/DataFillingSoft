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
    public partial class warning : Form
    {
        private Point mouse_offset;
        private Function function;
        public warning()
        {
            InitializeComponent();
            function = Function.GetInstance();
        }

        private void warning_Load(object sender, EventArgs e)
        {
            lblAsk.Text = "This Will Delete All Form Entered By You As Well As Registration Details";
        }

        private void warning_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void warning_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            log_in logIn = new log_in();
            logIn.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AuthKey.ToString()=="")
            {
                DialogResult dialogResult = MessageBox.Show("You are not registered yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                if (dialogResult == DialogResult.OK)
                {
                    this.Hide();
                    log_in logIn = new log_in();
                    logIn.Show();
                }
                return;
            }
            bool ans = function.Execute($"UPDATE Users SET Address='',Gender='',Age='',FormNo='',UserName='',DesktopPassword='',MacAddress='',AuthenticationKey='' WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");
            if (ans)
            {
                function.Execute($"DELETE FROM FormData WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");
                DataTransferProperty.AuthKey = "";
                Properties.Settings.Default.AuthKey = "";
                Properties.Settings.Default.Save();
                DialogResult dialogResult = MessageBox.Show("Your project is reset successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    this.Hide();
                    log_in logIn = new log_in();
                    logIn.Show();
                }
            }
            else
            {
                function.MessageBox("Failed to reset project, You are not registered to system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
