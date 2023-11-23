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

    private void LastWordsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        LastWordsList.SelectedItem = null;
    }

    private void LastWordsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var w = (WordObject)e.Item;
        Navigation.PushAsync(new WordExplanationPage() { BindingContext = new WordExplanationViewModel(w) });
    }
}

