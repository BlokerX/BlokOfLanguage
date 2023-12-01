namespace BlokOfLanguage.DataBase
{
    public static class Constants
    {
        public const string DatabaseFilename = "BlokOfLanguageSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache /*| 
        
            SQLite.SQLiteOpenFlags.ProtectionNone*/
        ;
        //todo dodać magazyn zewnętrzny aby był ciągły dostęp
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        private static BlokOfLanguageDatabase _db;

        public static BlokOfLanguageDatabase DB
        {
            get { return _db; }
        }

        public static void LoadDataBase()
        {
            _db = new BlokOfLanguageDatabase();
        }

    }
}
