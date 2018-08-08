using System.Windows;

namespace GeekHunterProject
{
    public partial class AddCandidateWindow : Window
    {
        public AddCandidateWindow(Candidate candidate)
        {
            DataContext = candidate;
            CurrentCandidate = candidate; 
            InitializeComponent();
            CandidateSkillGrid.ItemsSource = CurrentCandidate.SkillList;
            AllSkillGrid.ItemsSource = CurrentCandidate.NoSkillList;
        }

        public Candidate CurrentCandidate { get; set; }

        private void OnInit(object sender, RoutedEventArgs e)
        {
        }

        private void SubmitCandidate(object sender, RoutedEventArgs e)
        {
            CurrentCandidate = (Candidate)DataContext;
            DialogResult = true;
            Close();
        }

        private void ButtonDeleteSkill_Click(object sender, RoutedEventArgs e)
        {
            var currentSkill = (Skill)CandidateSkillGrid.SelectedItem;
            if (currentSkill != null)
            {
                CurrentCandidate.NoSkillList.Add(currentSkill);
                CurrentCandidate.SkillList.Remove(currentSkill);
                RefreshDataGrids();
            }
        }

        private void ButtonAddSkill_Click(object sender, RoutedEventArgs e)
        {
            var currentSkill = (Skill)AllSkillGrid.SelectedItem;
            if (currentSkill != null)
            {
                CurrentCandidate.SkillList.Add(currentSkill);
                CurrentCandidate.NoSkillList.Remove(currentSkill);
                RefreshDataGrids();
            }

        }

        private void RefreshDataGrids()
        {
            CandidateSkillGrid.ItemsSource = null;
            AllSkillGrid.ItemsSource = null;

            CandidateSkillGrid.ItemsSource = CurrentCandidate.SkillList;
            AllSkillGrid.ItemsSource = CurrentCandidate.NoSkillList;
        }
    }
}