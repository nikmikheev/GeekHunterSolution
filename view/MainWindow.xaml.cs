using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

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
            Candidate newCandidate = new Candidate("New", "Candidate", DateTime.Now);
            AddCandidateWindow addCandidateWindow = new AddCandidateWindow(newCandidate);
            if (addCandidateWindow.ShowDialog() == true)
            {
                newCandidate = addCandidateWindow.CurrentCandidate;
                ((GeekHunterApp)Application.Current).Candidates.AddCandidate(newCandidate);
                candidateGrid.ItemsSource = null;
                candidateGrid.ItemsSource = ((GeekHunterApp)Application.Current).Candidates.AllCandidates();

            }
        }

        private void OpenEditCandidateWindow(object sender, RoutedEventArgs e)
        {
            Candidate editCandidate = (Candidate)candidateGrid.SelectedItem;
            if (editCandidate != null)
            {
                AddCandidateWindow editCandidateWindow = new AddCandidateWindow(editCandidate);
                if (editCandidateWindow.ShowDialog() == true)
                {
                    editCandidate = editCandidateWindow.CurrentCandidate;
                    ((GeekHunterApp)Application.Current).Candidates.EditCandidate(editCandidate);
                }
            }
        }

        private void OpenAddCandidateSkillWindow(object sender, RoutedEventArgs e)
        {
            Candidate newCandidate = new Candidate("New", "Candidate", DateTime.Now);
            AddCandidateWindow addCandidateWindow = new AddCandidateWindow(newCandidate);
            if (addCandidateWindow.ShowDialog() == true)
            {
                newCandidate = addCandidateWindow.CurrentCandidate;
                ((GeekHunterApp)Application.Current).Candidates.AddCandidate(newCandidate);
                candidateGrid.ItemsSource = null;
                candidateGrid.ItemsSource = ((GeekHunterApp)Application.Current).Candidates.AllCandidates();
            }
        }

        private void CandidateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            candidateSkillGrid.ItemsSource = ((Candidate)e.AddedItems[0]).SkillList;
        }
    }
}