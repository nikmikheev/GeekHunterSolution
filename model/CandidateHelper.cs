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
        private readonly DatabaseHelperClass dbHelper;
        private CandidateSkillHelper skills;

        public CandidateHelper()
        {
            dbHelper = new DatabaseHelperClass();
            skills = new CandidateSkillHelper(dbHelper);
        }


        public ObservableCollection<Candidate> AllCandidates()
        {
            try
            {
                dbHelper.CheckConnectionState();
                using (var cmd = new SQLiteCommand(dbHelper.Connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT id, FirstName, LastName, EnteredDate FROM Candidate";
                    var adapter = new SQLiteDataAdapter(cmd);
                    var dataCandidate = new DataTable();
                    adapter.Fill(dataCandidate);

                    cmd.CommandText = @"SELECT CandidateId, Skill.Id AS 'SkillId', name AS 'SkillName' 
                                        FROM CandidateSkill inner join Skill 
                                        ON Skill.Id = CandidateSkill.SkillID";

                    var adapter2 = new SQLiteDataAdapter(cmd);
                    var dataSkills = new DataTable();
                    adapter2.Fill(dataSkills);

                    cmd.CommandText = @"SELECT Candidate.Id AS 'CandidateID', Skill.Id AS 'SkillId', name AS 'SkillName' 
                                        FROM Candidate LEFT JOIN Skill
                                        WHERE (Candidate.Id, Skill.Id) NOT IN (SELECT CandidateId as 'ID', Skill.Id 
                                        FROM CandidateSkill INNER JOIN Skill ON Skill.Id = CandidateSkill.SkillID)";

                    var adapter3 = new SQLiteDataAdapter(cmd);
                    var dataNoSkills = new DataTable();
                    adapter3.Fill(dataNoSkills);

                    var myCollection = (from DataRow row in dataCandidate.Rows
                                                    select new Candidate
                                                    {
                                                        Id = Convert.ToInt32(row["Id"]),
                                                        FirstName = row["FirstName"].ToString(),
                                                        LastName = row["LastName"].ToString(),
                                                        EnteredDate = Convert.ToDateTime(row["EnteredDate"])
                                                    }).ToList();

                    foreach (DataRow row in dataSkills.Rows)
                    {
                        var tmpId = Convert.ToInt32(row["CandidateId"]);
                        var tmpIndex = myCollection.FindIndex(x => x.Id == tmpId);
                        if (tmpIndex >= 0)
                            myCollection[tmpIndex].SkillList.Add(new Skill()
                            {
                                Id = Convert.ToInt32(row["SkillId"]),
                                Name = row["SkillName"].ToString()
                            });
                    }


                    foreach (DataRow row in dataNoSkills.Rows)
                    {
                        var tmpId = Convert.ToInt32(row["CandidateId"]);
                        var tmpIndex = myCollection.FindIndex(x => x.Id == tmpId);
                        if (tmpIndex >= 0)
                            myCollection[tmpIndex].NoSkillList.Add(new Skill()
                            {
                                Id = Convert.ToInt32(row["SkillId"]),
                                Name = row["SkillName"].ToString()
                            });
                    }

                    var candidatesList = new ObservableCollection<Candidate>(myCollection);
                    return candidatesList;
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
            var result = -1;
            dbHelper.CheckConnectionState();

            if (!IsCandidateExists(editCandidate.Id)) return result;
            using (var cmd = new SQLiteCommand(dbHelper.Connection))
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

                // add assigned skills 
                foreach (Skill skill in editCandidate.SkillList)
                {
                    skills.AddCandidateSkill(editCandidate.Id, skill.Id);
                }

                // delete not-assigned skills 
                foreach (Skill noSkill in editCandidate.SkillList)
                {
                    skills.DeleteCandidateSkill(editCandidate.Id, noSkill.Id);
                }

            }
            return result;
        }


        public int AddCandidate(Candidate newCandidate)
        {
            var result = -1;
            dbHelper.CheckConnectionState();

            using (var cmd = new SQLiteCommand(dbHelper.Connection))
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
            var result = -1;
            dbHelper.CheckConnectionState();

            if (!IsCandidateExists(candidateId)) return result; // Candidate doesnt exists, nothing to delete 

            using (var cmd = new SQLiteCommand(dbHelper.Connection))
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
            var RowCount = 0;
            using (var cmd = new SQLiteCommand(dbHelper.Connection))
            {
                cmd.CommandText = @"SELECT count(id) FROM Candidate WHERE Id = @Id";
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", candidateId);
                try
                {
                    var tmpRes = cmd.ExecuteScalar();
                    RowCount = Convert.ToInt32(tmpRes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return RowCount > 0;
        }

    }
}
