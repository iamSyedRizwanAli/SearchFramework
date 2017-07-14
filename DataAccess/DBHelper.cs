using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccess
{
    internal class DBHelper : IDisposable
    {
        private String connString = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
        SqlConnection con = null;

        public DBHelper()
        {
            con = new SqlConnection(connString);
            con.Open();
        }

        public int ExecuteQuery(String query)
        {
            SqlCommand com = new SqlCommand(query, con);
            return com.ExecuteNonQuery();
        }

        public Object ExecuteScalar(String query)
        {
            SqlCommand com = new SqlCommand(query, con);
            return com.ExecuteScalar();
        }

        public SqlDataReader ExecuteDataReader(String query)
        {
            SqlCommand com = new SqlCommand(query, con);
            return com.ExecuteReader();
        }

        public DataTable getDataTable(String query)
        {
            DataTable table = null;

            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(com);
            DataSet dataSet = new DataSet();
            sda.Fill(dataSet);
            table = dataSet.Tables[0];

            return table;
        }

        void IDisposable.Dispose()
        {
            con.Close();
        }
    }
}
