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

        private string _partOfSpeech;
        public string PartOfSpeech
        {
            get => _partOfSpeech;
            set
            {
                _partOfSpeech = value;
                OnPropertyChanged(nameof(PartOfSpeech));
            }
        }

        private string _difficultLevel;
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
            if (TranslatedWord == string.Empty || BaseLanguageWord == string.Empty)
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
            // Base Language Word //
            var q1 = $"SELECT * FROM BaseLanguageWord WHERE Word like '{BaseLanguageWord}';";

            var baseLanguageWords = Constants.DB.SelectQueryAboutBaseLanguageWordObjectsAsync(q1).Result;

            int baseLanguageWord_ID = 0;

            if (baseLanguageWords.Count > 0)
                baseLanguageWord_ID = baseLanguageWords.First().ID;
            else
            {
                BaseLanguageWord b;
                await Constants.DB.InsertObjectAsync(b = CreateBaseLanguageWordObject());
                baseLanguageWord_ID = b.ID;
            }

            // Translated Word //
            var q2 = $"SELECT * FROM TranslatedWord WHERE Word like '{TranslatedWord}';";

            var translatedWords = Constants.DB.SelectQueryAboutTranslatedWordObjectsAsync(q2).Result;

            int translateWord_ID = 0;

            if (translatedWords.Count > 0)
                translateWord_ID = translatedWords.First().ID;
            else
            {
                TranslatedWord t;
                await Constants.DB.InsertObjectAsync(t = CreateTranslatedWordObject());
                translateWord_ID = t.ID;
            }

            // Word Meaning - Relation //

            var q3 = $"SELECT * FROM WordMeaning WHERE " +
                     $"BaseLanguageWord_ID={baseLanguageWord_ID} AND " +
                     $"TranslatedWord_ID={translateWord_ID}";
            if (PartOfSpeech != null) q3 += $" AND PartOfSpeech like '{PartOfSpeech}'";
            q3 += ";";
            var wordMeanings = Constants.DB.SelectQueryAboutWordMeaningObjectsAsync(q3).Result;

            if (wordMeanings.Count == 0)
                await Constants.DB.InsertObjectAsync(CreateWordMeaningObject(baseLanguageWord_ID, translateWord_ID));

            Debug.WriteLine("[Database Insert] Successfull!");
        }

        private void ResetForm()
        {
            TranslatedWord = string.Empty;
            BaseLanguageWord = string.Empty;
            PartOfSpeech = string.Empty;
            DifficultLevel = string.Empty;
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
