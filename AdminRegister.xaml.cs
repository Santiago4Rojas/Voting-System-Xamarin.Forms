using JRojas;
using System;
using System.IO;
using Xamarin.Forms;
using System.Diagnostics;


namespace JRojas
{
    public partial class AdminRegister : ContentPage
    {
        private DatabaseService _databaseService;

        public AdminRegister()
        {
            InitializeComponent();
            _databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserDatabase.db3"));
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor completa todos los campos", "OK");
                return;
            }

            // Verificar si el email contiene el carácter especial '@'
            if (!emailEntry.Text.Contains("@"))
            {
                await DisplayAlert("Error", "El correo electrónico no cumple con las condiciones de un correo electronico", "OK");
                return;
            }

            var existingUser = await _databaseService.GetUserAsync(usernameEntry.Text);
            if (existingUser != null)
            {
                await DisplayAlert("Error", "Este nombre de usuario ya está en uso", "OK");
                return;
            }

            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text, // En un entorno real, asegúrate de encriptar la contraseña
                Email = emailEntry.Text
            };

            await _databaseService.SaveUserAsync(user);
            await DisplayAlert("Registro exitoso", "Te has registrado correctamente", "OK");

            // Redirigir al login o dashboard de admin
            await Navigation.PushModalAsync(new SelectAction());
            Debug.WriteLine($"El Nombre de usuario ingresado es: {usernameEntry.Text}");
            Debug.WriteLine($"El correo ingresado es: {emailEntry.Text}");
        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

}