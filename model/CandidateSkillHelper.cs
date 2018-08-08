using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekHunterProject
{
    public class CandidateSkillHelper
    {
        // -- CandidateSkills Functions ----
        private DatabaseHelperClass databaseHelper;

        public CandidateSkillHelper(DatabaseHelperClass databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        //// return DataTable with skills for one Candidate, or all if CandidateId =0
        //public DataTable GetDataTableCandidateSkills(int CandidateId)
        //{
        //    DataTable dTable = new DataTable();
        //    try
        //    {
        //        databaseHelper.CheckConnectionState();
        //        using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            if (CandidateId == 0)
        //            {
        //                cmd.CommandText = "select Candidate.Id,FirstName, LastName, Skill.Id as 'SkillId', name as 'Skill' from " +
        //                       "Candidate inner join CandidateSkill on Candidate.Id = CandidateSkill.CandidateID " +
        //                       "inner join Skill on Skill.Id = CandidateSkill.SkillID";
        //                cmd.Prepare();
        //            }
        //            else
        //            {
        //                cmd.CommandText = "select Candidate.Id,FirstName, LastName, Skill.Id as 'SkillId', name as 'Skill' from " +
        //                       "Candidate inner join CandidateSkill on Candidate.Id = CandidateSkill.CandidateID " +
        //                       "inner join Skill on Skill.Id = CandidateSkill.SkillID WHERE Candidate.Id = @Id";
        //                cmd.Prepare();
        //                cmd.Parameters.AddWithValue("@Id", CandidateId);
        //            }
        //            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
        //            adapter.Fill(dTable);
        //            return dTable;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return null;
        //    }

        //}

        public int AddCandidateSkill(int candidateId, int newSkillId)
        {
            int result = -1;
            databaseHelper.CheckConnectionState();
            
            // Record already exists, nothing to add.  
            if (IsCandidateSkillExists(candidateId, newSkillId) == 1)
            {
                result = 1; // success
                return result;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "INSERT INTO CandidateSkill(CandidateId, SkillID) VALUES (@CandidateId, @SkillID)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@CandidateId", candidateId);
                cmd.Parameters.AddWithValue("@SkillId", newSkillId);
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

        public int DeleteCandidateSkill(int candidateId, int skillId)
        {
            int result = -1; // error
            databaseHelper.CheckConnectionState();

            // Record doesnt exists, nothing to delete.  
            if (IsCandidateSkillExists(candidateId, skillId) < 1)
            {
                result = 1; // success
                return result;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "DELETE FROM CandidateSkill WHERE CandidateId = @CandidateId AND SkillId = @SkillId";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@CandidateId", candidateId);
                cmd.Parameters.AddWithValue("@SkillId", skillId);
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

        public int IsCandidateSkillExists(int candidateId, int skillId)
        {
            //            databaseHelper.CheckConnectionState();
            int RowCount = 0;
            using (SQLiteCommand cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = "SELECT count(*) FROM CandidateSkill WHERE CandidateId = @CandidateId AND SkillId = @SkillId";
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@CandidateId", candidateId);
                cmd.Parameters.AddWithValue("@SkillId", skillId);
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
