using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System;
using System.Reflection;

namespace JRojas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDashboardPage : ContentPage
    {
        private CandidateService _candidateService;
        private string _imagePath;

        public AdminDashboardPage()
        {
            InitializeComponent();
            _candidateService = new CandidateService();
        }

        private async void OnSelectPhotoClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Seleccionar una foto"
            });

            if (result != null)
            {
                _imagePath = result.FullPath;
                SelectedImage.Source = ImageSource.FromFile(_imagePath);
            }
        }

        private void OnAddCandidateClicked(object sender, EventArgs e)
        {
            var candidateName = CandidateNameEntry.Text;
            var candidatePosition = CandidatePositionEntry.Text;
            var visibility = VisibilityPicker.SelectedItem?.ToString(); // Obtén la selección del Picker

            if (string.IsNullOrEmpty(candidateName) || string.IsNullOrEmpty(candidatePosition) || string.IsNullOrEmpty(_imagePath) || string.IsNullOrEmpty(visibility))
            {
                DisplayAlert("Error", "Por favor completa todos los campos, incluyendo la visibilidad y la foto", "OK");
                return;
            }

            var newCandidate = new Candidate
            {
                Name = candidateName,
                Position = candidatePosition,
                ImagePath = _imagePath,
                Visibility = visibility // Nueva propiedad que guarda la visibilidad
            };

            _candidateService.AddCandidate(newCandidate);
            DisplayAlert("Éxito", "Candidato agregado correctamente", "OK");
        }
        
    }

}