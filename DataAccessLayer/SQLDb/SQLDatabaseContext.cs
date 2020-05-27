using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLayer.SQLDb
{
    public class SQLDatabaseContext : IDatabaseFactory
    {
        MySqlConnection sqlConnection;
        public SQLDatabaseContext()
        {
            string connectionString = "SERVER = 127.0.0.1; PORT = 3306; DATABASE = test; USER Id = root; PASSWORD = root";
            sqlConnection = new MySqlConnection(connectionString);

        }

        public void WriteToDatabase(string fileLoction)
        {
            string command;
            try
            {
                string fileLocation = "C:/Users/vmattapalli/Desktop/PointOfInterestCoordinatesList.txt";
                string[] lines = File.ReadAllLines(fileLocation);
                var fileLocationList = fileLocation.Split('/');
                var tableName = fileLocationList[fileLocationList.Length - 1].Split('.')[0];
                var keyNames = lines[0].Split('|');
                var dataType = " varchar(256) ";

                string sql = "CREATE TABLE IF NOT EXISTS " + tableName + " ( " + string.Join(" varchar(256) , ", keyNames);
                sql += dataType + ");";
                
                sqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                string[] commands = new string[lines.Length];
                for (int i = 1; i < lines.Length; i++)
                {


                    var row = lines[i].Split("|");
                    row = row.Select(x => x.Replace("\"", "\\\"")).ToArray();
                    commands[i - 1] = "( \"" + string.Join("\",\"", row) + "\")";



                }
                command = "INSERT INTO " + tableName + " VALUES " + string.Join(',', commands);

                command = command.Remove(command.Length - 1, 1) + ";";
                cmd = new MySqlCommand(command, sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
