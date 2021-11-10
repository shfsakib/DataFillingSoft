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
        DataTable table = new DataTable();
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
            userName = Properties.Settings.Default.userid;
            txtEmail.Text = Properties.Settings.Default.email;
            txtAttachName.Text = userName + ".zip";
        }
        private DataTable TableData()
        {
            table.Columns.Add("DATA_ID", typeof(int));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("FormNo", typeof(string));
            table.Columns.Add("CompanyCode", typeof(string));
            table.Columns.Add("CompanyName", typeof(string));
            table.Columns.Add("CompanyAddress", typeof(string));
            table.Columns.Add("ZipCode", typeof(string));
            table.Columns.Add("Fax", typeof(string));
            table.Columns.Add("Website", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("ContactNo", typeof(string));
            table.Columns.Add("State", typeof(string));
            table.Columns.Add("Country", typeof(string));
            table.Columns.Add("Headquarter", typeof(string));
            table.Columns.Add("NoOfEmployees", typeof(string));
            table.Columns.Add("Industry", typeof(string));
            table.Columns.Add("BrandAmbassador", typeof(string));
            table.Columns.Add("MediaPartner", typeof(string));
            table.Columns.Add("SocialMedia", typeof(string));
            table.Columns.Add("FrenchiesPartner", typeof(string));
            table.Columns.Add("Investor", typeof(string));
            table.Columns.Add("AdvertisingPartner", typeof(string));
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Services", typeof(string));
            table.Columns.Add("Manager", typeof(string));
            table.Columns.Add("RegistrationDate", typeof(string));
            table.Columns.Add("YearlyRevenue", typeof(string));
            table.Columns.Add("Subclassification", typeof(string));
            table.Columns.Add("Landmark", typeof(string));
            table.Columns.Add("AccoutAudit", typeof(string));
            table.Columns.Add("Currency", typeof(string));
            table.Columns.Add("YearlyExpense", typeof(string));
            table.Columns.Add("USER_ID", typeof(string));

            if (!File.Exists(Path.GetFullPath("form-data.txt")))
            {
                MessageBox.Show("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            string[] allLine = File.ReadAllLines("form-data.txt");
            string[] values;
            for (int i = 0; i < allLine.Length; i++)
            {
                values = allLine[i].ToString().Split(new string[] { "/?" }, StringSplitOptions.None);
                string[] row = new string[values.Length + 1];
                for (int j = 0; j < values.Length + 1; j++)
                {
                    if (j == values.Length)
                    {
                        row[j] = Properties.Settings.Default.userid;
                    }
                    else
                        row[j] = values[j].Trim();
                }

                table.Rows.Add(row);
            }

            return table;

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
                        string rule7 = "User:\r\nMistake in tag\r\nKindly refer rule no 7 for tags";
                        string rule18 = "User:\r\nMistake in tag\r\nKindly refer rule no 18 for website";
                        string rule11 =
                            "User:\r\nkindly refer rule no 11 for spacing after dot (.) and comma (,) in address column.";
                        string ruleSpell = "User:\r\nSpelling Mistake";
                        string ruleMissing = "User:\r\nMissing Data.\r\nKindly refer the form.";
                        if ((j + 1) == 3)
                        {
                            string formNo = dataTable.Rows[i][j].ToString();
                            if (formNo != "" && formNo.Length >= 3)
                            {
                                string startForm = formNo.Substring(0, 3);
                                string endForm = formNo.Substring((formNo.Length - 3), 3);
                                if (startForm != "<B>" || endForm != "<B>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (formNo.Contains("data") || formNo.Contains("not") || formNo.Contains("available") ||
                                         formNo.Contains("data not") ||
                                         formNo.Contains("data not available") || formNo.Contains("Data") || formNo.Contains("Not") || formNo.Contains("Available") ||
                                         formNo.Contains("Data Not"))
                                {
                                    if (formNo != "<B>Data Not Available<B>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }

                        }
                        else if ((j + 1) == 5)
                        {
                            string companyName = dataTable.Rows[i][j].ToString();
                            if (companyName != "" && companyName.Length >= 3)
                            {
                                string startcompanyName = companyName.Substring(0, 3);
                                string endcompanyName =
                                    companyName.Substring(companyName.Length - 3, 3);
                                if (startcompanyName != "<R>" || endcompanyName != "<R>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (companyName.Contains("data") || companyName.Contains("not") || companyName.Contains("available") ||
                                         companyName.Contains("data not") ||
                                         companyName.Contains("data not available") || companyName.Contains("Data") || companyName.Contains("Not") || companyName.Contains("Available") ||
                                         companyName.Contains("Data Not"))
                                {
                                    if (companyName != "<R>Data Not Available<R>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 9)
                        {
                            string website = dataTable.Rows[i][j].ToString();
                            if (website != "" && website.Length >= 6)
                            {
                                string startwebsite = website.Substring(0, 6);
                                string endData = website.Substring(website.Length - 6, 6);

                                if (startwebsite != "<I><U>" || endData.Contains("<") || endData.Contains(">"))
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule18);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule18.Length).Font.Bold = false;
                                }
                                else if (website.Contains("data") || website.Contains("not") || website.Contains("available") ||
                                         website.Contains("data not") ||
                                         website.Contains("data not available") || website.Contains("Data") || website.Contains("Not") || website.Contains("Available") ||
                                         website.Contains("Data Not"))
                                {
                                    if (website != "<I><U>Data Not Available")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 23)
                        {
                            string product = dataTable.Rows[i][j].ToString();
                            if (product != "" && product.Length >= 6)
                            {
                                string startproduct = product.Substring(0, 6);
                                string endData = product.Substring(product.Length - 6, 6);

                                if (startproduct != "<R><B>" || endData.Contains("<") || endData.Contains(">"))
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (product.Contains("data") || product.Contains("not") || product.Contains("available") ||
                                         product.Contains("data not") ||
                                         product.Contains("data not available") || product.Contains("Data") || product.Contains("Not") || product.Contains("Available") ||
                                         product.Contains("Data Not"))
                                {
                                    if (product != "<R><B>Data Not Available")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 25)
                        {
                            string manager = dataTable.Rows[i][j].ToString();
                            if (manager != "" && manager.Length >= 3)
                            {
                                string startmanager = manager.Substring(0, 3);
                                string endmanager = manager.Substring(manager.Length - 3, 3);
                                if (startmanager != "<B>" || endmanager != "<B>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (manager.Contains("data") || manager.Contains("not") || manager.Contains("available") ||
                                         manager.Contains("data not") ||
                                         manager.Contains("data not available") || manager.Contains("Data") || manager.Contains("Not") || manager.Contains("Available") ||
                                         manager.Contains("Data Not"))
                                {
                                    if (manager != "<B>Data Not Available<B>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 14)
                        {
                            string headQ = dataTable.Rows[i][j].ToString();
                            if (headQ != "" && headQ.Length >= 6)
                            {
                                string startheadQ = headQ.Substring(0, 6);
                                string endheadQ = headQ.Substring(headQ.Length - 6, 6);
                                if (startheadQ != "<I><U>" || endheadQ != "<U><I>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (headQ.Contains("data") || headQ.Contains("not") || headQ.Contains("available") ||
                                         headQ.Contains("data not") ||
                                         headQ.Contains("data not available") || headQ.Contains("Data") || headQ.Contains("Not") || headQ.Contains("Available") ||
                                         headQ.Contains("Data Not"))
                                {
                                    if (headQ != "<I><U>Data Not Available<U><I>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 13)
                        {
                            string country = dataTable.Rows[i][j].ToString();
                            if (country != "" && country.Length >= 6)
                            {
                                string startcountry = country.Substring(0, 6);
                                string endcountry = country.Substring(country.Length - 6, 6);
                                if (startcountry != "<R><I>" || endcountry != "<I><R>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (country.Contains("data") || country.Contains("not") || country.Contains("available") ||
                                         country.Contains("data not") ||
                                         country.Contains("data not available") || country.Contains("Data") || country.Contains("Not") || country.Contains("Available") ||
                                         country.Contains("Data Not"))
                                {
                                    if (country != "<R><I>Data Not Available<I><R>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 16)
                        {
                            string industry = dataTable.Rows[i][j].ToString();
                            if (industry != "" && industry.Length >= 6)
                            {
                                string startindustry = industry.Substring(0, 6);
                                string endindustry = industry.Substring(industry.Length - 6, 6);
                                if (startindustry != "<B><U>" || endindustry != "<U><B>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (industry.Contains("data") || industry.Contains("not") || industry.Contains("available") ||
                                         industry.Contains("data not") ||
                                         industry.Contains("data not available") || industry.Contains("Data") || industry.Contains("Not") || industry.Contains("Available") ||
                                         industry.Contains("Data Not"))
                                {
                                    if (industry != "<B><U>Data Not Available<U><B>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 28)
                        {
                            string subClass = dataTable.Rows[i][j].ToString();
                            if (subClass != "" && subClass.Length >= 6)
                            {
                                string startsubClass = subClass.Substring(0, 6);
                                string endsubClass = subClass.Substring(subClass.Length - 6, 6);
                                if (startsubClass != "<I><U>" || endsubClass != "<U><I>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (subClass.Contains("data") || subClass.Contains("not") || subClass.Contains("available") ||
                                         subClass.Contains("data not") ||
                                         subClass.Contains("data not available") || subClass.Contains("Data") || subClass.Contains("Not") || subClass.Contains("Available") ||
                                         subClass.Contains("Data Not"))
                                {
                                    if (subClass != "<I><U>Data Not Available<U><I>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 17)
                        {
                            string brandAmb = dataTable.Rows[i][j].ToString();
                            if (brandAmb != "" && brandAmb.Length >= 3)
                            {
                                string startbrandAmb = brandAmb.Substring(0, 3);
                                string endbrandAmb = brandAmb.Substring(brandAmb.Length - 3, 3);
                                if (startbrandAmb != "<B>" || endbrandAmb != "<B>")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule7.Length).Font.Bold = false;
                                }
                                else if (brandAmb.Contains("data") || brandAmb.Contains("not") || brandAmb.Contains("available") ||
                                         brandAmb.Contains("data not") ||
                                         brandAmb.Contains("data not available") || brandAmb.Contains("Data") || brandAmb.Contains("Not") || brandAmb.Contains("Available") ||
                                         brandAmb.Contains("Data Not"))
                                {
                                    if (brandAmb != "<B>Data Not Available<B>")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else if ((j + 1) == 6)
                        {
                            string comAddress = dataTable.Rows[i][j].ToString();
                            if (comAddress != "" && comAddress.Length >= 3)
                            {
                                if (!comAddress.Contains(",  ") || !comAddress.Contains(".  "))
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(rule11);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, rule11.Length).Font.Bold = false;
                                }
                                else if (comAddress.Contains("data") || comAddress.Contains("not") || comAddress.Contains("available") ||
                                         comAddress.Contains("data not") ||
                                         comAddress.Contains("data not available") || comAddress.Contains("Data") || comAddress.Contains("Not") || comAddress.Contains("Available") ||
                                         comAddress.Contains("Data Not"))
                                {
                                    if (comAddress != "Data Not Available")
                                    {
                                        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                            .Characters(5, ruleSpell.Length).Font.Bold = false;
                                    }
                                }
                            }
                            else
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
                        else
                        {
                            string data = dataTable.Rows[i][j].ToString();
                            if (data.Contains("data") || data.Contains("not") || data.Contains("available") ||
                                data.Contains("data not") ||
                                data.Contains("data not available") || data.Contains("Data") || data.Contains("Not") || data.Contains("Available") ||
                                data.Contains("Data Not"))
                            {
                                if (data != "Data Not Available")
                                {
                                    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                    worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                                    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                        .Characters(5, ruleSpell.Length).Font.Bold = false;
                                }
                            }
                            else if (data == "")
                            {
                                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                                    .Characters(5, ruleMissing.Length).Font.Bold = false;
                            }
                        }
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
                        function.MessageBox("Please close previous generated excel sheet first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                function.MessageBox("Please close previous generated excel sheet first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    zip.Password = "1234";
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
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email is required", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password is required", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!function.IsConnected())
            {
                function.MessageBox("Please connect the internet", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 
                return;
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
                    message.To.Add(new MailAddress("softreview3@gmail.com"));
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
                    //mysmtp.Credentials = new NetworkCredential("softreview3@gmail.com", "public@123");
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

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
