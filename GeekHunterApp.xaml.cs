using System.Windows;

namespace GeekHunterProject
{
    public partial class GeekHunterApp : Application
    {
        public CandidateHelper Candidates { get; private set; }

        void AppStartup(object sender, StartupEventArgs args)
        {
            this.Candidates = new CandidateHelper();
            MainWindow mainWindow = new MainWindow();
            mainWindow.candidateGrid.ItemsSource = Candidates.AllCandidates();
            mainWindow.Show();
        }

    }
}