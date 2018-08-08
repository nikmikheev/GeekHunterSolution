using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GeekHunterProject
{
    public class Candidate
    {
        private int id;
        private string firstName;
        private string lastName;
        private DateTime enteredDate;
        private List<Skill> skillList; 

        #region Properties Getters and Setters

        public int Id { get => id; set => id = value; }

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                this.firstName = value;
            }
        }

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
        public Candidate() {
            this.skillList = new List<Skill>();
        }

        public Candidate(string firstName, string lastName, DateTime enteredDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.enteredDate = enteredDate;
            this.skillList = new List<Skill>();
    }

    public Candidate(int id, string firstName, string lastName, DateTime enteredDate)
        {
            this.Id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.enteredDate = enteredDate;
            this.skillList = new List<Skill>();
        }

     }

}

