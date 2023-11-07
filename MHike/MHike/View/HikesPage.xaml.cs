using MHike.Model;
using MHike.View;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MHike
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HikesPage : ContentPage
    {

        public HikesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                hikeCollectionView.ItemsSource = await App.MyDatabase.GetAllHike();
            }
            catch (Exception ex)
            {
            }
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HikeAddUpdate());
        }

        async void SwipeItem_InvokedAsync_Edit(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as HikeModel;
            await Navigation.PushAsync(new HikeAddUpdate(emp));
        }
        async void SwipeItem_InvokedAsync_Delete(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as HikeModel;
            var result = await DisplayAlert("Delete", $"Are you sure to delete {emp.Name}?", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteHike(emp);
                hikeCollectionView.ItemsSource = await App.MyDatabase.GetAllHike();
            }
        }

        async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            hikeCollectionView.ItemsSource = await App.MyDatabase.Search(e.NewTextValue);
        }

        private void hikeCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedHike = e.CurrentSelection.FirstOrDefault() as HikeModel;
            if (selectedHike != null)
            {
                hikeCollectionView.SelectedItem = null;
                Navigation.PushAsync(new HikeDetail(selectedHike));
            }
        }
    }
}