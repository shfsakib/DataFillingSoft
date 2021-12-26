using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFillingSoftDeskApp.Class
{
    class ApiDataModel
    {
        private ApiDataModel apiDataModel;
        private static ApiDataModel _instance;

        public static ApiDataModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ApiDataModel();
            }
            return _instance;
        }
        
        public int id { get; set; }
        public string auth_code { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mac_address { get; set; }
        public string password { get; set; }
        public string is_active { get; set; }
        public string is_registered { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }
}
