using SQLite;
using System.Diagnostics;

namespace BlokOfLanguage.DataBase
{
    public class BlokOfLanguageDatabase
    {
        SQLiteAsyncConnection Database;

        public BlokOfLanguageDatabase()
        {
            testFn();
        }

        private async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            var result1 = await Database.CreateTableAsync<WordMeaning>();
            var result2 = await Database.CreateTableAsync<TranslatedWord>();
            var result3 = await Database.CreateTableAsync<BaseLanguageWord>();

#if DEBUG
            Debug.WriteLine("DB path:" + Database.DatabasePath);
#endif
        }

        async private void testFn()
        {
            var result = await GetWordObjects();
            foreach (var item in result)
            {
                Debug.WriteLine("[OBJECT]" +
                    " T_ID: " + item.TranslateWord_ID +
                    " B_ID: " + item.BaseLanguageWord_ID +
                    " M_ID: " + item.WordMeaning_ID +
                    " Word: " + item.TranslatedWord +
                    " Polish: " + item.BaseLanguageWord +
                    " Part of speech: " + item.PartOfSpeech +
                    " Last update time: " + item.LastUpdateTime +
                    " Difficult level: " + item.DifficultLevel +
                    " Description: " + item.Description +
                    " Is difficult: " + item.IsDifficultWord +
                    " Is favourite: " + item.IsFavourite
                    );
            }
            Debug.WriteLine("s");
        }

        public async Task<List<WordObject>> GetWordObjects()
        {
            await Init();

            var q = "SELECT " +
                     "T.Word         as      \"TranslatedWord\"      , " +
                     "B.Word         as      \"BaseLanguageWord\"    , " +
                     "B.ID           as      \"BaseLanguageWord_ID\" , " +
                     "T.ID           as      \"TranslateWord_ID\"    , " +
                     "M.ID           as      \"WordMeaning_ID\"      , " +
                     "M.PartOfSpeech                                 , " +
                     "M.LastUpdateTime                               , " +
                     "T.DifficultLevel                               , " +
                     "M.Description                                  , " +
                     "T.IsDifficultWord                              , " +
                     "T.IsFavourite                                    " +
                     " from WordMeaning as M " +
                     "INNER JOIN BaseLanguageWord as B " +
                     "ON M.BaseLanguageWord_ID=B.ID " +
                     "INNER JOIN TranslatedWord as T " +
                     "ON M.TranslatedWord_ID=T.ID;";

            return Database.QueryAsync<WordObject>(q).Result;
        }

        //#region sample methods

        //public async Task<List<WordMeaning>> GetItemsAsync()
        //{
        //    await Init();
        //    return await Database.Table<WordMeaning>().ToListAsync();
        //}

        //public async Task<List<WordMeaning>> GetItemsNotDoneAsync()
        //{
        //    await Init();
        //    return await Database.Table<WordMeaning>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<WordMeaning>("SELECT * FROM [WordMeaning] WHERE [Done] = 0");
        //}

        //public async Task<WordMeaning> GetItemAsync(int id)
        //{
        //    await Init();
        //    return await Database.Table<WordMeaning>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        //public async Task<int> SaveItemAsync(WordMeaning item)
        //{
        //    await Init();
        //    if (item.ID != 0)
        //        return await Database.UpdateAsync(item);
        //    else
        //        return await Database.InsertAsync(item);
        //}

        //public async Task<int> DeleteItemAsync(WordMeaning item)
        //{
        //    await Init();
        //    return await Database.DeleteAsync(item);
        //}

        //#endregion

    }
}
