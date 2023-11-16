using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;
using System.Diagnostics;

namespace BlokOfLanguage.Pages.Views;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private List<WordObject> Words { get; set; }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            Words = await Constants.DB.GetWordObjectsAsync();
            LastWordsList.ItemsSource = Words; // dodać architekturę mvvm
        }
        catch (Exception ex) { Debug.WriteLine("[EXCEPTION]: " + ex); }
    }
}

