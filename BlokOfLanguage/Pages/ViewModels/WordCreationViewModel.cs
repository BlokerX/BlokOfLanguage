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

        public void AddButtonClicked(out WordObject wordObject)
        {
            Debug.WriteLine(
                $"[ADD] {TranslatedWord} " +
                $"{BaseLanguageWord} " +
                $"{PartOfSpeech} " +
                $"{DifficultLevel} " +
                $"{Description} " +
                $"{IsFavourite} " +
                $"{IsDifficultWord}");

            wordObject = new WordObject()
            {
                TranslatedWord = TranslatedWord,
                BaseLanguageWord = BaseLanguageWord,
                Description = Description,
                DifficultLevel = DifficultLevel,
                IsDifficultWord = IsDifficultWord,
                IsFavourite = IsFavourite,
                LastUpdateTime = DateTime.Now,
                PartOfSpeech = PartOfSpeech,
            };

            // todo dodać dodawanie do bazy danych jeśli
            // rekordy w jednej tableli już są
            // to dopisać tylko te których nie ma 

            ResetForm();
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
