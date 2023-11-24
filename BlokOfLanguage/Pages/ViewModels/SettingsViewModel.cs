using BlokOfLanguage.DataBase;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
#if DEBUG
using System.Diagnostics;
using System.Threading;
#endif

namespace BlokOfLanguage.Pages.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public string PathOfDatabase => Constants.DatabasePath;

        public System.Windows.Input.ICommand CopyPathOfDatabaseToClipboardCommand { get; set; }
        public System.Windows.Input.ICommand ImportCommand { get; set; }
        public System.Windows.Input.ICommand ExportCommand { get; set; }

        public SettingsViewModel()
        {
            CopyPathOfDatabaseToClipboardCommand = new Command(CopyPathOfDatabaseToClipboard);
            ImportCommand = new Command(async () => await ImportDatabase());
            ExportCommand = new Command(async () => await ExportDatabase());
        }

        private void CopyPathOfDatabaseToClipboard()
        {
            Clipboard.SetTextAsync(PathOfDatabase);
#if DEBUG
            Debug.WriteLine("Path of database has copied successfull!");
#endif
        }

        private async Task ImportDatabase()
        {
#if DEBUG
            Debug.WriteLine("Import of database has successfull!");
#endif
            //todo do zrobienia kopiowanie do schowka
        }

        private async Task ExportDatabase()
        {
            //if (!FileSystem.Current.AppPackageFileExistsAsync(PathOfDatabase).Result)
            //{
            //    //todo wait DisplayAlert("Can not export.", "Can not find database!", "OK");
            //    return;
            //}

            //using var stream = await FileSystem.OpenAppPackageFileAsync(PathOfDatabase);
            //var fileSaverResult = await FileSaver.Default.SaveAsync("BlokOfLanguageSQLite_DatabaseImport" + ".db3", stream);
            //if (fileSaverResult.IsSuccessful)
            //{
            //    await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show();
            //}
            //else
            //{
            //    await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show();
            //}
#if DEBUG
            Debug.WriteLine("Export of database has successfull!");
#endif
            //todo do zrobienia kopiowanie do schowka
        }
    }
}
