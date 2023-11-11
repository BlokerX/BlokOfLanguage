using BlokOfLanguage.DataBase;

namespace BlokOfLanguage;

public partial class WordsListPage : ContentPage
{
	public WordsListPage()
	{
		InitializeComponent();

		Words = Constants.DB.GetWordObjects().Result;

		ListOfItems.ItemsSource = Words; // dodać architekturę mvvm
	}

    List<WordObject> Words { get; set; }
}