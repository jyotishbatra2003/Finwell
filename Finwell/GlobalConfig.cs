using FinwellLibrary.DataAccess;
using FinwellLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary
{
    public static class GlobalConfig
    { 
        public static IDataConnection Connection { get; private set; }
        public static userModel CurrentUser { get; set; }
        public static void InitializeConnections()
        {
            SqlConnector sql = new SqlConnector(); 
            Connection = sql;
        } 
         
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
