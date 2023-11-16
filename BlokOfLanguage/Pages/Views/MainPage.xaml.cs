using BlokOfLanguage.DataBase.EntityObjects;
using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await ((MainViewModel)BindingContext)?.LoadLastWordsList();
    }
}

