using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;

namespace BlokOfLanguage.Pages.Views;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        Words = Constants.DB.GetWordObjectsAsync().Result;
        LastWordsList.ItemsSource = Words; // dodać architekturę mvvm
    }

    private List<WordObject> Words { get; set; }

}

