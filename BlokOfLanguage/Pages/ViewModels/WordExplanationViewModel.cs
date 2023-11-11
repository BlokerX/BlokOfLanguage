using BlokOfLanguage.DataBase.EntityObjects;

namespace BlokOfLanguage.Pages.ViewModels
{
    public class WordExplanationViewModel
    {
        public WordExplanationViewModel(WordObject word)
        {
            Word = word;
        }

        public WordObject Word { get; set; } = new WordObject();

        public string ID { get => "[" + Word.TranslateWord_ID + "] Poziom: " + Word.DifficultLevel; }
        public string TranslateWordLine { get => Word.TranslatedWord + " [" + Word.PartOfSpeech + "] &lt;3 !!"; }
        public string BaseLanguageWordLine { get => "1." + Word.BaseLanguageWord; }
        public string Description { get => Word.Description; }
        public string LastUpdate { get => "Data dodania: " + Word.LastUpdateTime.ToString(); }
    }
}
