using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;
using System.Diagnostics;

namespace BlokOfLanguage.Pages.ViewModels
{
    public class WordCreationViewModel : ViewModel
    {
        private string _translatedWord;
        public string TranslatedWord
        {
            get => _translatedWord;
            set
            {
                _translatedWord = value;
                OnPropertyChanged(nameof(TranslatedWord));
            }
        }
        //todo usuwać graniczne spacje (z początku i końca wyrazu)
        private string _baseLanguageWord;
        public string BaseLanguageWord
        {
            get => _baseLanguageWord;
            set
            {
                _baseLanguageWord = value;
                OnPropertyChanged(nameof(BaseLanguageWord));
            }
        }

        private string _partOfSpeech = string.Empty;
        public string PartOfSpeech
        {
            get => _partOfSpeech;
            set
            {
                _partOfSpeech = value;
                OnPropertyChanged(nameof(PartOfSpeech));
            }
        }

        private string _difficultLevel = "Unknown";
        public string DifficultLevel
        {
            get => _difficultLevel;
            set
            {
                _difficultLevel = value;
                OnPropertyChanged(nameof(DifficultLevel));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private bool _isFavourite = false;
        public bool IsFavourite
        {
            get => _isFavourite;
            set
            {
                _isFavourite = value;
                OnPropertyChanged(nameof(IsFavourite));
            }
        }

        private bool _isDifficultWord = false;
        public bool IsDifficultWord
        {
            get => _isDifficultWord;
            set
            {
                _isDifficultWord = value;
                OnPropertyChanged(nameof(IsDifficultWord));
            }
        }

        public async Task<bool> AddButtonClickedAsync()
        {
            // todo uniemożliwić jeśli nazwy puste
            if (TranslatedWord == string.Empty ||
                BaseLanguageWord == string.Empty ||
                PartOfSpeech == string.Empty)
                return false;

#if DEBUG
            Debug.WriteLine(
            $"[ADD] {TranslatedWord} " +
            $"{BaseLanguageWord} " +
            $"{PartOfSpeech} " +
            $"{DifficultLevel} " +
            $"{Description} " +
            $"{IsFavourite} " +
            $"{IsDifficultWord}");
#endif

            await AddToDataBaseAsync();

            // koniecznie na końcu
            ResetForm();
            return true;
        }

        public void ResetButtonClicked()
        {
            ResetForm();
        }

        private async Task AddToDataBaseAsync()
        { // todo naprawić funkcję
#if DEBUG
            int i_debug = 0;
#endif
            // Base Language Word //
            var q1 = $"SELECT * FROM BaseLanguageWord WHERE Word like '{BaseLanguageWord}';";

            var baseLanguageWords = Constants.DB.SelectQueryAboutBaseLanguageWordObjectsAsync(q1).Result;
#if DEBUG
            Debug.WriteLine($"Polecenie [{q1}] zwróciło {baseLanguageWords.Count} wierszy");
#endif

            int baseLanguageWord_ID;
            if (baseLanguageWords.Count > 0)
                baseLanguageWord_ID = baseLanguageWords.First().ID;
            else
            {
                BaseLanguageWord b;
#if DEBUG
                i_debug =
#endif
                await Constants.DB.InsertObjectAsync(b = CreateBaseLanguageWordObject());
                baseLanguageWord_ID = b.ID;
#if DEBUG
                Debug.WriteLine($"Polecenie [CreateBaseLanguageWordObject] utworzyło {i_debug} wierszy");
#endif
            }

            // Translated Word //
            var q2 = $"SELECT * FROM TranslatedWord WHERE Word like '{TranslatedWord}';";

            var translatedWords = Constants.DB.SelectQueryAboutTranslatedWordObjectsAsync(q2).Result;
#if DEBUG
            Debug.WriteLine($"Polecenie [{q2}] zwróciło {translatedWords} wierszy");
#endif

            int translateWord_ID;
            if (translatedWords.Count > 0)
                translateWord_ID = translatedWords.First().ID;
            else
            {
                TranslatedWord t;
#if DEBUG
                i_debug =
#endif
                await Constants.DB.InsertObjectAsync(t = CreateTranslatedWordObject());
                translateWord_ID = t.ID;
#if DEBUG
                Debug.WriteLine($"Polecenie [CreateTranslatedWordObject] utworzyło {i_debug} wierszy");
#endif
            }

            // Word Meaning - Relation //

            var q3 = $"SELECT * FROM WordMeaning WHERE " +
                     $"BaseLanguageWord_ID={baseLanguageWord_ID} AND " +
                     $"TranslatedWord_ID={translateWord_ID}";
            if (PartOfSpeech != null) q3 += $" AND PartOfSpeech like '{PartOfSpeech}'";
            q3 += ";";
            var wordMeanings = Constants.DB.SelectQueryAboutWordMeaningObjectsAsync(q3).Result;
#if DEBUG
            Debug.WriteLine($"Polecenie [{q3}] zwróciło {wordMeanings.Count} wierszy");
#endif

            if (wordMeanings.Count == 0)
            {

#if DEBUG
                i_debug =
#endif
                await Constants.DB.InsertObjectAsync(CreateWordMeaningObject(baseLanguageWord_ID, translateWord_ID));
#if DEBUG
                Debug.WriteLine($"Polecenie [CreateWordMeaningObject] utworzyło {i_debug} wierszy");
#endif
            }

        }

        private void ResetForm()
        {
            TranslatedWord = string.Empty;
            BaseLanguageWord = string.Empty;
            PartOfSpeech = string.Empty;
            DifficultLevel = "Unknown";
            Description = string.Empty;
            IsFavourite = false;
            IsDifficultWord = false;
        }

        public TranslatedWord CreateTranslatedWordObject()
        {
            return new TranslatedWord()
            {
                ID = Constants.DB.GetTranslatedWordNewIDAsync().Result,
                DifficultLevel = DifficultLevel,
                Word = TranslatedWord,
                IsDifficultWord = IsDifficultWord,
                IsFavourite = IsFavourite,
            };
        }

        public BaseLanguageWord CreateBaseLanguageWordObject()
        {
            return new BaseLanguageWord()
            {
                ID = Constants.DB.GetBaseLanguageWordNewIDAsync().Result,
                Word = BaseLanguageWord,
            };
        }

        public WordMeaning CreateWordMeaningObject(int baseLanguageWord_ID, int translatedWord_ID)
        {
            return new WordMeaning()
            {
                ID = Constants.DB.GetWordMeaningNewIDAsync().Result,
                BaseLanguageWord_ID = baseLanguageWord_ID,
                TranslatedWord_ID = translatedWord_ID,
                Description = Description,
                PartOfSpeech = PartOfSpeech,
                LastUpdateTime = DateTime.Now,
            };
        }

    }
}
