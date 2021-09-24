using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ModuleEcheloneRebooted
{
    public class Helpers
    {
        private static Random random = new Random();
        private string connStr;

        public Helpers(IConfiguration _configuration)
        {
            connStr = _configuration.GetConnectionString(name: "postgre");
        }
        
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void UpdateAllGuid()
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connStr);
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "select \"id\" from \"Users\""
                };

                NpgsqlDataReader sdr = comm.ExecuteReader();
                NpgsqlConnection conn2 = new NpgsqlConnection(connStr); 
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
    }




}