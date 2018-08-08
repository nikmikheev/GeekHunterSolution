using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;

namespace GeekHunterProject
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void OpenAddCandidateWindow(object sender, RoutedEventArgs e)
        {
            AddCandidateWindow addCandidateWindow = new AddCandidateWindow();
            if (addCandidateWindow.ShowDialog() == true) {
                Candidate newCandidate = addCandidateWindow.CurrentCandidate;
                ((GeekHunterApp)Application.Current).Candidates.AddCandidate(newCandidate);
                //  ((GeekHunterApp)Application.Current).Candidates.CollectionChanged Helper.AddCandidate(newCandidate);
            }
        }

        private void OpenAddCandidateSkillWindow(object sender, RoutedEventArgs e)
        {
            AddCandidateWindow addCandidateWindow = new AddCandidateWindow();
            if (addCandidateWindow.ShowDialog() == true)
            {
                Candidate newCandidate = addCandidateWindow.CurrentCandidate;
                ((GeekHunterApp)Application.Current).Candidates.AddCandidate(newCandidate);
                //  ((GeekHunterApp)Application.Current).Candidates.CollectionChanged Helper.AddCandidate(newCandidate);
            }
        }

        private void CandidateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selected = candidateGrid.SelectedItem;
            //if (selected is DataRowView)
            //{
            //    int index = Convert.ToInt32(((DataRowView)selected).Row.ItemArray[0]);
            //    candidateSkillGrid.ItemsSource = ((GeekHunterApp)Application.Current).CandidateSkill.GetDataTableCandidateSkills(index).DefaultView;
            //}

            candidateSkillGrid.ItemsSource = ((Candidate)e.AddedItems[0]).SkillList;
        }
    }
}