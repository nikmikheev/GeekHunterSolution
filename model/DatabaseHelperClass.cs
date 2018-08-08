using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace GeekHunterProject
{
    public class DatabaseHelperClass
    {
        private string connectionString;

        public SQLiteConnection Connection { get; set; }

        public DatabaseHelperClass()
        {
            ConnectToDatabase();
        }

        /// <summary>
        /// Creates a connection with our database file.
        /// </summary>
        public void ConnectToDatabase()
        {
            string dbFileName = AppDomain.CurrentDomain.BaseDirectory + "db\\GeekHunter.sqlite";
            connectionString = $"Data Source={dbFileName};Version=3;";

            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
                connectionString += "New=True;";
                CreateTablesAndFillTestData();
            }
            else
            {
                connectionString += "New=False;";
            }

            Connection = new SQLiteConnection(connectionString);
            Connection.Open();
        }

        /// <summary>
        /// return true, if connection is open, reconnect if connection closed
        /// </summary>
        /// <returns></returns>
        public bool CheckConnectionState()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                Console.WriteLine("Connection with database reopened");
            }
            return (Connection.State == ConnectionState.Open);
        }


        /// <summary>
        /// Create all table and fill tables with test data.
        /// </summary>
        public void CreateTablesAndFillTestData()
        {
            try
            {
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS 'Candidate'(
                            'Id'    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'FirstName' text NOT NULL,
                            'LastName'  text NOT NULL,
                            'EnteredDate'   Datetime NOT NULL)";
                SQLiteCommand command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();

                sql = @"INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(1, 'Jon', 'Snow', '2012-01-01 00:00:00');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(2, 'Daenerys', 'Tangaryen', '2011-11-01 00:00:00');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(3, 'Cersei', 'Lannister', '2010-01-01 00:00:00');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(4, 'Sansa', 'Stark', '2012-01-01 00:00:00');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(5, 'Arya', 'Stark', '2012-06-01 00:00:00');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(6, 'Tyrion', 'Lannister', '1992-01-01 00:00:00');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(7, 'New', 'Candidate', '2018-08-07 07:31:59.9055334');
                    INSERT INTO 'Candidate'(Id, FirstName, LastName, EnteredDate) VALUES(8, 'Nikolay', 'Mikheev', '2018-08-07 12:19:28.3797349');";
                command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();

                sql = @"CREATE TABLE IF NOT EXISTS `Skill` (
                        'Id'	INTEGER PRIMARY KEY AUTOINCREMENT,
	                    'Name'	text NOT NULL);";
                command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();

                sql = @"INSERT INTO 'Skill' (Id,Name) VALUES (1,'SQL');
                    INSERT INTO 'Skill' (Id,Name) VALUES (2,'JavaScript');
                    INSERT INTO 'Skill' (Id,Name) VALUES (3,'C#');
                    INSERT INTO 'Skill' (Id,Name) VALUES (4,'Java');
                    INSERT INTO 'Skill' (Id,Name) VALUES (5,'Python');";
                command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();

                sql = @"CREATE TABLE IF NOT EXISTS 'CandidateSkill' (
                        'CandidateID'	INTEGER NOT NULL,
	                    'SkillID'	INTEGER NOT NULL,
	                PRIMARY KEY('CandidateID','SkillID'));";
                command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();

                sql = @"INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (1,2);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (1,1);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (2,2);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (2,3);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (4,4);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (4,5);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (4,3);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (8,1);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (8,2);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (8,3);
                    INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (8,5);";
                command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
        }

    }
}
