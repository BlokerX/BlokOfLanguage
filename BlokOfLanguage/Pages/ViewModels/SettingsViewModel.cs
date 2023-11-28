using BlokOfLanguage.DataBase;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
#if DEBUG
using System.Diagnostics;
using System.Text;
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
            var result = await FilePicker.Default.PickAsync();
            if (result == null) return;
            var tab = File.ReadAllLines(result.FullPath);
            await Constants.DB.ImportDataFromQuery(tab);
#if DEBUG
            Debug.WriteLine("Import of database has successfull!");
#endif
            //todo do zrobienia kopiowanie do schowka
        }

        private async Task ExportDatabase()
        {
#if ANDROID
            PermissionStatus statusread = await Permissions.RequestAsync<Permissions.StorageRead>();
            while (statusread != PermissionStatus.Granted)
            {
                statusread = await Permissions.RequestAsync<Permissions.StorageRead>();
            };

            PermissionStatus statuswrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
            while (statuswrite != PermissionStatus.Granted)
            {
                statuswrite = await Permissions.RequestAsync<Permissions.StorageRead>();
            };
#endif
            if (!FileSystem.Current.AppPackageFileExistsAsync(PathOfDatabase).Result)
            {
                //todo wait DisplayAlert("Can not export.", "Can not find database!", "OK");
                return;
            }
            using var stream = new MemoryStream(Encoding.Default.GetBytes(await Constants.DB.ExportDataToQuery()));
            var fileSaverResult = await FileSaver.Default.SaveAsync("BlokOfLanguageSQLite_DatabaseImport" + ".blokOfLanguageSaveFormat", stream);
            if (fileSaverResult.IsSuccessful)
            {
                await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show();
            }
            else
            {
                await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show();
            }
#if DEBUG
            Debug.WriteLine("Export of database has successfull!");
#endif
            //todo do zrobienia kopiowanie do schowka
        }
    }
}
