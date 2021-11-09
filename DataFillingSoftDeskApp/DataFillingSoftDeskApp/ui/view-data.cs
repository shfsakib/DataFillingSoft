using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFillingSoftDeskApp.Class;
using Ionic.Zip;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataFillingSoftDeskApp.ui
{
    public partial class view_data : Form
    {
        private Function function;
        public view_data()
        {
            InitializeComponent();
            btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            btnClose.BackColorChanged += (s, e) =>
            {
                btnClose.FlatAppearance.MouseOverBackColor = btnClose.BackColor;
            };
            function = Function.GetInstance();
        }

        private void view_data_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //function.LoadGrid(dataGridView1, $@"SELECT  COALESCE(NULLIF(FormSerial,''), 'Data Not Available') FormSerial,COALESCE(NULLIF(FileName,''), 'Data Not Available') FileName,COALESCE(NULLIF(FormNo,''), 'Data Not Available') FormNo,COALESCE(NULLIF(CompanyCode,''), 'Data Not Available') CompanyCode,COALESCE(NULLIF(CompanyName,''), 'Data Not Available') CompanyName,COALESCE(NULLIF(CompanyAddress,''), 'Data Not Available') CompanyAddress,COALESCE(NULLIF(ZipCode,''), 'Data Not Available') ZipCode,COALESCE(NULLIF(Fax,''), 'Data Not Available') Fax,COALESCE(NULLIF(Website,''), 'Data Not Available') Website,COALESCE(NULLIF(Email,''), 'Data Not Available') Email,COALESCE(NULLIF(ContactNo,''), 'Data Not Available') ContactNo,COALESCE(NULLIF(State,''), 'Data Not Available') State,COALESCE(NULLIF(Country,''), 'Data Not Available') Country,COALESCE(NULLIF(NoOfEmployees,''), 'Data Not Available') Headquarter,COALESCE(NULLIF(NoOfEmployees,''), 'Data Not Available')  NoOfEmployees,COALESCE(NULLIF(Industry,''), 'Data Not Available') Industry,COALESCE(NULLIF(BrandAmbassador,''), 'Data Not Available') BrandAmbassador,COALESCE(NULLIF(MediaPartner,''), 'Data Not Available') MediaPartner,COALESCE(NULLIF(SocialMedia,''), 'Data Not Available') SocialMedia, 
            //COALESCE(NULLIF(FrenchiesPartner,''), 'Data Not Available') FrenchiesPartner,COALESCE(NULLIF(Investor,''), 'Data Not Available') Investor,COALESCE(NULLIF(AdvertisingPartner,''), 'Data Not Available') AdvertisingPartner,COALESCE(NULLIF(Product,''), 'Data Not Available') Product,COALESCE(NULLIF(Services,''), 'Data Not Available') Services,COALESCE(NULLIF(Manager,''), 'Data Not Available') Manager,COALESCE(NULLIF(RegistrationDate,''), 'Data Not Available') RegistrationDate,COALESCE(NULLIF(YearlyRevenue,''), 'Data Not Available') YearlyRevenue,COALESCE(NULLIF(Subclassification,''), 'Data Not Available') Subclassification,COALESCE(NULLIF(Landmark,''), 'Data Not Available') Landmark,COALESCE(NULLIF(AccoutAudit,''), 'Data Not Available') AccoutAudit,COALESCE(NULLIF(Currency,''), 'Data Not Available') Currency,COALESCE(NULLIF(YearlyExpense,''), 'Data Not Available') YearlyExpense
            //    FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");
            function.LoadGrid(dataGridView1, $@"SELECT        FormSerial, FileName, FormNo, CompanyCode, CompanyName, CompanyAddress, ZipCode, Fax, Website, Email, ContactNo, State, Country, Headquarter, NoOfEmployees, Industry, BrandAmbassador, MediaPartner, 
                         SocialMedia, FrenchiesPartner, Investor, AdvertisingPartner, Product, Services, Manager, RegistrationDate, YearlyRevenue, Subclassification, Landmark, AccoutAudit, Currency, YearlyExpense
FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private DataTable TableData()
        {
            return function.LoadTable($@"SELECT        FormSerial, FileName, FormNo, CompanyCode, CompanyName, CompanyAddress, ZipCode, Fax, Website, Email, ContactNo, State, Country, Headquarter, NoOfEmployees, Industry, BrandAmbassador, MediaPartner, 
                         SocialMedia, FrenchiesPartner, Investor, AdvertisingPartner, Product, Services, Manager, RegistrationDate, YearlyRevenue, Subclassification, Landmark, AccoutAudit, Currency, YearlyExpense
FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");

        }

        private void ExportToExcel(DataTable dataTable, string filePath)
        {
            try
            {
                if (dataTable == null || dataTable.Columns.Count == 0)
                {
                    function.MessageBox("Null or empty data table", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
                        //string ruleMissing= "User:\r\nMissing Data.\r\nKindly refer the form.";
                        //if ((j + 1) == 3)
                        //{
                        //    string formNo = dataTable.Rows[i][j].ToString();
                        //    if (formNo != "")
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
                        //        else if (formNo.Contains("data") || formNo.Contains("not") || formNo.Contains("available") ||
                        //                 formNo.Contains("data not") ||
                        //                 formNo.Contains("data not available") || formNo.Contains("Data") || formNo.Contains("Not") || formNo.Contains("Available") ||
                        //                 formNo.Contains("Data Not"))
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
                        //else if ((j + 1) == 5)
                        //{
                        //    string companyName = dataTable.Rows[i][j].ToString();
                        //    if (companyName != "")
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
                        //        else if (companyName.Contains("data") || companyName.Contains("not") || companyName.Contains("available") ||
                        //                 companyName.Contains("data not") ||
                        //                 companyName.Contains("data not available") || companyName.Contains("Data") || companyName.Contains("Not") || companyName.Contains("Available") ||
                        //                 companyName.Contains("Data Not"))
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
                        //else if ((j + 1) == 9)
                        //{
                        //    string website = dataTable.Rows[i][j].ToString();
                        //    if (website != "")
                        //    {
                        //        string startwebsite = website.Substring(0, 6);
                        //        string endData = website.Substring(website.Length-6, 6);

                        //        if (startwebsite != "<I><U>" || endData.Contains("<") || endData.Contains(">"))
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule18);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule18.Length).Font.Bold = false;
                        //        }
                        //        else if (website.Contains("data") || website.Contains("not") || website.Contains("available") ||
                        //                 website.Contains("data not") ||
                        //                 website.Contains("data not available") || website.Contains("Data") || website.Contains("Not") || website.Contains("Available") ||
                        //                 website.Contains("Data Not"))
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
                        //else if ((j + 1) == 23)
                        //{
                        //    string product = dataTable.Rows[i][j].ToString();
                        //    if (product != "")
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
                        //        else if (product.Contains("data") || product.Contains("not") || product.Contains("available") ||
                        //                 product.Contains("data not") ||
                        //                 product.Contains("data not available") || product.Contains("Data") || product.Contains("Not") || product.Contains("Available") ||
                        //                 product.Contains("Data Not"))
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
                        //    if (manager != "")
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
                        //        else if (manager.Contains("data") || manager.Contains("not") || manager.Contains("available") ||
                        //                 manager.Contains("data not") ||
                        //                 manager.Contains("data not available") || manager.Contains("Data") || manager.Contains("Not") || manager.Contains("Available") ||
                        //                 manager.Contains("Data Not"))
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
                        //else if ((j + 1) == 14)
                        //{
                        //    string headQ = dataTable.Rows[i][j].ToString();
                        //    if (headQ != "")
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
                        //        else if (headQ.Contains("data") || headQ.Contains("not") || headQ.Contains("available") ||
                        //                 headQ.Contains("data not") ||
                        //                 headQ.Contains("data not available") || headQ.Contains("Data") || headQ.Contains("Not") || headQ.Contains("Available") ||
                        //                 headQ.Contains("Data Not"))
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
                        //else if ((j + 1) == 13)
                        //{
                        //    string country = dataTable.Rows[i][j].ToString();
                        //    if (country != "")
                        //    {
                        //        string startcountry = country.Substring(0, 6);
                        //        string endcountry = country.Substring(country.Length - 6,6);
                        //        if (startcountry != "<R><I>" || endcountry != "<I><R>")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule7);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule7.Length).Font.Bold = false;
                        //        }
                        //        else if (country.Contains("data") || country.Contains("not") || country.Contains("available") ||
                        //                 country.Contains("data not") ||
                        //                 country.Contains("data not available") || country.Contains("Data") || country.Contains("Not") || country.Contains("Available") ||
                        //                 country.Contains("Data Not"))
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
                        //else if ((j + 1) == 16)
                        //{
                        //    string industry = dataTable.Rows[i][j].ToString();
                        //    if (industry != "")
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
                        //        else if (industry.Contains("data") || industry.Contains("not") || industry.Contains("available") ||
                        //                 industry.Contains("data not") ||
                        //                 industry.Contains("data not available") || industry.Contains("Data") || industry.Contains("Not") || industry.Contains("Available") ||
                        //                 industry.Contains("Data Not"))
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
                        //else if ((j + 1) == 28)
                        //{
                        //    string subClass = dataTable.Rows[i][j].ToString();
                        //    if (subClass != "")
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
                        //        else if (subClass.Contains("data") || subClass.Contains("not") || subClass.Contains("available") ||
                        //                 subClass.Contains("data not") ||
                        //                 subClass.Contains("data not available") || subClass.Contains("Data") || subClass.Contains("Not") || subClass.Contains("Available") ||
                        //                 subClass.Contains("Data Not"))
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
                        //else if ((j + 1) == 17)
                        //{
                        //    string brandAmb = dataTable.Rows[i][j].ToString();
                        //    if (brandAmb != "")
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
                        //        else if (brandAmb.Contains("data") || brandAmb.Contains("not") || brandAmb.Contains("available") ||
                        //                 brandAmb.Contains("data not") ||
                        //                 brandAmb.Contains("data not available") || brandAmb.Contains("Data") || brandAmb.Contains("Not") || brandAmb.Contains("Available") ||
                        //                 brandAmb.Contains("Data Not"))
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
                        //else if ((j + 1) == 6)
                        //{
                        //    string comAddress = dataTable.Rows[i][j].ToString();
                        //    if (comAddress != "")
                        //    {
                        //        if (!comAddress.Contains(",  ") || !comAddress.Contains(".  "))
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(rule11);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, rule11.Length).Font.Bold = false;
                        //        }
                        //        else if (comAddress.Contains("data") || comAddress.Contains("not") || comAddress.Contains("available") ||
                        //                 comAddress.Contains("data not") ||
                        //                 comAddress.Contains("data not available") || comAddress.Contains("Data") || comAddress.Contains("Not") || comAddress.Contains("Available") ||
                        //                 comAddress.Contains("Data Not"))
                        //        {
                        //            if (comAddress != "Data Not Available")
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
                        //else
                        //{
                        //    string data = dataTable.Rows[i][j].ToString();
                        //    if (data.Contains("data") || data.Contains("not") || data.Contains("available") ||
                        //        data.Contains("data not") ||
                        //        data.Contains("data not available") || data.Contains("Data") || data.Contains("Not") || data.Contains("Available") ||
                        //        data.Contains("Data Not"))
                        //    {
                        //        if (data != "Data Not Available")
                        //        {
                        //            worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //            worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleSpell);
                        //            worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //                .Characters(5, ruleSpell.Length).Font.Bold = false;
                        //        }
                        //    }else if (data=="")
                        //    {
                        //        worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                        //        worksheet.Cells[(i + 2), (j + 1)].AddComment(ruleMissing);
                        //        worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame
                        //            .Characters(5, ruleMissing.Length).Font.Bold = false;
                        //    }
                        //}
                    }

                    //if (dataTable.Rows[i][j].ToString() == "544644444")
                    //{
                    //    worksheet.Cells[(i + 2), (j + 1)].Interior.Color = Color.Yellow;
                    //    worksheet.Cells[(i + 2), (j + 1)].AddComment("Title"+"\r\n"+"Hello");
                    //    worksheet.Cells[(i + 2), (j + 1)].Comment.Shape.TextFrame.Characters(5, 10).Font.Bold = false;
                    //}
                }

                //check filepath
                if (filePath != null || filePath != "")
                {
                    try
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                        @"\FormFolder\FormData.xlsx"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                  @"\FormFolder\FormData.xlsx");
                        }

                        Directory.CreateDirectory(
                                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FormFolder");
                        worksheet.SaveAs(filePath, Type.Missing);
                        excelApp.Quit();
                        DialogResult dialogResult = MessageBox.Show("Exported to project folder successfully to Documents FormData folder", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.OK)
                        {
                            this.Hide();
                        }
                    }
                    catch (Exception ex)
                    {
                        DialogResult dialogResult = MessageBox.Show("Excel file can\'t be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (dialogResult == DialogResult.OK)
                        {
                            this.Hide();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                function.MessageBox("Please close previous generated excel sheet first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ExportToExcel(TableData(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FormFolder\FormData.xlsx");
            //Thread.Sleep(TimeSpan.FromSeconds(5));
            //SaveZip();
        }


    }
}
