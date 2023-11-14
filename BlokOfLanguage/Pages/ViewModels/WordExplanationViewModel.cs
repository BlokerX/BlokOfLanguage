using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;

namespace BlokOfLanguage.Pages.ViewModels
{
    public class WordExplanationViewModel : ViewModel
    {
        public WordExplanationViewModel() { }

        public WordExplanationViewModel(WordObject word)
        {
            Word = word;
        }

        public WordObject Word { get; set; } = new WordObject();

        public string ID { get => "[" + Word.TranslatedWord_ID + "] Poziom: " + Word.DifficultLevel; }
        public string TranslateWordLine { get => Word.TranslatedWord + " [" + Word.PartOfSpeech + "] <3 !!"; }
        public string BaseLanguageWordLine { get => "1." + Word.BaseLanguageWord; }
        public string Description { get => Word.Description; }
        public string LastUpdate { get => "Data dodania: " + Word.LastUpdateTime.ToString(); }

        public async Task DeleteFromDataBase()
        {
            // usuwanie word meaning
            var wmI = await Constants.DB.GetWordMeaningAsync(Word.WordMeaning_ID);
            await Constants.DB.DeleteObjectAsync(wmI);

            // usuwanie base language word jeśli nikt go więcej nie używa
            if (Constants.DB.SelectQueryAboutWordMeaningObjectsAsync($"SELECT * FROM WordMeaning WHERE BaseLanguageWord_ID={Word.BaseLanguageWord_ID};").Result.Count == 0)
            {
                var blwI = await Constants.DB.GetBaseLanguageWordAsync(Word.BaseLanguageWord_ID);
                await Constants.DB.DeleteObjectAsync(blwI);
            }

            // usuwanie translated word jeśli nikt go więcej nie używa
            if (Constants.DB.SelectQueryAboutTranslatedWordObjectsAsync($"SELECT * FROM WordMeaning WHERE TranslatedWord_ID={Word.TranslatedWord_ID};").Result.Count == 0)
            {
                var twI = await Constants.DB.GetTranslatedWordAsync(Word.TranslatedWord_ID);
                await Constants.DB.DeleteObjectAsync(twI);
            }
        }

        public void EditInDataBase()
        {

        }
    }
}
