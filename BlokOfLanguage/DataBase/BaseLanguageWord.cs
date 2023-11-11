using SQLite;

namespace BlokOfLanguage.DataBase
{
    public class BaseLanguageWord
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Word { get; set; }
    }
}
