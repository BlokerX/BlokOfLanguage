using SQLite;

namespace BlokOfLanguage.DataBase
{
    public class TranslatedWord
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Word { get; set; }

        public bool IsDifficultWord { get; set; }

        public bool IsFavourite { get; set; }

        public string DifficultLevel { get; set; }

    }
}