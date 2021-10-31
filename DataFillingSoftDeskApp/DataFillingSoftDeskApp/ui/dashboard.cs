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
            txtFileTaken.Text =
                function.IsExist(
                    $"SELECT FormNo FROM Users WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");
            txtFileDone.Text = function.IsExist($@"SELECT COUNT(FORMNo) FROM FormData WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");

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
                panelProgressBar.Width = 290;
                lblPercentage.Text = "100%";
            }

            txtClientName.Text =
                function.IsExist(
                    $"SELECT FirstName FROM Users WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");
            string date = function.IsExist(
                $"SELECT RegistrationDate FROM Users WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");
            string userId = function.IsExist(
                $"SELECT UserName FROM Users WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}'");
            txtUserIdRegDate.Text = userId + " : " + date;
            if (taken == done)
            {
                btnSave.Enabled = false;
                btnSubmit.Enabled = true;
            }

            lblDate.Text = DateTime.Now.ToString();
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

            if (txtFormNo.Text == "" || txtCompanyCode.Text == "" || txtCompanyName.Text == "" || richAddress.Text == "" || txtZip.Text == "" || txtFax.Text == "" || txtWebsite.Text == "" || txtEmail.Text == "" || txtContactNo.Text == "" || txtState.Text == "")
            {
                function.MessageBox("Please at least fill up group 1 fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (lblFormSl.Text == "" || lblFormSl.Text == null || string.IsNullOrEmpty(lblFormSl.Text))
                {
                    if (imageString == "")
                    {
                        function.MessageBox("Please load demo image first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }
                    bool ans = function.Execute(
                                  $@"INSERT INTO FormData(FormNo,CompanyCode,CompanyName,CompanyAddress,ZipCode,Fax,Website,Email,ContactNo,State,Country,Headquarter,NoOfEmployees,Industry,BrandAmbassador,MediaPartner,SocialMedia,FrenchiesPartner,Investor,AdvertisingPartner,Product,Services,Manager,RegistrationDate,YearlyRevenue,Subclassification,Landmark,AccoutAudit,Currency,YearlyExpense,FileName,AuthenticationKey,EntryTime) 
VALUES('{txtFormNo.Text}','{txtCompanyCode.Text}','{txtCompanyName.Text}','{richAddress.Text}','{txtZip.Text}','{txtFax.Text}','{txtWebsite.Text}','{txtEmail.Text}','{txtContactNo.Text}','{txtState.Text}','{txtCountry.Text}','{txtHeadQuarter.Text}','{txtNoofEmp.Text}','{richIndustry.Text}','{txtBrandAmbs.Text}','{txtMediaPart.Text}','{txtSocialMedia.Text}','{txtFrenPart.Text}','{txtInvestor.Text}','{txtAdvtPart.Text}','{txtProduct.Text}','{txtServices.Text}','{txtManager.Text}','{txtRegDate.Text}','{txtYearlyRev.Text}','{richSubClassification.Text}','{txtLandMark.Text}','{txtAccAudit.Text}','{txtCurrency.Text}','{txtYearlyExpense.Text}','{fileName}','{Properties.Settings.Default.AuthKey}','{function.Date()}')");
                    if (ans)
                    {
                        Clear();
                        LoadData();
                        lblFormSl.Text = "";
                        function.MessageBox("Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    bool ans = function.Execute($@"UPDATE FormData SET FormNo='{txtFormNo.Text}',CompanyCode='{txtCompanyCode.Text}',CompanyName='{txtCompanyName.Text}',
CompanyAddress='{richAddress.Text}',ZipCode='{txtZip.Text}',Fax='{txtFax.Text}',Website='{txtWebsite.Text}',Email='{txtEmail.Text}',ContactNo='{txtContactNo.Text}',State='{txtState.Text}',
Country='{txtCountry.Text}',Headquarter='{txtHeadQuarter.Text}',NoOfEmployees='{txtNoofEmp.Text}',Industry='{richIndustry.Text}',BrandAmbassador='{txtBrandAmbs.Text}',MediaPartner='{txtMediaPart.Text}',
SocialMedia='{txtSocialMedia.Text}',FrenchiesPartner='{txtFrenPart.Text}',Investor='{txtInvestor.Text}',AdvertisingPartner='{txtAdvtPart.Text}',Product='{txtProduct.Text}',Services='{txtServices.Text}',
Manager='{txtManager.Text}',RegistrationDate='{txtRegDate.Text}',YearlyRevenue='{txtYearlyRev.Text}',Subclassification='{richSubClassification.Text}',Landmark='{txtLandMark.Text}',AccoutAudit='{txtAccAudit.Text}',
Currency='{txtCurrency.Text}',YearlyExpense='{txtYearlyExpense.Text}' WHERE FormSerial='{lblFormSl.Text}'");
                    if (ans)
                    {
                        Clear();
                        LoadData();
                        lblFormSl.Text = "";
                        function.MessageBox("Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }

        }

        private void FirstForm()
        {
            lblFormSl.Text = function.IsExist(
                $"SELECT TOP 1 FormSerial FROM FormData WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");
            GetFieldData(lblFormSl.Text);
        }
        private void LastForm()
        {
            lblFormSl.Text = function.IsExist(
                $"SELECT TOP 1 FormSerial FROM FormData WHERE AuthenticationKey='{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial DESC");
            GetFieldData(lblFormSl.Text);
        }
        private void btnNextForm_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            if (nextClick == 0)
            {
                FirstForm();
                nextClick = 1;
            }
            else
            {
                string nextId = function.IsExist(
                    $"SELECT TOP 1 FormSerial FROM FormData WHERE FormSerial>'{lblFormSl.Text}' AND AuthenticationKey='{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");
                if (nextId != "")
                {
                    lblFormSl.Text = nextId;
                    GetFieldData(lblFormSl.Text);
                }
                else
                {
                    LastForm();
                }

            }

        }

        private void btnPrevForm_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            string prevId = function.IsExist(
                $"SELECT TOP 1 FormSerial FROM FormData WHERE FormSerial<'{lblFormSl.Text}' AND AuthenticationKey='{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial DESC");
            if (prevId != "")
            {
                lblFormSl.Text = prevId;
                GetFieldData(lblFormSl.Text);
            }
            else
            {
                FirstForm();
            }

        }

        private void GetFieldData(string serial)
        {
            txtFormNo.Text = function.IsExist(
                $"SELECT TOP 1 FormNo FROM FormData WHERE FormSerial='{serial}'");
            txtCompanyCode.Text = function.IsExist($"SELECT TOP 1 CompanyCode FROM FormData WHERE FormSerial='{serial}'");
            txtCompanyName.Text = function.IsExist($"SELECT TOP 1 CompanyName FROM FormData WHERE FormSerial='{serial}'");
            richAddress.Text = function.IsExist($"SELECT TOP 1 CompanyAddress FROM FormData WHERE FormSerial='{serial}'");
            txtZip.Text = function.IsExist($"SELECT TOP 1 ZipCode FROM FormData WHERE FormSerial='{serial}'");
            txtFax.Text = function.IsExist($"SELECT TOP 1 Fax FROM FormData WHERE FormSerial='{serial}'");
            txtWebsite.Text = function.IsExist($"SELECT TOP 1 Website FROM FormData WHERE FormSerial='{serial}'");
            txtEmail.Text = function.IsExist($"SELECT TOP 1 Email FROM FormData WHERE FormSerial='{serial}'");
            txtContactNo.Text = function.IsExist($"SELECT TOP 1 ContactNo FROM FormData WHERE FormSerial='{serial}'");
            txtState.Text = function.IsExist($"SELECT TOP 1 State FROM FormData WHERE FormSerial='{serial}'");
            txtCountry.Text = function.IsExist($"SELECT TOP 1 Country FROM FormData WHERE FormSerial='{serial}'");
            txtHeadQuarter.Text = function.IsExist($"SELECT TOP 1 Headquarter FROM FormData WHERE FormSerial='{serial}'");
            txtNoofEmp.Text = function.IsExist($"SELECT TOP 1 NoOfEmployees FROM FormData WHERE FormSerial='{serial}'");
            richIndustry.Text = function.IsExist($"SELECT TOP 1 Industry FROM FormData WHERE FormSerial='{serial}'");
            txtBrandAmbs.Text = function.IsExist($"SELECT TOP 1 BrandAmbassador FROM FormData WHERE FormSerial='{serial}'");
            txtMediaPart.Text = function.IsExist($"SELECT TOP 1 MediaPartner FROM FormData WHERE FormSerial='{serial}'");
            txtSocialMedia.Text = function.IsExist($"SELECT TOP 1 SocialMedia FROM FormData WHERE FormSerial='{serial}'");
            txtFrenPart.Text = function.IsExist($"SELECT TOP 1 FrenchiesPartner FROM FormData WHERE FormSerial='{serial}'");
            txtInvestor.Text = function.IsExist($"SELECT TOP 1 Investor FROM FormData WHERE FormSerial='{serial}'");
            txtAdvtPart.Text = function.IsExist($"SELECT TOP 1 AdvertisingPartner FROM FormData WHERE FormSerial='{serial}'");
            txtProduct.Text = function.IsExist($"SELECT TOP 1 Product FROM FormData WHERE FormSerial='{serial}'");
            txtServices.Text = function.IsExist($"SELECT TOP 1 Services FROM FormData WHERE FormSerial='{serial}'");
            txtManager.Text = function.IsExist($"SELECT TOP 1 Manager FROM FormData WHERE FormSerial='{serial}'");
            txtRegDate.Text = function.IsExist($"SELECT TOP 1 RegistrationDate FROM FormData WHERE FormSerial='{serial}'");
            txtYearlyRev.Text = function.IsExist($"SELECT TOP 1 YearlyRevenue FROM FormData WHERE FormSerial='{serial}'");
            richSubClassification.Text = function.IsExist($"SELECT TOP 1 Subclassification FROM FormData WHERE FormSerial='{serial}'");
            txtLandMark.Text = function.IsExist($"SELECT TOP 1 Landmark FROM FormData WHERE FormSerial='{serial}'");
            txtAccAudit.Text = function.IsExist($"SELECT TOP 1 AccoutAudit FROM FormData WHERE FormSerial='{serial}'");
            txtCurrency.Text = function.IsExist($"SELECT TOP 1 Currency FROM FormData WHERE FormSerial='{serial}'");
            txtYearlyExpense.Text = function.IsExist($"SELECT TOP 1 YearlyExpense FROM FormData WHERE FormSerial='{serial}'");


        }

        private void txtNoofEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
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
    }
}
