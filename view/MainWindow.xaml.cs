using System;
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

        private void RefreshDataGrid()
        {
            candidateGrid.ItemsSource = null;
            candidateGrid.ItemsSource = ((GeekHunterApp)Application.Current).Candidates.AllCandidates();
        }

        private void OpenAddCandidateWindow(object sender, RoutedEventArgs e)
        {
            var newCandidate = new Candidate("New", "Candidate", DateTime.Now);
            var addCandidateWindow = new AddCandidateWindow(newCandidate);
            if (addCandidateWindow.ShowDialog() == true)
            {
                newCandidate = addCandidateWindow.CurrentCandidate;
                ((GeekHunterApp)Application.Current).Candidates.AddCandidate(newCandidate);
                RefreshDataGrid();
            }
        }


        private void OpenEditCandidateWindow(object sender, RoutedEventArgs e)
        {
            var editCandidate = (Candidate)candidateGrid.SelectedItem;
            if (editCandidate != null)
            {
                var editCandidateWindow = new AddCandidateWindow(editCandidate);
                if (editCandidateWindow.ShowDialog() == true)
                {
                    editCandidate = editCandidateWindow.CurrentCandidate;
                    ((GeekHunterApp)Application.Current).Candidates.EditCandidate(editCandidate);
                    RefreshDataGrid();
                }
            }
        }


        private void CandidateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CandidateSkillGrid.ItemsSource = ((Candidate)e.AddedItems[0]).SkillList;
        }
    }
}