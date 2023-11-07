using MHike.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MHike.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HikeAddUpdate : ContentPage
    {

        HikeModel _hike;

        public HikeAddUpdate()
        {
            InitializeComponent();
        }
        public HikeAddUpdate(HikeModel hike)
        {
            InitializeComponent();
            Title = "Edit Hike";
            _hike = hike;
            nameEntry.Text = hike.Name;
            datePicker.Date = DateTime.Parse(hike.Date);
            locationEntry.Text = hike.Location;
            lengthEntry.Text = hike.Length;
            parkingEntry.Text = hike.Parking;
            levelEntry.Text = hike.Level;
            descriptionEntry.Text = hike.Description;
            nameEntry.Focus();

        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhiteSpace value is Invalid", "OK");
            }
            else if (_hike != null)
            {
                EditHike();
            }
            else
            {
                AddNewHike();
            }
        }

        private async void EditHike()
        {
            _hike.Name = nameEntry.Text;
            _hike.Date = datePicker.Date.ToShortDateString();
            _hike.Length = lengthEntry.Text;
            _hike.Location = locationEntry.Text;
            _hike.Description = descriptionEntry.Text;
            _hike.Level = levelEntry.Text;
            _hike.Parking = parkingEntry.Text;
            //_hike.CreatedAt = 
            await App.MyDatabase.UpdateHike(_hike);
            await Navigation.PopAsync();
        }

        private async void AddNewHike()
        {
            DateTime date = datePicker.Date;
            await App.MyDatabase.CreateHike(new HikeModel
            {
                Name = nameEntry.Text,
                Date = date.ToShortDateString(),
                Location = locationEntry.Text,
                Length = lengthEntry.Text,
                Level = levelEntry.Text,
                Parking = parkingEntry.Text,
                Description = descriptionEntry.Text,
                CreatedAt = DateTime.Now.ToString("hh:mm"),
                LastUpdated = DateTime.Now.ToString("hh:mm")
            });
            await Navigation.PopAsync();
        }
    }
}
