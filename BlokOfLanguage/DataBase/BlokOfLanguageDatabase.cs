﻿using BlokOfLanguage.DataBase.EntityObjects;
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
            var result = await GetWordObjectsAsync();
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

        public async Task<List<WordObject>> GetWordObjectsAsync()
        {
            await Init();
            return Database.QueryAsync<WordObject>(query:

                " SELECT " +

                " T.Word         as      \"TranslatedWord\"      , " +
                " B.Word         as      \"BaseLanguageWord\"    , " +
                " B.ID           as      \"BaseLanguageWord_ID\" , " +
                " T.ID           as      \"TranslateWord_ID\"    , " +
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
            return Database.QueryAsync<BaseLanguageWord>("SELECT id FROM BaseLanguageWord ORDER BY id DESC LIMIT 1;").Result.First().ID + 1;
        }

        public async Task<int> GetTranslatedWordNewIDAsync()
        {
            await Init();
            return Database.QueryAsync<TranslatedWord>("SELECT id FROM TranslatedWord ORDER BY id DESC LIMIT 1;").Result.First().ID + 1;
        }

        public async Task<int> GetWordMeaningNewIDAsync()
        {
            await Init();
            return Database.QueryAsync<WordMeaning>("SELECT id FROM WordMeaning ORDER BY id DESC LIMIT 1;").Result.First().ID + 1;
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

        //public async Task<WordMeaning> GetItemAsync(int id)
        //{
        //    await Init();
        //    return await Database.Table<WordMeaning>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        #region Save object
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

    }
}
