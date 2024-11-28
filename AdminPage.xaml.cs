using JRojas;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace JRojas
{
    public partial class AdminPage : ContentPage
    {
        private DatabaseService _databaseService;

        public AdminPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserDatabase.db3"));
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = await _databaseService.GetUserAsync(usernameEntry.Text);

            if (user != null && user.Password == passwordEntry.Text)
            {
                await DisplayAlert("Login Exitoso", "Bienvenido", "OK");
                await Navigation.PushModalAsync(new SelectAction());
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
            Debug.WriteLine($"El Nombre de usuario ingresado es: {usernameEntry.Text}");
        }
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AdminRegister());

        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }

}

