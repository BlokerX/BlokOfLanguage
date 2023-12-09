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
            SortButtonCommand = new Command(Sort);
        }

        public ICommand RefreshButtonCommand { get; set; }
        public ICommand SortButtonCommand { get; set; }
        public void Sort() => FilterVisible = !FilterVisible;


        private bool _filterVisible = false;

        public bool FilterVisible
        {
            get => _filterVisible;
            set
            {
                _filterVisible = value;
                OnPropertyChanged(nameof(FilterVisible));
            }
        }

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


        private Filters _filter = Filters.TranslatedWordAsc;

        public Filters Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged(nameof(Filter));
                RefreshList();
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

        private bool _isSearch;

        public bool IsSearch
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
                switch (Filter)
                {
                    case Filters.TranslatedWordAsc:
                        Words = Constants.DB.GetWordObjectsOrderByTranslatedWordAsync(true).Result;
                        break;
                    case Filters.BaseLanguageWordAsc:
                        //Words = Constants.DB.GetWordObjectsByBaseLanguageWordAsync(true).Result;break;
                    case Filters.DateTimeAsc://break;
                    default: 
                Words = Constants.DB.GetWordObjectsAsync().Result;
                        break;
                }

                IsSearch = false;
            }
        }

        public enum Filters
        {
            TranslatedWordAsc,
            TranslatedWordDesc,

            BaseLanguageWordAsc,
            BaseLanguageWordDesc,

            DateTimeAsc,
            DateTimeDesc
        }

    }
}
