using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace VERIFICATION
{
    class DbConnection
    {
        public static SqlConnection con = new SqlConnection("Server=tcp:nimasamlsserver.database.windows.net,1433;Initial Catalog=NIMASAMLS.MDF;Persist Security Info=False;User ID=nimasamls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        

        public static void checkConnection()
        {
            if (DbConnection.con.State == ConnectionState.Open)
            {
                DbConnection.con.Close();
            }
        }

       
    }
}
