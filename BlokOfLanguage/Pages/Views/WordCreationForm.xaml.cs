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
        await (BindingContext as WordCreationViewModel)?.AddButtonClickedAsync();
    }

}