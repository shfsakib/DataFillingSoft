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
            function.LoadGrid(dataGridView1, $@"SELECT        FormSerial, FormNo, CompanyCode, CompanyName, CompanyAddress, ZipCode, Fax, Website, Email, ContactNo, State, Country, Headquarter, NoOfEmployees, Industry, BrandAmbassador, MediaPartner, SocialMedia, 
            FrenchiesPartner, Investor, AdvertisingPartner, Product, Services, Manager, RegistrationDate, YearlyRevenue, Subclassification, Landmark, AccoutAudit, Currency, YearlyExpense, FileName
                FROM            FormData WHERE AuthenticationKey = '{Properties.Settings.Default.AuthKey}' ORDER BY FormSerial ASC");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private DataTable TableData()
        {
            return function.LoadTable($@"SELECT        FormSerial, FormNo, CompanyCode, CompanyName, CompanyAddress, ZipCode, Fax, Website, Email, ContactNo, State, Country, Headquarter, NoOfEmployees, Industry, BrandAmbassador, MediaPartner, SocialMedia, 
            FrenchiesPartner, Investor, AdvertisingPartner, Product, Services, Manager, RegistrationDate, YearlyRevenue, Subclassification, Landmark, AccoutAudit, Currency, YearlyExpense, FileName
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
