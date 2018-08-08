using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace GeekHunterProject
{
    /// <summary>
    ///  Candidate Class Functions 
    /// </summary>
    public class CandidateHelper
    {
        private DatabaseHelperClass dbHelper;
        private CandidateSkillHelper skills;

        public CandidateHelper()
        {
            this.dbHelper = new DatabaseHelperClass();
            this.skills = new CandidateSkillHelper(dbHelper);
        }


        public ObservableCollection<Candidate> AllCandidates()
        {
            try
            {
                dbHelper.CheckConnectionState();
                using (SQLiteCommand cmd = new SQLiteCommand(dbHelper.Connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT id, FirstName, LastName, EnteredDate FROM Candidate";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dataCandidate = new DataTable();
                    adapter.Fill(dataCandidate);

                    cmd.CommandText = @"SELECT CandidateId, Skill.Id AS 'SkillId', name AS 'SkillName' 
                                        FROM CandidateSkill inner join Skill 
                                        ON Skill.Id = CandidateSkill.SkillID";

                    SQLiteDataAdapter adapter2 = new SQLiteDataAdapter(cmd);
                    DataTable dataSkills = new DataTable();
                    adapter2.Fill(dataSkills);

                    List<Candidate> myCollection = (from DataRow row in dataCandidate.Rows
                                                    select new Candidate
                                                    {
                                                        Id = Convert.ToInt32(row["Id"]),
                                                        FirstName = row["FirstName"].ToString(),
                                                        LastName = row["LastName"].ToString(),
                                                        EnteredDate = Convert.ToDateTime(row["EnteredDate"])
                                                    }).ToList();

                    foreach (DataRow row in dataSkills.Rows)
                    {
                        int tmpId = Convert.ToInt32(row["CandidateId"]);
                        int tmpIndex = myCollection.FindIndex(x => x.Id == tmpId);
                        if (tmpIndex >= 0)
                        {
                            myCollection[tmpIndex].SkillList.Add(new Skill()
                            {
                                Id = Convert.ToInt32(row["SkillId"]),
                                Name = row["SkillName"].ToString()
                            });
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

        /// <summary>
        /// Candidate Edit function
        /// </summary>
        /// <param name="editCandidate"></param>
        /// <returns></returns>
        public int EditCandidate(Candidate editCandidate)
        {
            int result = -1;
            dbHelper.CheckConnectionState();

            if (!IsCandidateExists(editCandidate.Id))
            {
                // Candidate doesnt exists, nothing to edit
                return result; 
            }
            using (SQLiteCommand cmd = new SQLiteCommand(dbHelper.Connection))
            {
                cmd.CommandText = @"UPDATE Candidate 
                                   SET FirstName = @FirstName 
                                   SET LastName = @LastName 
                                   WHERE Id = @Id";
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
            dbHelper.CheckConnectionState();

            using (SQLiteCommand cmd = new SQLiteCommand(dbHelper.Connection))
            {
                cmd.CommandText = @"INSERT INTO Candidate(FirstName, LastName, EnteredDate) 
                                    VALUES (@FirstName, @LastName, @EnteredDate)";
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

        /// <summary>
        /// Delete candidates with Id = candidateId
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns>int</returns>
        public int DeleteCandidate(int candidateId)
        {
            int result = -1;
            dbHelper.CheckConnectionState();

            if (!IsCandidateExists(candidateId))
            {
                return result; // Candidate doesnt exists, nothing to delete 
            }

            using (SQLiteCommand cmd = new SQLiteCommand(dbHelper.Connection))
            {
                cmd.CommandText = @"DELETE FROM Candidate WHERE Id = @Id";
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

        /// <summary>
        /// Return true if candidate with Id exists
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns>bool</returns>
        public bool IsCandidateExists(int candidateId)
        {
            int RowCount = 0;
            using (SQLiteCommand cmd = new SQLiteCommand(dbHelper.Connection))
            {
                cmd.CommandText = @"SELECT count(id) FROM Candidate WHERE Id = @Id";
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", candidateId);
                try
                {
                    RowCount = (int)(cmd.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return (RowCount > 0);
        }

    }
}
