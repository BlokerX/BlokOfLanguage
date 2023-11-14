using SQLite;

namespace BlokOfLanguage.DataBase.EntityObjects
{
    public class WordObject
    {
        /// <summary>
        /// Main key.
        /// </summary>
        [Indexed]
        public int WordMeaning_ID { get; set; }

        #region BaseLanguageWord

        /// <summary>
        /// Foreign key.
        /// </summary>
        [Indexed]
        public int BaseLanguageWord_ID { get; set; }

        public string BaseLanguageWord { get; set; }

        #endregion

        #region TranslatedWord

        /// <summary>
        /// Foreign key.
        /// </summary>
        [Indexed]
        public int TranslatedWord_ID { get; set; }

        public string TranslatedWord { get; set; }

        public string DifficultLevel { get; set; }

        public bool IsDifficultWord { get; set; }

        public bool IsFavourite { get; set; }

        #endregion

        public string PartOfSpeech { get; set; }
        
        public string Description { get; set; }
        
        public DateTime LastUpdateTime { get; set; }

    }
}
