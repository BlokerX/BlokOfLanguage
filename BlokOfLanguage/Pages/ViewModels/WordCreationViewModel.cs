using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;

namespace BlokOfLanguage.Pages.ViewModels
{
    class WordCreationViewModel
    {
        public string TranslatedWord { get; set; }
        public string BaseLanguageWord { get; set; }

        public string PartOfSpeech { get; set; }
        public string DifficultLevel { get; set; }
        public string Description { get; set; }

        public bool IsFavourite { get; set; }
        public bool IsDifficultWord { get; set; }

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
