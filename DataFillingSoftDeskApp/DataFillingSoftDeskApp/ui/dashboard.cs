using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.Class;

namespace DataFillingSoftDeskApp.ui
{
    public partial class dashboard : Form
    {
        private Point mouse_offset;
        private string fileName = "";
        private Function function;
        private byte[] bytes;
        private string imageString = "";
        int nextClick = 0;
        private int prevClick = 0;
        string serial = "";
        ContextMenu emptyMenu = new ContextMenu();
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
            function = Function.GetInstance();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            btnGroup1.Enabled = false;
            LoadData();
            lblFormSl.Text = "";
        }

        private void LoadData()
        {
            txtFileTaken.Text = Properties.Settings.Default.filetaken;
            if (File.Exists(Path.GetFullPath("form-data.txt")))
            {
                string[] allLine = File.ReadAllLines("form-data.txt");
                txtFileDone.Text = allLine.Length.ToString();
            }
            else
                txtFileDone.Text = "0";

            float taken = Convert.ToInt32(txtFileTaken.Text);
            float done = Convert.ToInt32(txtFileDone.Text);
            float percent = (done / taken) * 100;
            if (percent == 0)
            {
                panelProgressBar.Width = 0;
                lblPercentage.Text = "0%";
            }
            else if (percent < 100)
            {
                panelProgressBar.Width = Convert.ToInt32(percent) * 3;
                lblPercentage.Text = percent.ToString() + "%";
            }
            else if (percent == 100)
            {
                panelProgressBar.Width = 268;
                lblPercentage.Text = "100%";
            }

            txtClientName.Text = Properties.Settings.Default.firstName;
            string date = Properties.Settings.Default.registrationdate;
            string userId = Properties.Settings.Default.userid;
            txtUserIdRegDate.Text = userId + " : " + date;
            if (taken == done)
            {
                btnSave.Enabled = false;
                btnSubmit.Enabled = true;
            }

            lblDate.Text = DateTime.Now.ToString();
            string regDate = date;
            lblExpireDate.Text = (Convert.ToDateTime(regDate.Substring(0, 10)).AddDays(21)).ToString("MM/dd/yyyy") + " " + regDate.Substring(11, 8).Replace("_", " ");
            if (Convert.ToDateTime(lblDate.Text) >= Convert.ToDateTime(lblExpireDate.Text))
            {
                btnLoadFiles.Enabled = btnNewForm.Enabled = btnPrevForm.Enabled = btnNextForm.Enabled =
                    btnGroup1.Enabled = btnGroup2.Enabled =
                        btnGroup3.Enabled = btnSave.Enabled = btnViewData.Enabled = panelGrp1.Enabled = false;
            }

            txtFormNo.ContextMenu = emptyMenu;
            txtCompanyCode.ContextMenu = emptyMenu;
            txtCompanyName.ContextMenu = emptyMenu;
            txtZip.ContextMenu = emptyMenu;
            txtFax.ContextMenu = emptyMenu; 
            txtWebsite.ContextMenu = emptyMenu;
            txtEmail.ContextMenu = emptyMenu;
            txtContactNo.ContextMenu = emptyMenu;
            txtState.ContextMenu = emptyMenu;
            txtCountry.ContextMenu = emptyMenu;
            txtHeadQuarter.ContextMenu = emptyMenu;
            txtNoofEmp.ContextMenu = emptyMenu;
            txtBrandAmbs.ContextMenu = emptyMenu;
            txtMediaPart.ContextMenu = emptyMenu;
            txtSocialMedia.ContextMenu = emptyMenu;
            txtFrenPart.ContextMenu = emptyMenu;
            txtInvestor.ContextMenu = emptyMenu;
            txtAdvtPart.ContextMenu = emptyMenu;
            txtProduct.ContextMenu = emptyMenu;
            txtServices.ContextMenu = emptyMenu;
            txtManager.ContextMenu = emptyMenu;
            txtRegDate.ContextMenu = emptyMenu;
            txtYearlyRev.ContextMenu = emptyMenu;
            txtLandMark.ContextMenu = emptyMenu;
            txtAccAudit.ContextMenu = emptyMenu;
            txtYearlyExpense.ContextMenu = emptyMenu;
            txtCurrency.ContextMenu = emptyMenu;
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
                this.Hide();
                log_in login = new log_in();
                login.Show();
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
            txtFormNo.Focus();
        }

