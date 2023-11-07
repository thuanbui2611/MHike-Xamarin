using MHike.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MHike.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObservationDetail : ContentPage
    {
        public ObservationDetail(ObservationModel observation)
        {
            InitializeComponent();
            BindingContext = observation;
        }
    }
}