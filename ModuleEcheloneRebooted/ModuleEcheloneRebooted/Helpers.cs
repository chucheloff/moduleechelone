using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Npgsql;

namespace ModuleEcheloneRebooted
{
    public class Helpers
    {
        private static Random _random = new();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static void UpdateAllGuid(string _connStr)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(_connStr);
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "select \"id\" from \"Users\""
                };

                NpgsqlDataReader sdr = comm.ExecuteReader();
                NpgsqlConnection conn2 = new NpgsqlConnection(_connStr); 
                conn2.Open();
                while (sdr.Read())
                {
                    var rowsAffected = 0;
                    using (var cmd = new NpgsqlCommand(
                        "update \"Users\" set \"GUID\" = (@str) where \"id\" = (@id) ", conn2))
                    {
                        cmd.Parameters.AddWithValue("id", (int) sdr["id"]);
                        cmd.Parameters.AddWithValue("str", RandomString(16));
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine(rowsAffected > 0
                        ? $"Updated {rowsAffected} GUIDs successfully"
                        : $"Something went wrong");
                }
               conn.Close(); 
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        public static T ExecuteSQL_Scalar<T>(string connectionString, string sql, params Tuple<string, string>[] parameters)
        {
            T obj = default(T);
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                foreach (var tuple in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(tuple.Item1, tuple.Item2));
                }
                conn.Open();
                object pObj = cmd.ExecuteScalar();
                if (pObj != null)
                { //db request returned something
                    obj = (T)pObj;
                }
                conn.Close(); //close connection
            }
            return  obj;
        }
        
        public static T ExecuteSQL_Scalar<T>(string connectionString, string sql, params NpgsqlParameter[]  parameters)
        {
            Console.WriteLine("EXECUTING NEW SQL QUERY");
            Console.WriteLine(sql);
            T obj = default(T);
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                foreach (var param in parameters)
                {
                    Console.WriteLine($"Adding param to sql query[@name = {param.NpgsqlValue}]");
                    cmd.Parameters.Add(param);
                }
                conn.Open();
                object pObj = cmd.ExecuteScalar();
                if (pObj != null)
                { //db request returned something
                    obj = (T) pObj;
                }
                conn.Close(); //close connection
            }
            return  obj;
        }
        
        public static T ExecuteSQL_Scalar<T>(string connectionString, string sql)
        {
            NpgsqlParameter[] parameters = new NpgsqlParameter[0];
            T obj = ExecuteSQL_Scalar<T>(connectionString, sql, parameters);
            return obj;
        }
        
        
        public static int GetNextId(string _connStr)
        {
            var id = -1;
            using(NpgsqlConnection conn = new NpgsqlConnection(_connStr))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select max(\"ProductID\") from \"Products\"";
                object pObj = cmd.ExecuteScalar();
                if (pObj != null)
                {
                    id = (int)pObj;
                }
                conn.Close();
            }
            Console.WriteLine($"Returning new id for the Products table [@id={id}]");
            return id + 1;
        }
    }




}