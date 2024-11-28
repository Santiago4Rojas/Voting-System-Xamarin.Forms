using System;
using Xamarin.Forms;

namespace JRojas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnStudentClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new StudentPage());
        }

        private void OnTeacherClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TeacherPage());
        }

        private void OnAdminClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AdminPage());
        }
    }
}