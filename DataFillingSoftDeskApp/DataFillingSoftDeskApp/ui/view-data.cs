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
        DataTable table = new DataTable();
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
                return;
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
            dataGridView1.DataSource = table;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
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
                    }
                }

                //check filepath
                if (filePath != null || filePath != "")
                {
                    try
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                        @"\ExportViewData\FormData.xlsx"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                  @"\ExportViewData\FormData.xlsx");
                        }

                        Directory.CreateDirectory(
                                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ExportViewData");
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
                        DialogResult dialogResult = MessageBox.Show("Excel file can\'t be saved " + "Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (dialogResult == DialogResult.OK)
                        {
                            this.Hide();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                function.MessageBox("Please close previous generated excel sheet first"+ "Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ExportToExcel(table, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ExportViewData\FormData.xlsx");
            }
            else
            {
                function.MessageBox("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        } 
    }
}
