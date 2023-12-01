using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            BindingContext = new SettingsViewModel(this);
            InitializeComponent();
        }
    }
}
