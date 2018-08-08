using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeekHunterProject
{
    public class CandidateHelper
    {
        private DatabaseHelperClass databaseHelper;

        public CandidateHelper(DatabaseHelperClass databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        // -- Candidate Functions ----

        public ObservableCollection<Candidate> Candidates
        {
            get
            {
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2 = new DataTable();
                try
                {
                    databaseHelper.CheckConnectionState();
                    using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT id, FirstName, LastName, EnteredDate FROM Candidate";
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        adapter.Fill(dataTable1);

                        cmd.CommandText = "select CandidateId, Skill.Id as 'SkillId', name as 'SkillName' from " +
                                   "CandidateSkill inner join Skill on Skill.Id = CandidateSkill.SkillID";
                        //cmd.Prepare();
                        //cmd.Parameters.AddWithValue("@Id", CandidateId);

                        SQLiteDataAdapter adapter2 = new SQLiteDataAdapter(cmd);
                        adapter2.Fill(dataTable2);

                        List<Candidate> myCollection = new List<Candidate>();

                        myCollection = (from DataRow row in dataTable1.Rows
                                        select new Candidate
                                        {
                                            Id = Convert.ToInt32(row["Id"]),
                                            FirstName = row["FirstName"].ToString(),
                                            LastName = row["LastName"].ToString(),
                                            EnteredDate = Convert.ToDateTime(row["EnteredDate"])
                                        }).ToList();


                        foreach (DataRow row in dataTable2.Rows)
                        {
                            int tmpId = Convert.ToInt32(row["CandidateId"]);
                            int tmpIndex = myCollection.FindIndex(x => x.Id == tmpId);
                            if (tmpIndex >= 0) 
                                {
                                myCollection[tmpIndex].SkillList.Add(new Skill() {Id = Convert.ToInt32(row["SkillId"]),
                                                                                  Name = row["SkillName"].ToString()});
                                }
                        }


                        ObservableCollection<Candidate> CandidatesList = new ObservableCollection<Candidate>(myCollection);
                    return CandidatesList;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }

        }

        public DataSet GetDataTableCandidate()
        {
            DataSet dDataSet= new DataSet();
            String sqlQuery;
            try
            {
                databaseHelper.CheckConnectionState();
                sqlQuery = "SELECT * FROM Candidate";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, databaseHelper.Connection);
                adapter.Fill(dDataSet.Tables[0]);
                return dDataSet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }

        public int EditCandidate(Candidate editCandidate)
        {
            int result = -1;
            databaseHelper.CheckConnectionState();

            if (IsCandidateExists(editCandidate.Id) != 1)
            {
                return result; // Candidate doesnt exists, nothing to edit
            }
            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "UPDATE Candidate "
                    + "SET FirstName = @FirstName "
                    + "SET LastName = @LastName "
                    + "WHERE Id = @Id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@FirstName", editCandidate.FirstName);
                cmd.Parameters.AddWithValue("@LastName", editCandidate.LastName);
                cmd.Parameters.AddWithValue("@Id", editCandidate.Id);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return result;
        }


        public int AddCandidate(Candidate newCandidate)
        {
            int result = -1;
            databaseHelper.CheckConnectionState();

            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "INSERT INTO Candidate(FirstName, LastName, EnteredDate) VALUES (@FirstName, @LastName, @EnteredDate)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@FirstName", newCandidate.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newCandidate.LastName);
                cmd.Parameters.AddWithValue("@EnteredDate", newCandidate.EnteredDate);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return result;
        }

        // Delete candidates with Id = candidateId
        public int DeleteCandidate(int candidateId)
        {
            int result = -1;
            databaseHelper.CheckConnectionState();

            if (IsCandidateExists(candidateId) < 1)
            {
                return result; // Candidate doesnt exists, nothing to delete 
            }

            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "DELETE FROM Candidate WHERE Id = @Id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", candidateId);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return result;
        }

        // return count of candidates with Id = CandidateId
        public int IsCandidateExists(int candidateId)
        {
            // databaseHelper.CheckConnectionState();
            int RowCount = 0;
            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "SELECT count(id) FROM Candidate WHERE Id = @Id";
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", candidateId);
                try
                {
                    RowCount = (int)(cmd.ExecuteScalar());
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return RowCount;
        }

    }
}
