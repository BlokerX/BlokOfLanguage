using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views;

public partial class WordExplanationPage : ContentPage
{
    public WordExplanationPage()
    {
        InitializeComponent();
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (await DisplayAlert("Are you sure you want to delete this word?", "This action cannot be undone.", "Yes", "No"))
        {
            await (BindingContext as WordExplanationViewModel)?.DeleteFromDataBase();
            Navigation.RemovePage(this);
        }
    }
}