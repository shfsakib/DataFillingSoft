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

namespace DataFillingSoftDeskApp.ui
{
    public partial class dashboard : Form
    {
        private Point mouse_offset;
        private string fileName = "";
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
            btnGroup1.Enabled = false;
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
            panelGrp1.Enabled = btnGroup2.Enabled = btnGroup3.Enabled = true;
            panelGrp2.Enabled = panelGrp3.Enabled = btnGroup1.Enabled = false;

        }

        private void btnGroup2_Click(object sender, EventArgs e)
        {
            panelGrp2.Enabled = btnGroup1.Enabled = btnGroup3.Enabled = true;
            panelGrp1.Enabled = panelGrp3.Enabled = btnGroup2.Enabled = false;

        }

        private void btnGroup3_Click(object sender, EventArgs e)
        {
            panelGrp3.Enabled = btnGroup1.Enabled = btnGroup2.Enabled = true;
            panelGrp1.Enabled = panelGrp2.Enabled = btnGroup3.Enabled = false;
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            //// open file dialog   
            //OpenFileDialog open = new OpenFileDialog();
            //// image filters  
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.bmp)|*.jpg; *.jpeg;  *.bmp";
            //if (open.ShowDialog() == DialogResult.OK)
            //{
            //    // display image in picture box  
            //    pictureBox1.Image = new Bitmap(open.FileName);
            //    // image file path  
            //    fileName = open.FileName;

            //}
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    byte[] bytes = File.ReadAllBytes(fileName);
                    string contentType = "";
                    //Set the contenttype based on File Extension

                    switch (Path.GetExtension(fileName))
                    {
                        case ".jpg":
                            contentType = "image/jpeg";
                            break;
                        case ".png":
                            contentType = "image/png";
                            break;
                        case ".gif":
                            contentType = "image/gif";
                            break;
                        case ".bmp":
                            contentType = "image/bmp";
                            break;
                    }

                    pictureBox1.Image = Image.FromStream(new MemoryStream(bytes));
                }
            }
        }

        private void AddTextToTextbox(TextBox textBox, string dataText)
        {
            if (textBox.Text.Contains(dataText))
            {
                textBox.Text=textBox.Text.Replace(dataText,null);
            }
            else
            {
                textBox.Text = dataText + textBox.Text;
            }

        }
        private void AddTextToRichTextbox(RichTextBox textBox, string dataText)
        {
            if (textBox.Text.Contains(dataText))
            {
                textBox.Text = textBox.Text.Replace(dataText, null);
            }
            else
            {
                textBox.Text = dataText + textBox.Text;
            }

        }
        private void btnFormNoB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFormNo,"<B>");
        }

        private void btnFormNoI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFormNo, "<I>");

        }

        private void btnFormNoU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFormNo, "<U>");

        }

        private void btnFormNoR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFormNo, "<R>");

        }

        private void btnComCodeB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyCode, "<B>");

        }

        private void btnComCodeI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyCode, "<I>");

        }

        private void btnComCodeU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyCode, "<U>");

        }

        private void btnComCodeR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyCode, "<R>");

        }

        private void btnComNameB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyName, "<B>");

        }

        private void btnComNameI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyName, "<I>");

        }

        private void btnComNameU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyName, "<U>");

        }

        private void btnComNameR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCompanyName, "<R>");

        }

        private void btnComAddB_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richAddress, "<B>");

        }

        private void btnComAddI_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richAddress, "<I>");

        }

        private void btnComAddU_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richAddress, "<U>");

        }

        private void btnComAddR_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richAddress, "<R>");

        }

        private void btnZipB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtZip, "<B>");

        }

        private void btnZipI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtZip, "<I>");

        }

        private void btnZipU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtZip, "<U>");

        }

        private void btnZipR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtZip, "<R>");

        }

        private void btnFaxB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFax, "<B>");

        }

        private void btnFaxI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFax, "<I>");

        }

        private void btnFaxU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFax, "<U>");

        }

        private void btnFaxR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFax, "<R>");

        }

        private void btnWebB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtWebsite, "<B>");

        }

        private void btnWebI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtWebsite, "<I>");

        }

        private void btnWebU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtWebsite, "<U>");

        }

        private void btnWebR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtWebsite, "<R>");
        }

        private void btnEmailB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtEmail, "<B>");

        }

        private void btnEmailI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtEmail, "<I>");

        }

        private void btnEmailU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtEmail, "<U>");

        }

        private void btnEmailR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtEmail, "<R>");

        }

        private void btnContB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtContactNo, "<B>");

        }

        private void btnContI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtContactNo, "<I>");

        }

        private void btnContU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtContactNo, "<U>");

        }

        private void btnContR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtContactNo, "<R>");

        }

        private void btnStateB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtState, "<B>");

        }

        private void btnStateI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtState, "<I>");

        }

        private void btnStateU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtState, "<U>");

        }

        private void btnStateR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtState, "<R>");

        }
    }
}
