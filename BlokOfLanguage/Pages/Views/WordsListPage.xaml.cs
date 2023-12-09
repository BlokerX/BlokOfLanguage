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
        ((WordListViewModel)BindingContext)?.RefreshList();
    }

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
        ((WordListViewModel)BindingContext)?.RefreshList();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((WordListViewModel)BindingContext)?.RefreshList();
    }

    private void TranslatedWordCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var vm = ((WordListViewModel)BindingContext);
        if (vm != null)
            if (e.Value)
            {
                vm.Filter = WordListViewModel.Filters.TranslatedWordAsc;
            }
            else
            {
                vm.Filter = WordListViewModel.Filters.TranslatedWordDesc;
            }
    }

    private void BaseLanguageWordCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var vm = ((WordListViewModel)BindingContext);
        if (vm != null)
            if (e.Value)
            {
                vm.Filter = WordListViewModel.Filters.BaseLanguageWordAsc;
            }
            else
            {
                vm.Filter = WordListViewModel.Filters.BaseLanguageWordDesc;
            }
    }

    private void DataCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var vm = ((WordListViewModel)BindingContext);
        if (vm != null)
            if (e.Value)
            {
                vm.Filter = WordListViewModel.Filters.DateTimeAsc;
            }
            else
            {
                vm.Filter = WordListViewModel.Filters.DateTimeDesc;
            }
    }
}