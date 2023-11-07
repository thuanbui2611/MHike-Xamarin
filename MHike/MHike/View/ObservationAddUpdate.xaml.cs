using MHike.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MHike.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObservationAddUpdate : ContentPage
    {
        ObservationModel _observation;
        HikeModel _hike;
        public ObservationAddUpdate(HikeModel hike)
        {
            InitializeComponent();
            _hike = hike;
        }
        public ObservationAddUpdate(ObservationModel observation)
        {
            InitializeComponent();
            string[] dateTime = observation.Time.Split(',');
            Title = "Edit Observation";
            _observation = observation;
            nameEntry.Text = observation.Name;
            datePicker.Date = DateTime.Parse(dateTime[1]);
            timePicker.Time = TimeSpan.Parse(dateTime[0]);
            commentEntry.Text = observation.Comment;
            nameEntry.Focus();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await DisplayAlert("Invalid", "Blank or Whitespace value is Invalid", "OK");
            }
            else if (_observation != null)
            {
                EditObservation();
            }
            else
            {
                AddNewObservation();
            }
        }

        private async void EditObservation()
        {
            string dateTime = timePicker.Time.ToString() + ", " + datePicker.Date.ToShortDateString();
            _observation.Name = nameEntry.Text;
            _observation.Time = dateTime;
            _observation.Comment = commentEntry.Text;
            //created_At?
            await App.MyDatabase.UpdateObservation(_observation);
            await Navigation.PopAsync();
        }

        private async void AddNewObservation()
        {
            DateTime date = datePicker.Date;
            var time = timePicker.Time;
            await App.MyDatabase.CreateObservation(new ObservationModel
            {
                HikeID = _hike.Id,
                Name = nameEntry.Text,
                Time = time.ToString() + ", " + date.ToShortDateString(),
                Comment = commentEntry.Text,
                CreatedAt = DateTime.Now.ToString("hh:mm"),
                LastUpdated = DateTime.Now.ToString("hh:mm")
            });
            await Navigation.PopAsync();
        }
    }
}