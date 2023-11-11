using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;
using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views;

public partial class WordsListPage : ContentPage
{
    public WordsListPage()
    {
        InitializeComponent();

        Words = Constants.DB.GetWordObjectsAsync().Result;

        ListOfItems.ItemsSource = Words; // dodać architekturę mvvm
    }

    private List<WordObject> Words { get; set; }

    private void ListOfItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ListOfItems.SelectedItem = null;
    }

    private void ListOfItems_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var w = (WordObject)e.Item;
        Navigation.PushAsync(new WordExplanationPage() { BindingContext = new WordExplanationViewModel(w) });
    }
}