using ECSystem.Telemetry.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ECSystem.Telemetry.Views {
    public partial class ItemDetailPage : ContentPage {
        public ItemDetailPage() {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}