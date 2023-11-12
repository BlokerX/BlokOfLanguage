using BlokOfLanguage.DataBase.EntityObjects;
using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views;

public partial class WordCreationForm : ContentPage
{
    public WordCreationForm()
    {
        InitializeComponent();
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        var w = new WordObject();
        (BindingContext as WordCreationViewModel)?.AddButtonClicked(out w);

        Navigation.PushAsync(new WordExplanationPage()
        { BindingContext = new WordExplanationViewModel(w) });
    }

}