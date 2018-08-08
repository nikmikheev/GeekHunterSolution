using System;
using System.Collections.Generic;

namespace GeekHunterProject
{
    public class Candidate
    {
        private int id;
        private string lastName;
        private DateTime enteredDate;
        private List<Skill> skillList;

        #region Properties Getters and Setters

        public int Id { get => id; set => id = value; }

        public string FirstName { get; set; }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                this.lastName = value;
            }
        }

        public DateTime EnteredDate
        {
            get { return this.enteredDate; }
            set
            {
                this.enteredDate = value;
            }
        }

        public List<Skill> SkillList { get => skillList; set => skillList = value; }

        #endregion
        public Candidate()
        {
            this.skillList = new List<Skill>();
        }

        public Candidate(string firstName, string lastName, DateTime enteredDate)
        {
            this.FirstName = firstName;
            this.lastName = lastName;
            this.enteredDate = enteredDate;
            this.skillList = new List<Skill>();
        }

        public Candidate(int id, string firstName, string lastName, DateTime enteredDate)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.lastName = lastName;
            this.enteredDate = enteredDate;
            this.skillList = new List<Skill>();
        }

    }

}