        private void btnGroup2_Click(object sender, EventArgs e)
        {
            panelGrp2.Enabled = btnGroup1.Enabled = btnGroup3.Enabled = true;
            panelGrp1.Enabled = panelGrp3.Enabled = btnGroup2.Enabled = false;
            txtCountry.Focus();
        }

        private void btnGroup3_Click(object sender, EventArgs e)
        {
            panelGrp3.Enabled = btnGroup1.Enabled = btnGroup2.Enabled = true;
            panelGrp1.Enabled = panelGrp2.Enabled = btnGroup3.Enabled = false;
            txtProduct.Focus();
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + "." + Path.GetExtension(openFileDialog1.FileName);
                    bytes = File.ReadAllBytes(openFileDialog1.FileName);
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
                    Clear();

                    pictureBox1.Image = Image.FromStream(new MemoryStream(bytes));
                    imageString = function.ImageToBase64(pictureBox1.Image, ImageFormat.Png);
                }
            }
        }

        private void AddTextToTextbox(TextBox textBox, string dataText)
        {
            if (textBox.Text.Contains(dataText))
            {
                textBox.Text = textBox.Text.Replace(dataText, null);
            }
            else
            {
                textBox.Text = dataText + textBox.Text;
                textBox.Focus();

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
                textBox.Focus();
            }
        }
        private void btnFormNoB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFormNo, "<B>");
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

        private void btnCountryB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCountry, "<B>");

        }

        private void btnCountryI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCountry, "<I>");

        }

        private void btnCountryU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCountry, "<U>");

        }

        private void btnCountryR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCountry, "<R>");

        }

        private void btnHeadB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtHeadQuarter, "<B>");

        }

        private void btnHeadI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtHeadQuarter, "<I>");

        }

        private void btnHeadU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtHeadQuarter, "<U>");

        }

        private void btnHeadR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtHeadQuarter, "<R>");

        }

        private void btnEmpNoB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtNoofEmp, "<B>");

        }

        private void btnEmpNoI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtNoofEmp, "<I>");

        }

        private void btnEmpNoU_Click(object sender, EventArgs e)
        {

            AddTextToTextbox(txtNoofEmp, "<U>");
        }

        private void btnEmpNoR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtNoofEmp, "<R>");

        }

        private void btnIndustryB_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richIndustry, "<B>");

        }

        private void btnIndustryI_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richIndustry, "<I>");

        }

        private void btnIndustryU_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richIndustry, "<U>");

        }

        private void btnIndustryR_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richIndustry, "<R>");

        }

        private void btnBAmB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtBrandAmbs, "<B>");

        }

        private void btnBAmI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtBrandAmbs, "<I>");

        }

        private void btnBAmU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtBrandAmbs, "<U>");

        }

        private void btnBAmR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtBrandAmbs, "<R>");

        }

        private void btnMediaB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtMediaPart, "<B>");

        }

        private void btnMediaI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtMediaPart, "<I>");

        }

        private void btnMediaU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtMediaPart, "<U>");

        }

        private void btnMediaR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtMediaPart, "<R>");

        }

        private void btnSocialB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtSocialMedia, "<B>");

        }

        private void btnSocialI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtSocialMedia, "<I>");

        }

        private void btnSocialU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtSocialMedia, "<U>");

        }

        private void btnSocialR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtSocialMedia, "<R>");

        }

        private void btnFrenPtB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFrenPart, "<B>");

        }

        private void btnFrenPtI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFrenPart, "<I>");

        }

        private void btnFrenPtU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFrenPart, "<U>");

        }

        private void btnFrenPtR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtFrenPart, "<R>");

        }

        private void btnInvestorB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtInvestor, "<B>");

        }

        private void btnInvestorI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtInvestor, "<I>");

        }

        private void btnInvestorU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtInvestor, "<U>");

        }

        private void btnInvestorR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtInvestor, "<R>");

        }

        private void btnAdB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAdvtPart, "<B>");

        }

        private void btnAdI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAdvtPart, "<I>");

        }

        private void btnAdU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAdvtPart, "<U>");

        }

        private void btnAdR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAdvtPart, "<R>");

        }

        private void btnProductB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtProduct, "<B>");

        }

        private void btnProductI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtProduct, "<I>");

        }

        private void btnProductU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtProduct, "<U>");

        }

        private void btnProductR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtProduct, "<R>");

        }

        private void btnServicesB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtServices, "<B>");

        }

        private void btnServicesI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtServices, "<I>");

        }

        private void btnServicesU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtServices, "<U>");

        }

        private void btnServicesR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtServices, "<R>");

        }

        private void btnManageB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtManager, "<B>");

        }

        private void btnManageI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtManager, "<I>");

        }

        private void btnManageU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtManager, "<U>");

        }

        private void btnManageR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtManager, "<R>");

        }

        private void btnRegDtB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtRegDate, "<B>");

        }

        private void btnRegDtI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtRegDate, "<I>");

        }

        private void btnRegDtU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtRegDate, "<U>");

        }

        private void btnRegDtR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtRegDate, "<R>");

        }

        private void btnYearlyB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyRev, "<B>");

        }

        private void btnYearlyI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyRev, "<I>");

        }

        private void btnYearlyU_Click(object sender, EventArgs e)
        {

            AddTextToTextbox(txtYearlyRev, "<U>");
        }

        private void btnYearlyR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyRev, "<R>");

        }

        private void btnSubClasB_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richSubClassification, "<B>");

        }

        private void btnSubClasI_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richSubClassification, "<I>");

        }

        private void btnSubClasU_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richSubClassification, "<U>");

        }

        private void btnSubClasR_Click(object sender, EventArgs e)
        {
            AddTextToRichTextbox(richSubClassification, "<R>");

        }

        private void btnLandMarkB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtLandMark, "<B>");

        }

        private void btnLandMarkI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtLandMark, "<I>");

        }

        private void btnLandMarkU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtLandMark, "<U>");

        }

        private void btnLandMarkR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtLandMark, "<R>");

        }

        private void btnAccAuditB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAccAudit, "<B>");

        }

        private void btnAccAuditI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAccAudit, "<I>");

        }

        private void btnAccAuditU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAccAudit, "<U>");

        }

        private void btnAccAuditR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtAccAudit, "<R>");

        }

        private void btnCurrencyB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCurrency, "<B>");

        }

        private void btnCurrencyI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCurrency, "<I>");

        }

        private void btnCurrencyU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCurrency, "<U>");

        }

        private void btnCurrencyR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtCurrency, "<R>");

        }

        private void btnYearExpnB_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyExpense, "<B>");

        }

        private void btnYearExpnI_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyExpense, "<I>");

        }

        private void btnYearExpnU_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyExpense, "<U>");

        }

        private void btnYearExpnR_Click(object sender, EventArgs e)
        {
            AddTextToTextbox(txtYearlyExpense, "<R>");

        }

        private void btnNewForm_Click(object sender, EventArgs e)
        {
            LoadData();
            Clear();

        }

        private void Clear()
        {
            txtFormNo.Text = txtCompanyCode.Text = txtCompanyName.Text = richAddress.Text =
                txtZip.Text = txtFax.Text = txtWebsite.Text = txtEmail.Text = txtContactNo.Text = txtState.Text = null;
            txtCountry.Text = txtHeadQuarter.Text = txtNoofEmp.Text = richIndustry.Text = txtBrandAmbs.Text =
                txtMediaPart.Text = txtSocialMedia.Text = txtFrenPart.Text = txtInvestor.Text = txtAdvtPart.Text = null;
            txtProduct.Text = txtServices.Text = txtManager.Text = txtRegDate.Text = txtYearlyRev.Text =
                richSubClassification.Text =
                    txtLandMark.Text = txtAccAudit.Text = txtCurrency.Text = txtYearlyExpense.Text = null;
            lblFormSl.Text = "";
            nextClick = 0;
            pictureBox1.Image = Properties.Resources.picture;
            panelGrp1.Enabled = true;
            panelGrp2.Enabled = panelGrp3.Enabled = false;
            btnGroup1.Enabled = false;
            btnGroup2.Enabled = btnGroup3.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblFormSl.Text == "" || lblFormSl.Text == null || string.IsNullOrEmpty(lblFormSl.Text))
                {
                    if (imageString == "")
                    {
                        function.MessageBox("Please load demo image first", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;

                    }

                    string oldData = "";
                    if (File.Exists(Path.GetFullPath("form-data.txt")))
                    {
                        StreamReader streamReader = new StreamReader("form-data.txt");
                        oldData = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                    bool ans = false;

                    if (oldData != "")
                    {
                        StreamWriter streamWriter = new StreamWriter("form-data.txt");
                        streamWriter.WriteLine(oldData + (Convert.ToInt32(Properties.Settings.Default.formserial) + 1) + "\t/?" + fileName + "\t/?" + txtFormNo.Text + "\t/?" + txtCompanyCode.Text + "\t/?" + txtCompanyName.Text +
                                               "\t/?" + richAddress.Text + "\t/?" + txtZip.Text + "\t/?" + txtFax.Text +
                                               "\t/?" +
                                               txtWebsite.Text + "\t/?" + txtEmail.Text + "\t/?" + txtContactNo.Text + "\t/?" +
                                               txtState.Text + "\t/?" + txtCountry.Text + "\t/?" + txtHeadQuarter.Text +
                                               "\t/?" +
                                               txtNoofEmp.Text + "\t/?" + richIndustry.Text + "\t/?" + txtBrandAmbs.Text +
                                               "\t/?" +
                                               txtMediaPart.Text + "\t/?" + txtSocialMedia.Text + "\t/?" + txtFrenPart.Text +
                                               "\t/?" + txtInvestor.Text + "\t/?" + txtAdvtPart.Text + "\t/?" +
                                               txtProduct.Text +
                                               "\t/?" + txtServices.Text + "\t/?" + txtManager.Text + "\t/?" +
                                               txtRegDate.Text +
                                               "\t/?" + txtYearlyRev.Text + "\t/?" + richSubClassification.Text + "\t/?" +
                                               txtLandMark.Text + "\t/?" + txtAccAudit.Text + "\t/?" + txtCurrency.Text +
                                               "\t/?" +
                                               txtYearlyExpense.Text);
                        streamWriter.Close();

                    }
                    else
                    {
                        StreamWriter streamWriter = new StreamWriter("form-data.txt");
                        streamWriter.WriteLine((Convert.ToInt32(Properties.Settings.Default.formserial) + 1) + "\t/?" + fileName + "\t/?" + txtFormNo.Text + "\t/?" + txtCompanyCode.Text + "\t/?" + txtCompanyName.Text +
                                               "\t/?" + richAddress.Text + "\t/?" + txtZip.Text + "\t/?" + txtFax.Text +
                                               "\t/?" +
                                               txtWebsite.Text + "\t/?" + txtEmail.Text + "\t/?" + txtContactNo.Text + "\t/?" +
                                               txtState.Text + "\t/?" + txtCountry.Text + "\t/?" + txtHeadQuarter.Text +
                                               "\t/?" +
                                               txtNoofEmp.Text + "\t/?" + richIndustry.Text + "\t/?" + txtBrandAmbs.Text +
                                               "\t/?" +
                                               txtMediaPart.Text + "\t/?" + txtSocialMedia.Text + "\t/?" + txtFrenPart.Text +
                                               "\t/?" + txtInvestor.Text + "\t/?" + txtAdvtPart.Text + "\t/?" +
                                               txtProduct.Text +
                                               "\t/?" + txtServices.Text + "\t/?" + txtManager.Text + "\t/?" +
                                               txtRegDate.Text +
                                               "\t/?" + txtYearlyRev.Text + "\t/?" + richSubClassification.Text + "\t/?" +
                                               txtLandMark.Text + "\t/?" + txtAccAudit.Text + "\t/?" + txtCurrency.Text +
                                               "\t/?" +
                                               txtYearlyExpense.Text);
                        streamWriter.Close();
                    }

                    ans = true;
                    if (ans)
                    {
                        Clear();
                        Properties.Settings.Default.filedone =
                            (Convert.ToInt32(Properties.Settings.Default.filedone) + 1).ToString();
                        Properties.Settings.Default.formserial =
                            (Convert.ToInt32(Properties.Settings.Default.formserial) + 1).ToString();
                        Properties.Settings.Default.Save();
                        LoadData();
                        lblFormSl.Text = "";
                        function.MessageBox("Saved successfully", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {

                    bool ans = false;
                    string[] formData = File.ReadAllLines("form-data.txt");
                    if (formData.Length > 0)
                    {

                        formData[Convert.ToInt32(lblFormSl.Text) - 1] =
                            (serial + "\t/?" + fileName + "\t/?" + txtFormNo.Text + "\t/?" + txtCompanyCode.Text + "\t/?" +
                             txtCompanyName.Text +
                             "\t/?" + richAddress.Text + "\t/?" + txtZip.Text + "\t/?" + txtFax.Text +
                             "\t/?" +
                             txtWebsite.Text + "\t/?" + txtEmail.Text + "\t/?" + txtContactNo.Text + "\t/?" +
                             txtState.Text + "\t/?" + txtCountry.Text + "\t/?" + txtHeadQuarter.Text +
                             "\t/?" +
                             txtNoofEmp.Text + "\t/?" + richIndustry.Text + "\t/?" + txtBrandAmbs.Text +
                             "\t/?" +
                             txtMediaPart.Text + "\t/?" + txtSocialMedia.Text + "\t/?" + txtFrenPart.Text +
                             "\t/?" + txtInvestor.Text + "\t/?" + txtAdvtPart.Text + "\t/?" +
                             txtProduct.Text +
                             "\t/?" + txtServices.Text + "\t/?" + txtManager.Text + "\t/?" +
                             txtRegDate.Text +
                             "\t/?" + txtYearlyRev.Text + "\t/?" + richSubClassification.Text + "\t/?" +
                             txtLandMark.Text + "\t/?" + txtAccAudit.Text + "\t/?" + txtCurrency.Text +
                             "\t/?" +
                             txtYearlyExpense.Text);
                        File.WriteAllLines(Path.GetFullPath("form-data.txt"), formData);
                        ans = true;
                        if (ans)
                        {
                            Clear();
                            LoadData();
                            lblFormSl.Text = "";
                            function.MessageBox("Saved successfully", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to access database. Please run this application as administrator.",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        function.MessageBox("Please load demo image first", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnNextForm_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            if (nextClick == 0)
            {
                lblFormSl.Text = "1";
                GetFieldData();
                nextClick = 1;
            }
            else
            {
                string nextId = (Convert.ToInt32(lblFormSl.Text) + 1).ToString();
                if (nextId != "")
                {
                    lblFormSl.Text = nextId;
                    GetFieldData();
                }
            }

        }

        private void btnPrevForm_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            if (lblFormSl.Text != "")
            {
                string prevId = (Convert.ToInt32(lblFormSl.Text) - 1).ToString();
                if (prevId != "")
                {
                    lblFormSl.Text = prevId;
                    GetFieldData();
                }
            }

        }

        private void GetFieldData()
        {
            if (!File.Exists(Path.GetFullPath("form-data.txt")))
            {
                MessageBox.Show("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string[] allLine = File.ReadAllLines("form-data.txt");
            string[] values = new string[] { };
            if (allLine.Length > 0)
            {

                if (Convert.ToInt32(lblFormSl.Text) - 1 != allLine.Length && Convert.ToInt32(lblFormSl.Text) <= allLine.Length && Convert.ToInt32(lblFormSl.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(lblFormSl.Text); i++)
                    {
                        if (i == Convert.ToInt32(lblFormSl.Text) - 1)
                        {
                            values = allLine[i].ToString().Split('/');
                        }
                    }
                }
                else if (Convert.ToInt32(lblFormSl.Text) <= 0)
                {
                    for (int i = 0; i < Convert.ToInt32(1); i++)
                    {
                        if (i == Convert.ToInt32(lblFormSl.Text))
                        {
                            values = allLine[i].ToString().Split('/');
                        }
                    }
                    lblFormSl.Text = "1";
                }
                else if (lblFormSl.Text == "1")
                {
                    for (int i = 0; i < Convert.ToInt32(lblFormSl.Text); i++)
                    {
                        if (i == Convert.ToInt32(lblFormSl.Text) - 1)
                        {
                            values = allLine[i].ToString().Split('/');
                        }
                    }
                    lblFormSl.Text = "1";
                }
                else if (Convert.ToInt32(lblFormSl.Text) > allLine.Length)
                {
                    for (int i = 0; i < allLine.Length; i++)
                    {
                        if (i == allLine.Length - 1)
                        {
                            values = allLine[i].ToString().Split('/');
                        }
                    }

                    lblFormSl.Text = allLine.Length.ToString();
                }
                else
                {
                    for (int i = 0; i < allLine.Length; i++)
                    {
                        if (i == allLine.Length - 1)
                        {
                            values = allLine[i].ToString().Split('/');
                        }
                    }

                }
            }
            if (values.Length > 0)
            {
                serial = values[0].ToString().Trim();
                fileName = values[1].ToString().Trim();
                txtFormNo.Text = values[2].ToString().Trim();
                txtCompanyCode.Text = values[3].ToString().Trim();
                txtCompanyName.Text = values[4].ToString().Trim();
                richAddress.Text = values[5].ToString().Trim();
                txtZip.Text = values[6].ToString().Trim();
                txtFax.Text = values[7].ToString().Trim();
                txtWebsite.Text = values[8].ToString().Trim();
                txtEmail.Text = values[9].ToString().Trim();
                txtContactNo.Text = values[10].ToString().Trim();
                txtState.Text = values[11].ToString().Trim();
                txtCountry.Text = values[12].ToString().Trim();
                txtHeadQuarter.Text = values[13].ToString().Trim();
                txtNoofEmp.Text = values[14].ToString().Trim();
                richIndustry.Text = values[15].ToString().Trim();
                txtBrandAmbs.Text = values[16].ToString().Trim();
                txtMediaPart.Text = values[17].ToString().Trim();
                txtSocialMedia.Text = values[18].ToString().Trim();
                txtFrenPart.Text = values[19].ToString().Trim();
                txtInvestor.Text = values[20].ToString().Trim();
                txtAdvtPart.Text = values[21].ToString().Trim();
                txtProduct.Text = values[22].ToString().Trim();
                txtServices.Text = values[23].ToString().Trim();
                txtManager.Text = values[24].ToString().Trim();
                txtRegDate.Text = values[25].ToString().Trim();
                txtYearlyRev.Text = values[26].ToString().Trim();
                richSubClassification.Text = values[27].ToString().Trim();
                txtLandMark.Text = values[28].ToString().Trim();
                txtAccAudit.Text = values[29].ToString().Trim();
                txtCurrency.Text = values[30].ToString().Trim();
                txtYearlyExpense.Text = values[31].ToString().Trim();
            }
        }

        private void txtNoofEmp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            view_data viewData = new view_data();
            viewData.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult =
                MessageBox.Show(
                    "Have your project completed?\r\n\r\nAfter submission of your project you will not able to work with this project\r\n\r\nAre you sure?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                submission submission = new submission();
                submission.Show();

            }
        }

        private void txtFormNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtCompanyCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtCompanyName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void richAddress_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtZip_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtFax_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtWebsite_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtEmail_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtContactNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtState_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtCountry_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtHeadQuarter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtNoofEmp_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void richIndustry_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtBrandAmbs_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtMediaPart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtSocialMedia_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtFrenPart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtInvestor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtAdvtPart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtProduct_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtServices_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtManager_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtRegDate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtYearlyRev_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void richSubClassification_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtLandMark_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtAccAudit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtCurrency_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtYearlyExpense_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                MessageBox.Show("Control key is blocked", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
