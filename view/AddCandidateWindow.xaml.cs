using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;

namespace GeekHunterProject
{
    public partial class AddCandidateWindow : Window
    {
        private Candidate currentCandidate;

        public AddCandidateWindow()
        {
            InitializeComponent();
        }

        public Candidate CurrentCandidate
        {
            get => currentCandidate;
            set => currentCandidate = value;
        }

        private void OnInit(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Candidate("New", "Candidate", DateTime.Now);
            CurrentCandidate = (Candidate)(this.DataContext);
        }

        private void SubmitCandidate(object sender, RoutedEventArgs e)
        {
            CurrentCandidate = (Candidate)(this.DataContext);
            this.DialogResult = true;
            this.Close();
        }
    }
}