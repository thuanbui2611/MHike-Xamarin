using MHike.Model;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MHike.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HikeDetail : ContentPage
    {
        private HikeModel _hike;
        public HikeDetail(HikeModel hike)
        {
            InitializeComponent();
            BindingContext = hike;
            _hike = hike;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ObservationCollectionView.ItemsSource = await App.MyDatabase.GetObservationByHikeId(_hike.Id);
        }

        private void ObservationCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedObservation = e.CurrentSelection.FirstOrDefault() as ObservationModel;
            if (selectedObservation != null)
            {
                ObservationCollectionView.SelectedItem = null;
                Navigation.PushAsync(new ObservationDetail(selectedObservation));
            }
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ObservationAddUpdate(_hike));
        }

        private async void SwipeItemObs_InvokedAsync_Delete(object sender, System.EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as ObservationModel;
            var result = await DisplayAlert("Delete", $"Are you sure to delete {emp.Name}?", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteObservation(emp);
                ObservationCollectionView.ItemsSource = await App.MyDatabase.GetObservationByHikeId(_hike.Id);
            }
        }

        private async void SwipeItemObs_InvokedAsync_Edit(object sender, System.EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as ObservationModel;

            await Navigation.PushAsync(new ObservationAddUpdate(emp));
        }
    }
}