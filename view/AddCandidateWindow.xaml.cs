using System.Windows;

namespace GeekHunterProject
{
    public partial class AddCandidateWindow : Window
    {
        public AddCandidateWindow(Candidate candidate)
        {
            this.DataContext = candidate;
            CurrentCandidate = candidate; 
            InitializeComponent();
        }

        public Candidate CurrentCandidate { get; set; }

        private void OnInit(object sender, RoutedEventArgs e)
        {
        }

        private void SubmitCandidate(object sender, RoutedEventArgs e)
        {
            CurrentCandidate = (Candidate)(this.DataContext);
            this.DialogResult = true;
            this.Close();
        }
    }
}