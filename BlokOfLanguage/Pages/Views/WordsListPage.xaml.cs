using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;
using BlokOfLanguage.Pages.ViewModels;

namespace BlokOfLanguage.Pages.Views;

public partial class WordsListPage : ContentPage
{
    public WordsListPage()
    {
        InitializeComponent();
    }

    private void WordsListPage_Loaded(object sender, EventArgs e)
    {
        RefreshList();
    }

    private void RefreshButton_Clicked(object sender, EventArgs e)
    {
        RefreshList();
    }

    public void RefreshList()
    {
        if (SearchEntry.Text != null)
            Words = Constants.DB.GetWordObjectsByWordAsync(SearchEntry.Text).Result;
        else
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

    private void SearchButton_Clicked(object sender, EventArgs e)
    {
        RefreshList();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        RefreshList();
    }
}