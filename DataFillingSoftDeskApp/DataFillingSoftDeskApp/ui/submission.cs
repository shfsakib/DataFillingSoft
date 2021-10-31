using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.Class;
using Ionic.Zip;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataFillingSoftDeskApp.ui
{
    public partial class submission : Form
    {
        private Function function;
        private string userName = "";
        public submission()
        {
            InitializeComponent();
            btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            btnClose.BackColorChanged += (s, e) =>
            {
                btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            };
            function = Function.GetInstance();

        }

        private void submission_Load(object sender, EventArgs e)
        {
            LoadData();
            txtPassword.Focus();
        }
        private void LoadData()
        {
            userName = function.IsExist(
                $@"SELECT UserName FROM Users WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}'");
            txtEmail.Text =
                function.IsExist(
                    $@"SELECT Email FROM Users WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}'");
            txtAttachName.Text = userName + ".zip";

        }
        private DataTable TableData()
        {
            return function.LoadTable($@"SELECT        FormSerial, FormNo, CompanyCode, CompanyName, CompanyAddress, ZipCode, Fax, Website, Email, ContactNo, State, Country, Headquarter, NoOfEmployees, Industry, BrandAmbassador, MediaPartner, SocialMedia, 
            FrenchiesPartner, Investor, AdvertisingPartner, Product, Services, Manager, RegistrationDate, YearlyRevenue, Subclassification, Landmark, AccoutAudit, Currency, YearlyExpense, FileName
                FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");

        }
        private bool ExportToExcel(DataTable dataTable, string filePath)
        {
            try
            {
                if (dataTable == null || dataTable.Columns.Count == 0)
                {
                    function.MessageBox("Null or empty data table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Load Excel
                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add();
                //single worksheet
                Excel.Worksheet worksheet = excelApp.ActiveSheet;
                //column headings
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, (i + 1)] = dataTable.Columns[i].ColumnName;
                }
                //rows
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cells[(i + 2), (j + 1)] = dataTable.Rows[i][j];
                    }

                }
                //check filepath
                if (filePath != null && filePath != "")
                {
                    try
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                        @"\FormZipFolder\" + userName + ".xlsx"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                        @"\FormZipFolder\" + userName + ".xlsx");
                        }

                        Directory.CreateDirectory(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FormZipFolder");
                        worksheet.SaveAs(filePath, Type.Missing);
                        excelApp.Quit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void SaveZip()
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + userName + ".zip"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                @"\" + userName + ".zip");
                }
                //Creates a ZipFile object that will ultimately be saved
                using (ZipFile zip = new ZipFile())
                {

                    zip.Password = "123";
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FormZipFolder";
                    zip.AddDirectory(fileName);
                    zip.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + userName + ".zip");

                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to cancel submission?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                dashboard dashboard = new dashboard();
                dashboard.Show();
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Are you sure want to cancel submission?", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                btnSend.Enabled = false;
                lblwait.Visible = true;
                ExportToExcel(TableData(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FormZipFolder\" + userName + ".xlsx");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                SaveZip();
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(txtEmail.Text);
                    message.To.Add(new MailAddress("shfkte@gmail.com"));
                    message.Subject = txtSubject.Text;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = txtMessage.Text;
                    //attachment
                    System.Net.Mail.Attachment attachment;
                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" +
                                      userName + ".zip";
                    attachment = new System.Net.Mail.Attachment(filePath);
                    message.Attachments.Add(attachment);
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(txtEmail.Text, txtPassword.Text);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    //mail to my email
                    MailMessage mymessage = new MailMessage();
                    SmtpClient mysmtp = new SmtpClient();
                    mymessage.From = new MailAddress(txtEmail.Text);
                    mymessage.To.Add(new MailAddress(txtEmail.Text));
                    mymessage.Subject = txtSubject.Text;
                    mymessage.IsBodyHtml = true; //to make mymessage body as html  
                    mymessage.Body = txtMessage.Text;
                    //attachment
                    System.Net.Mail.Attachment myattachment;
                    myattachment = new System.Net.Mail.Attachment(filePath);
                    mymessage.Attachments.Add(myattachment);
                    mysmtp.Port = 587;
                    mysmtp.Host = "smtp.gmail.com"; //for gmail host  
                    mysmtp.EnableSsl = true;
                    mysmtp.UseDefaultCredentials = false;
                    mysmtp.Credentials = new NetworkCredential(txtEmail.Text, txtPassword.Text);
                    mysmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mysmtp.Send(mymessage);
                    btnSend.Enabled = true;
                    lblwait.Visible = false;

                    DialogResult dialogResult = MessageBox.Show("Mail sent successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        this.Hide();
                        log_in login = new log_in();
                        login.Show();
                    }
                }
                catch (Exception ex)
                {
                    btnSend.Enabled = true;
                    lblwait.Visible = false;
                    MessageBox.Show("Please Kindly Make Sure to Turn On Less Secure App in Your Gmail Account Setting Or Check Your Internet Settings.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
