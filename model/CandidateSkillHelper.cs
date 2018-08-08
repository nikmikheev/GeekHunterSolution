using System;
using System.Data;
using System.Data.SQLite;

namespace GeekHunterProject
{
    /// <summary>
    /// CandidateSkills Functions 
    /// </summary>
    public class CandidateSkillHelper
    {
        private readonly DatabaseHelperClass databaseHelper;

        public CandidateSkillHelper(DatabaseHelperClass databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public int AddCandidateSkill(int candidateId, int newSkillId)
        {
            var result = -1;
            databaseHelper.CheckConnectionState();

            // Record already exists, nothing to add.  
            if (IsCandidateSkillExists(candidateId, newSkillId))
            {
                result = 1; // Success
                return result;
            }

            using (var cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = @"INSERT INTO CandidateSkill(CandidateId, SkillID) 
                                    VALUES (@CandidateId, @SkillID)";
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
            var result = -1;
            databaseHelper.CheckConnectionState();

            // Record doesn`t exists, nothing to delete.  
            if (!IsCandidateSkillExists(candidateId, skillId))
            {
                result = 1; // success
                return result;
            }

            using (var cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = @"DELETE FROM CandidateSkill 
                                    WHERE CandidateId = @CandidateId AND SkillId = @SkillId";
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
        /// <summary>
        /// Return true if candidate with Id and Skill ID exists
        /// </summary>
        /// <param name="candidateId"></param>
        /// <param name="skillId"></param>
        /// <returns>bool</returns>
        public bool IsCandidateSkillExists(int candidateId, int skillId)
        {
            var RowCount = 0;
            using (var cmd = new SQLiteCommand(databaseHelper.Connection))
            {
                cmd.CommandText = @"SELECT count(*) 
                                    FROM CandidateSkill 
                                    WHERE CandidateId = @CandidateId AND SkillId = @SkillId";
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@CandidateId", candidateId);
                cmd.Parameters.AddWithValue("@SkillId", skillId);
                try
                {
                    var tmpRes = cmd.ExecuteScalar();
                    RowCount = Convert.ToInt32(tmpRes);
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return RowCount > 0;
        }


    }
}
