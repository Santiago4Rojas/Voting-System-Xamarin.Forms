using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace JRojas
{
    public partial class StudentPage : ContentPage
    {
        private CandidateService _candidateService;
        public ObservableCollection<Candidate> Candidates { get; set; }

        public StudentPage()
        {
            InitializeComponent();
            _candidateService = new CandidateService();
            LoadCandidates();
        }

        private void LoadCandidates()
        {
            var candidates = _candidateService.GetCandidates();

            // Filtra candidatos según la visibilidad en StudentPage
            Candidates = new ObservableCollection<Candidate>(candidates.Where(c => c.Visibility == "Student"));
            candidatesCollectionView.ItemsSource = Candidates;
        }


        private void OnVoteCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var selectedCandidate = (Candidate)checkBox.BindingContext;

            // Marcar o desmarcar el candidato seleccionado
            if (selectedCandidate.IsSelected) ;
            {
                // Desmarcar los demás candidatos
                foreach (var candidate in Candidates)
                {
                    if (candidate != selectedCandidate)
                    {
                        candidate.IsSelected = false; // Desmarcar los otros
                    }
                }
            }
        }

        private Counter _counter = new Counter(); // Instancia de Counter

        private async void OnRegisterVoteClicked(object sender, EventArgs e)
        {
            var documentNumber = studentCodeEntry.Text;
            var selectedCandidate = Candidates.FirstOrDefault(c => c.IsSelected);

            if (selectedCandidate != null)
            {
                try
                {
                    // Registrar el voto en Counter
                    _counter.AddStudentVote(documentNumber, selectedCandidate.Name);
                    await DisplayAlert("Voto Registrado", $"Has votado por: {selectedCandidate.Name}", "OK");
                }
                catch (ArgumentException ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor selecciona un candidato", "OK");
            }
            Debug.WriteLine($"El codigo del estudiante es: {studentCodeEntry.Text}");
        }
        private void OnCandidateSelected(object sender, SelectionChangedEventArgs e)
        {
            // Obtener el candidato seleccionado
            var selectedCandidate = e.CurrentSelection.FirstOrDefault() as Candidate;

            // Marcar el candidato seleccionado y desmarcar los demás
            foreach (var candidate in Candidates)
            {
                candidate.IsSelected = candidate == selectedCandidate;
            }
        }
    }
}