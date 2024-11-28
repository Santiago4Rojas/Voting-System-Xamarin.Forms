using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace JRojas
{
    public partial class TeacherPage : ContentPage
    {
        private CandidateService _candidateService;

        public ObservableCollection<Candidate> Candidates { get; set; }

        public TeacherPage()
        {
            InitializeComponent();
            _candidateService = new CandidateService();
            LoadCandidates();
        }

        private void LoadCandidates()
        {
            var candidates = _candidateService.GetCandidates();

            // Filtra candidatos según la visibilidad en TeacherPage
            Candidates = new ObservableCollection<Candidate>(candidates.Where(c => c.Visibility == "Teacher"));
            candidatesCollectionView.ItemsSource = Candidates;
        }

        private void OnVoteCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var selectedCandidate = (Candidate)checkBox.BindingContext;

            // Marcar o desmarcar el candidato seleccionado
            if (selectedCandidate.IsSelected)
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
            // Validación del número de documento
            var documentNumber = documentNumberEntry.Text;
            if (string.IsNullOrWhiteSpace(documentNumber) || documentNumber.Length < 7 || documentNumber.Length > 10 || !documentNumber.All(char.IsDigit))
            {
                await DisplayAlert("Error", "El número de documento debe contener entre 7 y 10 dígitos numéricos.", "OK");
                return;
            }

            // Validación del programa
            var selectedProgram = programPicker.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedProgram))
            {
                await DisplayAlert("Error", "Por favor seleccione un programa.", "OK");
                return;
            }

            // Obtener el candidato seleccionado
            var selectedCandidate = Candidates.FirstOrDefault(c => c.IsSelected);
            if (selectedCandidate != null)
            {
                // Registrar el voto en Counter
                _counter.AddTeacherVote(selectedProgram, selectedCandidate.Name);

                // Mensaje de confirmación
                await DisplayAlert("Voto Registrado", $"Has votado por: {selectedCandidate.Name}\nDocumento: {documentNumber}\nPrograma: {selectedProgram}", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Por favor selecciona un candidato", "OK");
            }
            Debug.WriteLine($"El numero de documento ingresado es: {documentNumberEntry.Text}");
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