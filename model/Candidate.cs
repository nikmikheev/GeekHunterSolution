using System;
using System.Collections.Generic;

namespace GeekHunterProject
{
    public class Candidate
    {

        #region Properties Getters and Setters

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime EnteredDate { get; set; }

        public List<Skill> SkillList { get; set; }

        public List<Skill> NoSkillList { get; set; }

        #endregion
        public Candidate()
        {
            this.SkillList = new List<Skill>();
            this.NoSkillList = new List<Skill>();
        }

        public Candidate(string firstName, string lastName, DateTime enteredDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EnteredDate = enteredDate;
            this.SkillList = new List<Skill>();
            this.NoSkillList = new List<Skill>();
        }

        public Candidate(int id, string firstName, string lastName, DateTime enteredDate)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EnteredDate = enteredDate;
            this.SkillList = new List<Skill>();
            this.NoSkillList = new List<Skill>();
        }

    }

}

