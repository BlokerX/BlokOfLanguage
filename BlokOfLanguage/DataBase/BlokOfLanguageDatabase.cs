using BlokOfLanguage.DataBase.EntityObjects;
using SQLite;

#if DEBUG
using System.Diagnostics;
#endif

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

            await Database.ExecuteAsync("PRAGMA encoding=\"UTF-8\";");

            var result1 = await Database.CreateTableAsync<WordMeaning>();
            var result2 = await Database.CreateTableAsync<TranslatedWord>();
            var result3 = await Database.CreateTableAsync<BaseLanguageWord>();

#if DEBUG
            Debug.WriteLine("DB path:" + Database.DatabasePath);
#endif
        }

        async private void testFn()
        {
            var result = await GetWordObjectsAsync();
            foreach (var item in result)
            {
                Debug.WriteLine("[OBJECT]" +
                    " T_ID: " + item.TranslatedWord_ID +
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

        public async Task<List<WordObject>> GetWordObjectsAsync()
        {
            await Init();
            return Database.QueryAsync<WordObject>(query:

                " SELECT " +

                " T.Word         as      \"TranslatedWord\"      , " +
                " B.Word         as      \"BaseLanguageWord\"    , " +
                " B.ID           as      \"BaseLanguageWord_ID\" , " +
                " T.ID           as      \"TranslatedWord_ID\"    , " +
                " M.ID           as      \"WordMeaning_ID\"      , " +
                " M.PartOfSpeech                                 , " +
                " M.LastUpdateTime                               , " +
                " T.DifficultLevel                               , " +
                " M.Description                                  , " +
                " T.IsDifficultWord                              , " +
                " T.IsFavourite                                    " +

                "  from WordMeaning as M                           " +

                " INNER JOIN BaseLanguageWord as B                 " +
                " ON M.BaseLanguageWord_ID=B.ID                    " +

                " INNER JOIN TranslatedWord as T                   " +
                " ON M.TranslatedWord_ID=T.ID                     ;"

                ).Result;
        }

        public async Task<List<WordObject>> GetWordObjectsByDateLimitAsync(int count)
        {
            await Init();
            return Database.QueryAsync<WordObject>(query:

                " SELECT " +

                " T.Word         as      \"TranslatedWord\"      , " +
                " B.Word         as      \"BaseLanguageWord\"    , " +
                " B.ID           as      \"BaseLanguageWord_ID\" , " +
                " T.ID           as      \"TranslatedWord_ID\"    , " +
                " M.ID           as      \"WordMeaning_ID\"      , " +
                " M.PartOfSpeech                                 , " +
                " M.LastUpdateTime                               , " +
                " T.DifficultLevel                               , " +
                " M.Description                                  , " +
                " T.IsDifficultWord                              , " +
                " T.IsFavourite                                    " +

                "  from WordMeaning as M                           " +

                " INNER JOIN BaseLanguageWord as B                 " +
                " ON M.BaseLanguageWord_ID=B.ID                    " +

                " INNER JOIN TranslatedWord as T                   " +
                " ON M.TranslatedWord_ID=T.ID                      " +

                $" ORDER BY M.LastUpdateTime DESC LIMIT {count}   ;"

                ).Result;
        }

        public async Task<List<WordObject>> GetWordObjectsByWordAsync(string word)
        {
            await Init();
            return Database.QueryAsync<WordObject>(query:

                " SELECT " +

                " T.Word         as      \"TranslatedWord\"      , " +
                " B.Word         as      \"BaseLanguageWord\"    , " +
                " B.ID           as      \"BaseLanguageWord_ID\" , " +
                " T.ID           as      \"TranslatedWord_ID\"    , " +
                " M.ID           as      \"WordMeaning_ID\"      , " +
                " M.PartOfSpeech                                 , " +
                " M.LastUpdateTime                               , " +
                " T.DifficultLevel                               , " +
                " M.Description                                  , " +
                " T.IsDifficultWord                              , " +
                " T.IsFavourite                                    " +

                "  from WordMeaning as M                           " +

                " INNER JOIN BaseLanguageWord as B                 " +
                " ON M.BaseLanguageWord_ID=B.ID                    " +

                " INNER JOIN TranslatedWord as T                   " +
                " ON M.TranslatedWord_ID=T.ID                      " +

                $" WHERE T.Word LIKE '{word}'                      " +
                $" OR T.Word LIKE '{word}%'                        " +
                $" OR T.Word LIKE '%{word}%'                       " +
                $" OR B.Word LIKE '{word}'                         " +
                $" OR B.Word LIKE '{word}%'                        " +
                $" OR B.Word LIKE '%{word}%'                      " +

                $" ORDER BY T.Word ASC, B.Word ASC;"

                ).Result;
        }

        #region SendQueryToDatabase

        public async Task<List<BaseLanguageWord>> SelectQueryAboutBaseLanguageWordObjectsAsync(string query)
        {
            await Init();
            return Database.QueryAsync<BaseLanguageWord>(query).Result;
        }

        public async Task<List<TranslatedWord>> SelectQueryAboutTranslatedWordObjectsAsync(string query)
        {
            await Init();
            return Database.QueryAsync<TranslatedWord>(query).Result;
        }

        public async Task<List<WordMeaning>> SelectQueryAboutWordMeaningObjectsAsync(string query)
        {
            await Init();
            return Database.QueryAsync<WordMeaning>(query).Result;
        }

        #endregion

        #region Get new id

        public async Task<int> GetBaseLanguageWordNewIDAsync()
        {
            await Init();
            var result = Database.QueryAsync<BaseLanguageWord>("SELECT id FROM BaseLanguageWord ORDER BY id DESC LIMIT 1;").Result;
            if (result.Count > 0)
                return result.FirstOrDefault().ID + 1;
            return 1;
        }

        public async Task<int> GetTranslatedWordNewIDAsync()
        {
            await Init();
            var result = Database.QueryAsync<TranslatedWord>("SELECT id FROM TranslatedWord ORDER BY id DESC LIMIT 1;").Result;
            if (result.Count > 0)
                return result.FirstOrDefault().ID + 1;
            return 1;
        }

        public async Task<int> GetWordMeaningNewIDAsync()
        {
            await Init();
            var result = Database.QueryAsync<WordMeaning>("SELECT id FROM WordMeaning ORDER BY id DESC LIMIT 1;").Result;
            if (result.Count > 0)
                return result.FirstOrDefault().ID + 1;
            return 1;
        }

        #endregion

        #region Get objects from table

        public async Task<List<BaseLanguageWord>> GetBaseLanguageWordObjectsAsync()
        {
            await Init();
            return await Database.Table<BaseLanguageWord>().ToListAsync();
        }

        public async Task<List<TranslatedWord>> GetTranslatedWordObjectsAsync()
        {
            await Init();
            return await Database.Table<TranslatedWord>().ToListAsync();
        }

        public async Task<List<WordMeaning>> GetWordMeaningObjectsAsync()
        {
            await Init();
            return await Database.Table<WordMeaning>().ToListAsync();
        }

        #endregion

        //public async Task<List<WordMeaning>> GetItemsNotDoneAsync()
        //{
        //    await Init();
        //    return await Database.Table<WordMeaning>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<WordMeaning>("SELECT * FROM [WordMeaning] WHERE [Done] = 0");
        //}

        #region Get item by unique id

        public async Task<BaseLanguageWord> GetBaseLanguageWordAsync(int id)
        {
            await Init();
            return await Database.Table<BaseLanguageWord>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<TranslatedWord> GetTranslatedWordAsync(int id)
        {
            await Init();
            return await Database.Table<TranslatedWord>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<WordMeaning> GetWordMeaningAsync(int id)
        {
            await Init();
            return await Database.Table<WordMeaning>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        #endregion

        #region Save object (Update or Insert if it doesn't exist)
        // todo uniezależnić od id i podzielić na 2 różne metody
        public async Task<int> SaveObjectAsync(BaseLanguageWord item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> SaveObjectAsync(TranslatedWord item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> SaveObjectAsync(WordMeaning item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        #endregion

        #region InsertObject

        public async Task<int> InsertObjectAsync(BaseLanguageWord item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        public async Task<int> InsertObjectAsync(TranslatedWord item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        public async Task<int> InsertObjectAsync(WordMeaning item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        #endregion

        #region Delete object

        public async Task<int> DeleteObjectAsync(BaseLanguageWord item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<int> DeleteObjectAsync(TranslatedWord item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<int> DeleteObjectAsync(WordMeaning item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        #endregion

        #region Export and Import


        public async Task<string> ExportDataToQuery() // todo usprawnić w przyszłości
        {
            string exportQuery = string.Empty;

            exportQuery += "[BaseLanugageWord]\n";

            foreach (var item in await GetWordObjectsAsync())
            {
                string line = $"{item.BaseLanguageWord_ID},{item.BaseLanguageWord}\n";
                exportQuery += line;
            }

            exportQuery += "[TranslatedWord]\n";

            foreach (var item in await GetWordObjectsAsync())
            {
                string line = $"{item.TranslatedWord_ID},{item.TranslatedWord},{item.IsDifficultWord},{item.IsFavourite},{item.DifficultLevel}\n";
                exportQuery += line;
            }

            exportQuery += "[WordMeaning]\n";

            foreach (var item in await GetWordObjectsAsync())
            {
                string line = $"{item.WordMeaning_ID},{item.BaseLanguageWord_ID},{item.TranslatedWord_ID},{item.PartOfSpeech},{item.Description},{item.LastUpdateTime}\n";
                exportQuery += line;
            }

            return exportQuery;
        }

        public async Task ImportDataFromQuery(string[] source) // todo usprawnić w przyszłości
        {
            int i = 0;
            foreach (var line in source)
            {
                if (line == "[BaseLanguageWord]")//todo bool nie działa i parsowanie
                {
                    i = 1; continue;
                }
                if (line == "[TranslatedWord]")
                {
                    i = 2; continue;
                }
                if (line == "[WordMeaning]")
                {
                    i = 3;
                    continue;
                }

                switch (i)
                {
                    case 1:
                        if (true)
                        {
                            int j = 0;
                            string id = string.Empty, word = string.Empty;
                            foreach (char letter in line)
                            {
                                if (letter == ',')
                                    j++;
                                if (j == 0) id += letter;

                                if (j == 1) word += letter;

                            }
                            await Database.InsertAsync(new BaseLanguageWord()
                            {
                                ID = int.Parse(id),
                                Word = word
                            });
                        }
                        break;

                    case 2:
                        if (true)
                        {

                            int j = 0;
                            string id = string.Empty, word = string.Empty, isDifficultWord = string.Empty, isFavourite = string.Empty, difficultLevel = string.Empty;
                            foreach (char letter in line)
                            {
                                if (letter == ',')
                                    j++;
                                if (j == 0) id += letter;
                                if (j == 1) word += letter;
                                if (j == 2) isDifficultWord += letter;
                                if (j == 3) isFavourite += letter;
                                if (j == 4) difficultLevel += letter;

                            }
                            await Database.InsertAsync(new TranslatedWord()
                            {
                                ID = int.Parse(id),
                                Word = word,
                                IsDifficultWord = bool.Parse(isDifficultWord),
                                IsFavourite = bool.Parse(isFavourite),
                                DifficultLevel = difficultLevel
                            });
                        }
                        break;

                    case 3:
                        if (true)
                        {
                            int j = 0;
                            string id = string.Empty, blw_id = string.Empty, tw_id = string.Empty, partOfSpeech = string.Empty, description = string.Empty, lastUpdateTime = string.Empty;
                            foreach (char letter in line)
                            {
                                if (letter == ',')
                                    j++;
                                if (j == 0) id += letter;
                                if (j == 1) blw_id += letter;
                                if (j == 2) tw_id += letter;
                                if (j == 3) partOfSpeech += letter;
                                if (j == 4) description += letter;
                                if (j == 5) lastUpdateTime += letter;

                            }
                            await Database.InsertAsync(new WordMeaning()
                            {
                                ID = int.Parse(id),
                                BaseLanguageWord_ID = int.Parse(blw_id),
                                TranslatedWord_ID = int.Parse(tw_id),
                                PartOfSpeech = partOfSpeech,
                                Description = description,
                                LastUpdateTime = DateTime.Parse(lastUpdateTime)
                            });
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
