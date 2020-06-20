using System.Data;
using Npgsql;

namespace Arkanaoid_poo.Controlador
{
    public static class DBConnection
    {
        private static string ConnectionString = 
            "Server=127.0.0.1;Port=5432;User Id=postgres;Password=uca;Database=prueba";
            
        public static DataTable ExecuteQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();
            
            return ds.Tables[0];
        }
        
        public static void ExecuteNonQuery(string sql)
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
                    
                        conn.Open();
                        NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
                        nc.ExecuteNonQuery();
                        conn.Close();
                    }
            
    }
}