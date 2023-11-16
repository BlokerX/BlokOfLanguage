using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;
using System.Windows.Input;

namespace BlokOfLanguage.Pages.ViewModels
{
    public class WordListViewModel : ViewModel
    {
        public WordListViewModel()
        {
            RefreshButtonCommand = new Command(RefreshList);
        }

        public ICommand RefreshButtonCommand { get; set; }

        private List<WordObject> _words;

        public List<WordObject> Words
        {
            get => _words;
            set
            {
                _words = value;
                OnPropertyChanged(nameof(Words));
            }
        }

        private string _searchBarText;

        public string SearchBarText
        {
            get => _searchBarText;
            set
            {
                _searchBarText = value;
                OnPropertyChanged(nameof(SearchBarText));
            }
        }

        private bool? _isSearch;

        public bool? IsSearch
        {
            get => _isSearch;
            set
            {
                _isSearch = value;
                OnPropertyChanged(nameof(IsSearch));
            }
        }

        public void RefreshList()
        {
            if (SearchBarText != null && SearchBarText != string.Empty)
            {
                Words = Constants.DB.GetWordObjectsByWordAsync(SearchBarText).Result;
                IsSearch = true;
            }
            else
            {
                Words = Constants.DB.GetWordObjectsAsync().Result;
                IsSearch = false;
            }
        }

    }
}
