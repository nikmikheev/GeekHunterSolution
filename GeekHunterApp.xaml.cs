using System.Windows;

namespace GeekHunterProject
{
    public partial class GeekHunterApp : Application
    {
        public CandidateHelper Candidates { get; private set; }

        private void AppStartup(object sender, StartupEventArgs args)
        {
            Candidates = new CandidateHelper();
            var mainWindow = new MainWindow();
            mainWindow.candidateGrid.ItemsSource = Candidates.AllCandidates();
            mainWindow.Show();
        }

    }
}