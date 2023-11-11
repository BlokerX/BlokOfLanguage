using SQLite;

namespace BlokOfLanguage.DataBase
{
    public class WordMeaning
    {
        #region Keys

        /// <summary>
        /// Primary key.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Foreign key.
        /// </summary>
        [Indexed]
        public int BaseLanguageWord_ID { get; set; }

        /// <summary>
        /// Foreign key.
        /// </summary>
        [Indexed]
        public int TranslatedWord_ID { get; set; }

        #endregion

        public string PartOfSpeech { get; set; }

        public string TranslatedWord { get; set; }

        public string Description { get; set; }

        //public List<string> ExampleSentences { get; set; }

        //public List<string> Synonyms { get; set; }

        //public List<string> Antonyms { get; set; }

        //public List<string> RelatedWords { get; set; }

        public DateTime LastUpdateTime { get; set; }

        // todo zmienić //
        public bool Done { get; internal set; }
    }
}