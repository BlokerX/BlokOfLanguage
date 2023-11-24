using BlokOfLanguage.DataBase;
using BlokOfLanguage.DataBase.EntityObjects;
#if DEBUG
using System.Diagnostics;
#endif

namespace BlokOfLanguage.Pages.ViewModels
{
    public class MainViewModel : ViewModel
    {

        private List<WordObject> _words;

        public List<WordObject> Words
        {
            get => _words;
            set
            {
                _words = value;
                OnPropertyChanged(nameof(Words));
            }
        }

        public async Task LoadLastWordsList()
        {
            try
            {
                Words = await Constants.DB.GetWordObjectsByDateLimitAsync(5);
            }
            catch (Exception ex) { Debug.WriteLine("[EXCEPTION]: " + ex); }
        }
    }
}
