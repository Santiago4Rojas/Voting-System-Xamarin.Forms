using System;
using Xamarin.Forms;

namespace JRojas
{
    public partial class SelectAction : ContentPage
    {
        public SelectAction()
        {
            InitializeComponent();
        }

        private async void OnOption1Clicked(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new AdminDashboardPage());
        }

        private async void OnOption2Clicked(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new Counter());

        }
    }
}
