using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data;
using System.Xml;

namespace GeekHunterProject
{
    public partial class GeekHunterApp : Application
    {
        private DatabaseHelperClass dbHelper;
        private CandidateSkillHelper candidateSkillHelper;
        private CandidateHelper candidateHelper;

        void AppStartup(object sender, StartupEventArgs args)
        {

            this.dbHelper = new DatabaseHelperClass();
            this.candidateHelper = new CandidateHelper(dbHelper);
            this.candidateSkillHelper = new CandidateSkillHelper(dbHelper);

            MainWindow mainWindow = new MainWindow();
            //mainWindow.candidateGrid.ItemsSource = candidateHelper.GetDataTableCandidate().Tables[0].DefaultView;
            //mainWindow.candidateSkillGrid.ItemsSource = candidateSkillHelper.GetDataTableCandidateSkills(1).DefaultView;
            mainWindow.candidateGrid.ItemsSource = candidateHelper.Candidates;
            mainWindow.Show();
        }

        public DatabaseHelperClass DBHelper { get => dbHelper;}
        public CandidateSkillHelper CandidateSkill { get => candidateSkillHelper; }
        public CandidateHelper Candidates { get => candidateHelper; }
    }
}