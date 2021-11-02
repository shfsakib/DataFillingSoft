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
            function.LoadGrid(dataGridView1, $@"SELECT  COALESCE(NULLIF(FileName,''), 'Data Not Available') FileName,COALESCE(NULLIF(FormSerial,''), 'Data Not Available') FormSerial,COALESCE(NULLIF(FormNo,''), 'Data Not Available') FormNo,COALESCE(NULLIF(CompanyCode,''), 'Data Not Available') CompanyCode,COALESCE(NULLIF(CompanyName,''), 'Data Not Available') CompanyName,COALESCE(NULLIF(CompanyAddress,''), 'Data Not Available') CompanyAddress,COALESCE(NULLIF(ZipCode,''), 'Data Not Available') ZipCode,COALESCE(NULLIF(Fax,''), 'Data Not Available') Fax,COALESCE(NULLIF(Website,''), 'Data Not Available') Website,COALESCE(NULLIF(Email,''), 'Data Not Available') Email,COALESCE(NULLIF(ContactNo,''), 'Data Not Available') ContactNo,COALESCE(NULLIF(State,''), 'Data Not Available') State,COALESCE(NULLIF(Country,''), 'Data Not Available') Country,COALESCE(NULLIF(NoOfEmployees,''), 'Data Not Available') Headquarter,COALESCE(NULLIF(NoOfEmployees,''), 'Data Not Available')  NoOfEmployees,COALESCE(NULLIF(Industry,''), 'Data Not Available') Industry,COALESCE(NULLIF(BrandAmbassador,''), 'Data Not Available') BrandAmbassador,COALESCE(NULLIF(MediaPartner,''), 'Data Not Available') MediaPartner,COALESCE(NULLIF(SocialMedia,''), 'Data Not Available') SocialMedia, 
            COALESCE(NULLIF(FrenchiesPartner,''), 'Data Not Available') FrenchiesPartner,COALESCE(NULLIF(Investor,''), 'Data Not Available') Investor,COALESCE(NULLIF(AdvertisingPartner,''), 'Data Not Available') AdvertisingPartner,COALESCE(NULLIF(Product,''), 'Data Not Available') Product,COALESCE(NULLIF(Services,''), 'Data Not Available') Services,COALESCE(NULLIF(Manager,''), 'Data Not Available') Manager,COALESCE(NULLIF(RegistrationDate,''), 'Data Not Available') RegistrationDate,COALESCE(NULLIF(YearlyRevenue,''), 'Data Not Available') YearlyRevenue,COALESCE(NULLIF(Subclassification,''), 'Data Not Available') Subclassification,COALESCE(NULLIF(Landmark,''), 'Data Not Available') Landmark,COALESCE(NULLIF(AccoutAudit,''), 'Data Not Available') AccoutAudit,COALESCE(NULLIF(Currency,''), 'Data Not Available') Currency,COALESCE(NULLIF(YearlyExpense,''), 'Data Not Available') YearlyExpense
                FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private DataTable TableData()
        {
            return function.LoadTable($@"SELECT  COALESCE(NULLIF(FileName,''), 'Data Not Available') FileName,COALESCE(NULLIF(FormSerial,''), 'Data Not Available') FormSerial,COALESCE(NULLIF(FormNo,''), 'Data Not Available') FormNo,COALESCE(NULLIF(CompanyCode,''), 'Data Not Available') CompanyCode,COALESCE(NULLIF(CompanyName,''), 'Data Not Available') CompanyName,COALESCE(NULLIF(CompanyAddress,''), 'Data Not Available') CompanyAddress,COALESCE(NULLIF(ZipCode,''), 'Data Not Available') ZipCode,COALESCE(NULLIF(Fax,''), 'Data Not Available') Fax,COALESCE(NULLIF(Website,''), 'Data Not Available') Website,COALESCE(NULLIF(Email,''), 'Data Not Available') Email,COALESCE(NULLIF(ContactNo,''), 'Data Not Available') ContactNo,COALESCE(NULLIF(State,''), 'Data Not Available') State,COALESCE(NULLIF(Country,''), 'Data Not Available') Country,COALESCE(NULLIF(NoOfEmployees,''), 'Data Not Available') Headquarter,COALESCE(NULLIF(NoOfEmployees,''), 'Data Not Available')  NoOfEmployees,COALESCE(NULLIF(Industry,''), 'Data Not Available') Industry,COALESCE(NULLIF(BrandAmbassador,''), 'Data Not Available') BrandAmbassador,COALESCE(NULLIF(MediaPartner,''), 'Data Not Available') MediaPartner,COALESCE(NULLIF(SocialMedia,''), 'Data Not Available') SocialMedia, 
            COALESCE(NULLIF(FrenchiesPartner,''), 'Data Not Available') FrenchiesPartner,COALESCE(NULLIF(Investor,''), 'Data Not Available') Investor,COALESCE(NULLIF(AdvertisingPartner,''), 'Data Not Available') AdvertisingPartner,COALESCE(NULLIF(Product,''), 'Data Not Available') Product,COALESCE(NULLIF(Services,''), 'Data Not Available') Services,COALESCE(NULLIF(Manager,''), 'Data Not Available') Manager,COALESCE(NULLIF(RegistrationDate,''), 'Data Not Available') RegistrationDate,COALESCE(NULLIF(YearlyRevenue,''), 'Data Not Available') YearlyRevenue,COALESCE(NULLIF(Subclassification,''), 'Data Not Available') Subclassification,COALESCE(NULLIF(Landmark,''), 'Data Not Available') Landmark,COALESCE(NULLIF(AccoutAudit,''), 'Data Not Available') AccoutAudit,COALESCE(NULLIF(Currency,''), 'Data Not Available') Currency,COALESCE(NULLIF(YearlyExpense,''), 'Data Not Available') YearlyExpense
                FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");

        }
        private void ExportToExcel(DataTable dataTable, string filePath)
        {
            try
            {
                if (dataTable == null || dataTable.Columns.Count == 0)
                {
                    function.MessageBox("Null or empty data table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                function.MessageBox(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
