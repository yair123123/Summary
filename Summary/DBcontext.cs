using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Summary
{
    internal class DBcontext
    {
        public static string GetConnString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("secrets.json", optional: true, reloadOnChange: true) // Add secrets.json with optional and reload on change
                .Build();

            // Read the connection string from the configuration
            string conn = builder["ConnectionString"];
            if (conn == null)
            {
                throw new Exception("cannot read only");
            }
            return conn;
        }
    

    private readonly string _connectionString;
        public DBcontext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable MakeQuery(string queryStr, params SqlParameter[] parameters)
        {
            DataTable output = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(output);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return output;
        }
    }
}
