namespace BlokOfLanguage.DataBase
{
    public class WordObject
    {
        public int WordMeaning_ID { get; set; }

        public int BaseLanguageWord_ID { get; set; }

        public int TranslateWord_ID { get; set; }

        public string TranslatedWord { get; set; }

        public string BaseLanguageWord { get; set; }

        public string PartOfSpeech { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string DifficultLevel { get; set; }

        public string Description { get; set; }

        public bool IsDifficultWord { get; set; }

        public bool IsFavourite { get; set; }

    }
}
