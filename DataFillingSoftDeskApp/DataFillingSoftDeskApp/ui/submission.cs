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
using System.Text.RegularExpressions;
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
        DataTable noValidTable = new DataTable();
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

        }
        private void LoadData()
        {
            userName = Properties.Settings.Default.userid.Trim();

            txtAttachName.Text = "" + userName + ".zip";
            txtSubject.Text = Properties.Settings.Default.userid.Trim();

        }

        private DataTable TableData()
        {
            table.Columns.Add("Data_Id", typeof(string));
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
                    else if (j == 0)
                    {
                        row[j] = (i + 1).ToString();
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
                        //string rule7 = "User:\r\nMistake in tag\r\nKindly refer rule no 7 for tags";
                        //string rule18 = "User:\r\nMistake in tag\r\nKindly refer rule no 18 for website";
                        //string rule11 =
                        //    "User:\r\nkindly refer rule no 11 for spacing after dot (.) and comma (,) in address column.";
                        //string ruleSpell = "User:\r\nSpelling Mistake";
                        //string emailError = "User:\r\nEamil column must be lower case";
                        //string contactError = "User:\r\nSpecial character, space and characters are not allowed";
                        //string ruleMissing = "User:\r\nMissing Data.\r\nKindly refer the form.";

                        ////form no
                        //if ((j + 1) == 3)
                        //{
                        //    string formNo = dataTable.Rows[i][j].ToString();
                        //    if (formNo != "" && formNo.Length >= 3)
                        //    {
                        //        string startForm = formNo.Substring(0, 3);
                        //        string endForm = formNo.Substring((formNo.Length - 3), 3);
                        //        if (startForm != "<B>" || endForm != "<B>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(formNo))
                        //        {
                        //            if (formNo != "<B>Data Not Available<B>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }

                        //}
                        ////company name
                        //else if ((j + 1) == 5)
                        //{
                        //    string companyName = dataTable.Rows[i][j].ToString();
                        //    if (companyName != "" && companyName.Length >= 3)
                        //    {
                        //        string startcompanyName = companyName.Substring(0, 3);
                        //        string endcompanyName =
                        //            companyName.Substring(companyName.Length - 3, 3);
                        //        if (startcompanyName != "<R>" || endcompanyName != "<R>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(companyName))
                        //        {
                        //            if (companyName != "<R>Data Not Available<R>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //////company address
                        ////else if ((j + 1) == 6)
                        ////{
                        ////    string comAddress = dataTable.Rows[i][j].ToString();

                        ////    if (comAddress != "")
                        ////    {
                        ////        string lastText = comAddress.Substring(comAddress.Length - 1, 1);
                        ////        string checkingText = comAddress.Substring(0, comAddress.Length - 1);
                        ////        if (lastText.Contains(",") || lastText.Contains("."))
                        ////        {
                        ////            if (!checkingText.Contains(",  ") || !checkingText.Contains(".  "))
                        ////            {
                        ////                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        ////                worksheet.Cells[(i + 2), (j + 1)].AddComment(rule11);
                        ////                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        ////                    .Characters(5, rule11.Length).Font.Bold = false;
                        ////            }
                        ////        }
                        ////        else if (!comAddress.Contains(",  ") || !comAddress.Contains(".  "))
                        ////        {
                        ////            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        ////            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule11);
                        ////            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        ////                .Characters(5, rule11.Length).Font.Bold = false;
                        ////        }
                        ////        else if (StringValidationCheck(comAddress))
                        ////        {
                        ////            if (comAddress != "Data Not Available")
                        ////            {
                        ////                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        ////                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        ////                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        ////                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        ////            }
                        ////        }
                        ////    }
                        ////    else
                        ////    {
                        ////        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        ////        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        ////        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        ////            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        ////    }
                        ////}
                        ////website
                        //else if ((j + 1) == 9)
                        //{
                        //    string website = dataTable.Rows[i][j].ToString();
                        //    if (website != "" && website.Length >= 6)
                        //    {
                        //        string startwebsite = website.Substring(0, 6);
                        //        string endData = website.Substring(website.Length - 6, 6);

                        //        if (startwebsite != "<I><U>" || endData.Contains("<") || endData.Contains(">"))
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule18);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule18.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(website))
                        //        {
                        //            if (website != "<I><U>Data Not Available")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        ////email
                        //else if ((j + 1) == 10)
                        //{
                        //    string email = dataTable.Rows[i][j].ToString();
                        //    if (email != "")
                        //    {
                        //        if (email.Any(char.IsUpper))
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(emailError);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, emailError.Length).Font.Bold = false;
                        //        }
                        //        if (StringValidationCheck(email))
                        //        {
                        //            if (email != "Data Not Available")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        ////contact number
                        //else if ((j + 1) == 11)
                        //{
                        //    string contact = dataTable.Rows[i][j].ToString();
                        //    if (contact != "")
                        //    {
                        //        if (!System.Text.RegularExpressions.Regex.IsMatch(contact, "^[0-9]*$"))
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(contactError);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, contactError.Length).Font.Bold = false;
                        //        }
                        //        if (StringValidationCheck(contact))
                        //        {
                        //            if (contact != "Data Not Available")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 13)
                        //{
                        //    string country = dataTable.Rows[i][j].ToString();
                        //    if (country != "" && country.Length >= 6)
                        //    {
                        //        string startcountry = country.Substring(0, 6);
                        //        string endcountry = country.Substring(country.Length - 6, 6);
                        //        if (startcountry != "<R><I>" || endcountry != "<I><R>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(country))
                        //        {
                        //            if (country != "<R><I>Data Not Available<I><R>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 14)
                        //{
                        //    string headQ = dataTable.Rows[i][j].ToString();
                        //    if (headQ != "" && headQ.Length >= 6)
                        //    {
                        //        string startheadQ = headQ.Substring(0, 6);
                        //        string endheadQ = headQ.Substring(headQ.Length - 6, 6);
                        //        if (startheadQ != "<I><U>" || endheadQ != "<U><I>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(headQ))
                        //        {
                        //            if (headQ != "<I><U>Data Not Available<U><I>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 16)
                        //{
                        //    string industry = dataTable.Rows[i][j].ToString();
                        //    if (industry != "" && industry.Length >= 6)
                        //    {
                        //        string startindustry = industry.Substring(0, 6);
                        //        string endindustry = industry.Substring(industry.Length - 6, 6);
                        //        if (startindustry != "<B><U>" || endindustry != "<U><B>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(industry))
                        //        {
                        //            if (industry != "<B><U>Data Not Available<U><B>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 17)
                        //{
                        //    string brandAmb = dataTable.Rows[i][j].ToString();
                        //    if (brandAmb != "" && brandAmb.Length >= 3)
                        //    {
                        //        string startbrandAmb = brandAmb.Substring(0, 3);
                        //        string endbrandAmb = brandAmb.Substring(brandAmb.Length - 3, 3);
                        //        if (startbrandAmb != "<B>" || endbrandAmb != "<B>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(brandAmb))
                        //        {
                        //            if (brandAmb != "<B>Data Not Available<B>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 23)
                        //{
                        //    string product = dataTable.Rows[i][j].ToString();
                        //    if (product != "" && product.Length >= 6)
                        //    {
                        //        string startproduct = product.Substring(0, 6);
                        //        string endData = product.Substring(product.Length - 6, 6);

                        //        if (startproduct != "<R><B>" || endData.Contains("<") || endData.Contains(">"))
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(product))
                        //        {
                        //            if (product != "<R><B>Data Not Available")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 25)
                        //{
                        //    string manager = dataTable.Rows[i][j].ToString();
                        //    if (manager != "" && manager.Length >= 3)
                        //    {
                        //        string startmanager = manager.Substring(0, 3);
                        //        string endmanager = manager.Substring(manager.Length - 3, 3);
                        //        if (startmanager != "<B>" || endmanager != "<B>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(manager))
                        //        {
                        //            if (manager != "<B>Data Not Available<B>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}

                        //else if ((j + 1) == 28)
                        //{
                        //    string subClass = dataTable.Rows[i][j].ToString();
                        //    if (subClass != "" && subClass.Length >= 6)
                        //    {
                        //        string startsubClass = subClass.Substring(0, 6);
                        //        string endsubClass = subClass.Substring(subClass.Length - 6, 6);
                        //        if (startsubClass != "<I><U>" || endsubClass != "<U><I>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (StringValidationCheck(subClass))
                        //        {
                        //            if (subClass != "<I><U>Data Not Available<U><I>")
                        //            {
                        //                worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //                worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //                worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                    .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                        //else if ((j + 1) == 33)
                        //{
                        //}
                        //else
                        //{
                        //    string data = dataTable.Rows[i][j].ToString();
                        //    if (data == "")
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //    else if (!FirstLetterCapitalCheck(dataTable.Rows[i][j].ToString()))
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //    }
                        //    else if (StringValidationCheck(data))
                        //    {
                        //        if (data != "Data Not Available")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //no condition
                        //    }
                        //}
                    }

                }
                //check filepath
                if (filePath != null && filePath != "")
                {
                    try
                    {
                        if (File.Exists(Path.GetFullPath(userName + ".xlsx")))
                        {
                            File.Delete(Path.GetFullPath(userName + ".xlsx"));
                        }

                        worksheet.SaveAs(filePath, Type.Missing);
                        excelApp.Quit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // function.MessageBox("Please close previous generated excel sheet first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        function.MessageBox("1st error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //function.MessageBox("Please close previous generated excel sheet first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                function.MessageBox("2nd error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool StringValidationCheck(string data)
        {
            bool ans = false;

            if (data.ToUpper().Contains("DATA NOT AVAILABLE"))
            {
                ans = true;
            }
            return ans;
        }

        private bool FirstLetterCapitalCheck(string data)
        {
            bool ans = false;
            if (data.Length > 0)
            {
                string dataSubstring = data.Substring(0, 1);
                string capitalLetter = char.ToUpper(data[0]).ToString();
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regexItem.IsMatch(dataSubstring))
                {
                    ans = false;
                }
                else if (dataSubstring == capitalLetter)
                {
                    ans = true;
                }
            }

            return ans;
        }
        private void SaveZip()
        {
            try
            {
                if (File.Exists(Path.GetFullPath(userName + ".zip")))
                {
                    File.Delete(Path.GetFullPath(userName + ".zip"));
                }

                //Creates a ZipFile object that will ultimately be saved
                using (ZipFile zip = new ZipFile())
                {

                    zip.Password = "transonic@USA1201";
                    string fileName = Path.GetFullPath(userName + ".xlsx");
                    zip.AddItem(fileName, "");
                    zip.Save(Path.GetFullPath(userName + ".zip"));

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
            if (!function.IsConnected())
            {
                function.MessageBox("Please connect the internet", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                if (Properties.Settings.Default.submit == "false")
                {
                    MessageBox.Show("You\'ve already submitted your project", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    btnSend.Enabled = false;
                }
                btnSend.Enabled = false;
                lblwait.Visible = true;

                bool ans = ExportToExcel(TableData(),
                    Path.GetFullPath(userName + ".xlsx"));
                Thread.Sleep(TimeSpan.FromSeconds(5));
                if (ans)
                {
                    SaveZip();
                    try
                    {
                        //gmail mailing system

                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        //message.From = new MailAddress("submission.transonic@gmail.com");
                        message.From = new MailAddress("submission.transonictec@gmail.com");
                        //to mail
                        //message.To.Add(new MailAddress("submission.transonic@gmail.com"));
                        message.To.Add(new MailAddress("submission.transonictec@gmail.com"));

                        message.Subject = txtSubject.Text;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = txtMessage.Text;
                        //attachment
                        System.Net.Mail.Attachment attachment;
                        string filePath = Path.GetFullPath(userName + ".zip");
                        attachment = new System.Net.Mail.Attachment(filePath);
                        message.Attachments.Add(attachment);
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com"; //for gmail host 
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        //smtp.Credentials = new NetworkCredential("submission.transonic@gmail.com", "Ayaat@786786");
                        smtp.Credentials = new NetworkCredential("submission.transonictec@gmail.com", "Ayaat@7867861201");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);


                        btnSend.Enabled = true;
                        lblwait.Visible = false;

                        DialogResult dialogResult = MessageBox.Show("Mail sent successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.OK)
                        {
                            Properties.Settings.Default.submit = "false";
                            Properties.Settings.Default.Save();
                            try
                            {
                                if (File.Exists(Path.GetFullPath(userName + ".xlsx")))
                                {
                                    File.Delete(Path.GetFullPath(userName + ".xlsx"));
                                }
                            }
                            catch (Exception)
                            {

                            }
                            this.Hide();
                            log_in login = new log_in();
                            login.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSend.Enabled = true;
                        lblwait.Visible = false;
                        DialogResult dialogResult = MessageBox.Show("Please Kindly Make Sure to Turn On Less Secure App in Your Gmail Account Setting Or Check Your Internet Settings.",
                            "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.OK)
                        {

                            this.Hide();
                            dashboard dashboard = new dashboard();
                            dashboard.Show();
                        }
                    }
                }
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
