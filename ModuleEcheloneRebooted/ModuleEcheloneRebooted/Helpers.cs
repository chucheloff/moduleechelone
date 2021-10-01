using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ModuleEcheloneRebooted
{
    public class Helpers
    {
        private static Random _random = new Random();
        private string _connStr;

        public Helpers(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString(name: "postgre");
        }
        
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public void UpdateAllGuid()
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
                        cmd.Parameters.AddWithValue("str", this.RandomString(16));
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

        public T ExecuteSQL_Scalar<T>(string connectionString, string SQL, params Tuple<string, string>[] parameters)
        {
            T obj = default(T);
            using (NpgsqlConnection conn = new NpgsqlConnection(_connStr))
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                foreach (var tuple in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(tuple.Item1, tuple.Item2));
                }
                conn.Open();
                object _obj = cmd.ExecuteScalar();
                if (_obj != null)
                { //db request returned something
                    obj = (T)_obj;
                }
                conn.Close(); //close connection
            }
            return  obj;
        }
        
        public T ExecuteSQL_Scalar<T>(string connectionString, string SQL, params NpgsqlParameter[]  parameters)
        {
            T obj = default(T);
            using (NpgsqlConnection conn = new NpgsqlConnection(_connStr))
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                conn.Open();
                object _obj = cmd.ExecuteScalar();
                if (_obj != null)
                { //db request returned something
                    obj = (T)_obj;
                }
                conn.Close(); //close connection
            }
            return  obj;
        }

        public int GetNextID()
        {
            var id = -1;
            using(NpgsqlConnection conn = new NpgsqlConnection(_connStr))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select max(\"ProductID\") from \"Products\"";
                object _obj = cmd.ExecuteScalar();
                if (_obj != null)
                {
                    id = (int)_obj;
                }
                conn.Close();
            }
            return id + 1;
        }
    }




}