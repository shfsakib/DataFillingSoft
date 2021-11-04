using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web;
using System.Windows.Forms;

namespace DataFillingSoftDeskApp.Class
{
    public class Function
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private static Function _instance;
        private SqlConnection con;
        public static Function GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Function();
            }
            return _instance;
        }

        public Function()
        {
            if (con == null)
            {
                con = new SqlConnection(Connection);
            }
        }

        public string Connection2 = new SqlConnectionStringBuilder
        {
            DataSource = ".\\local",
            InitialCatalog = "DataFillingDb",
            UserID = "sa",
            Password = "123456789",
            MultipleActiveResultSets = true,
            ConnectTimeout = 0,
            Pooling = true,
            MinPoolSize = 0,
            MaxPoolSize = 4096
        }.ToString();
        public string Connection = new SqlConnectionStringBuilder
        {
            DataSource = ".\\local",
            InitialCatalog = "DataFillingDb",
            UserID = "sa",
            Password = "ShfS@kib16",
            MultipleActiveResultSets = true,
            ConnectTimeout = 0,
            Pooling = true,
            MinPoolSize = 0,
            MaxPoolSize = 4096
        }.ToString();
        public string Connection1 = @"Data Source=.\local;Initial Catalog=KidsLearningDb;Integrated Security=True";

        public List<string> ListData(string sql)
        {
            List<string> list = new List<string>();
            con = new SqlConnection(Connection);

            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); list.Clear();
                //List.Add("Select");
                while (rd.Read())
                {
                    list.Add(rd[0].ToString());
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                //ignore
            }
            return list;
        }
        public string ImageToBase64(Image image,
            System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public bool Execute(string str)
        {
            bool result = false;
            SqlConnection Conn = new SqlConnection(Connection);
            try
            {

                if (Conn.State != ConnectionState.Open) Conn.Open();
                SqlCommand cmd = new SqlCommand(str, Conn);
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    result = true;
                else
                    result = false;
                if (Conn.State != ConnectionState.Closed) Conn.Close();
            }
            catch (Exception ex) { if (Conn.State != ConnectionState.Closed) Conn.Close(); }
            return result;
        }

        public void MessageBox(string msg, string title, MessageBoxButtons button, MessageBoxIcon icon)
        {
            System.Windows.Forms.MessageBox.Show(msg, title, button, icon);
        }

        public string MacAddress()
        {
            var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            return macAddr.ToString();
        }
        public string IsExist(string str)
        {
            string result = "";
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                    result = DR[0].ToString();
                DR.Close();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
            return result;
        }

        public bool IsConnected()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ValidDate(TextBox date)
        {
            try
            {
                if (date.Text == "" || date.Text.Length != 10)
                    return true;
                else
                    DateTime.Parse(date.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            }
            catch (Exception EX)
            { return true; }

            return false;
        }

        public string Date()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy_hh:mm:ss");
            return date;
        }
        public DateTime Timezone(DateTime datetime)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime printDate = TimeZoneInfo.ConvertTime(datetime, timezoneInfo);
            return printDate;
        }

        public bool EmailValidation(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void LoadGrid(DataGridView ob, string query)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            try
            {
                ob.Visible = true;
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                ob.DataSource = table;
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }
        public DataTable LoadTable(string query)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            try
            {
                 
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed) con.Close();
                return table;
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
                return null;
            }
        }

        public bool MobileNoValidation(string mobileNo)
        {
            try
            {
                if (mobileNo == "" || mobileNo.Length < 11 || !mobileNo.StartsWith("0"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }
        public bool SendEmail(string fromMail, string toMail, string subject, string body, string fromPass)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromMail);
                message.To.Add(new MailAddress(toMail));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromMail, fromPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string CheckPassword(string pass)
        {
            if (pass.Length < 6 || pass.Length > 15)
            {
                return "Password must be between 6-15 character";
            }
            else if (pass.Contains(" "))
            {
                return "Remove space from your password";
            }
            else if (!pass.Any(char.IsUpper))
            {
                return "Password must have at least one capital latter";
            }
            else if (!pass.Any(char.IsLower))
            {
                return "Password must have at least one small latter";
            }
            else if (!pass.Any(char.IsDigit))
            {
                return "Password must be a combination of alphanumeric characters";
            }
            else
            {
                return "Strong password";
            }
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string GenerateId(string query)
        {
            string id = "";
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id = reader[0].ToString();
                    if (id == "")
                        id = "1001";
                    else
                    {
                        id = (int.Parse(id) + 1).ToString();
                    }

                }
                reader.Close();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch
            {
            }
            return id;
        }

        public string DateConvert(string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}