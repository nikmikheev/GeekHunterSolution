using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace GeekHunterProject
{
    public class DatabaseHelperClass
    {
        // Holds our connection with the database
        private SQLiteConnection connection;

        public SQLiteConnection Connection { get => connection; set => connection = value; }

        public DatabaseHelperClass()
        {
            ConnectToDatabase();
        }

        // Creates a connection with our database file.
        public void ConnectToDatabase()
        {
            string dbFileName = AppDomain.CurrentDomain.BaseDirectory + "db\\GeekHunter.sqlite";
            string connectionString = String.Format("Data Source={0};Version=3;", dbFileName);

            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
                connectionString += "New=True;";
                //CreateTablesAndFillTestData();
            }
            else
            {
                connectionString += "New=False;";
            }

            Connection = new SQLiteConnection(connectionString);
            Connection.Open();
        }

        // return true, if connection is open, reconnect if connection closed
        public bool CheckConnectionState()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                Console.WriteLine("Connection with database reopened");
            }

            return (Connection.State == ConnectionState.Open);
        }


    }
}
