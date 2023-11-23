using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views;

public partial class WordCreationForm : ContentPage
{
    public WordCreationForm()
    {
        InitializeComponent();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        var result = await (BindingContext as WordCreationViewModel)?.AddButtonClickedAsync();
        if(!result) await DisplayAlert("Can not create new word.","Please, write word, translation and define part of speech.", "OK");
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        (BindingContext as WordCreationViewModel)?.ResetButtonClicked();
    }
}