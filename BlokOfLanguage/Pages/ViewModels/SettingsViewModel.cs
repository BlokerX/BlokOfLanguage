using BlokOfLanguage.DataBase;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using System.Text;
#if DEBUG
using System.Diagnostics;
#endif

namespace BlokOfLanguage.Pages.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        private readonly Page page;
        public string PathOfDatabase => Constants.DatabasePath;

        public System.Windows.Input.ICommand CopyPathOfDatabaseToClipboardCommand { get; set; }
        public System.Windows.Input.ICommand ImportCommand { get; set; }
        public System.Windows.Input.ICommand ExportCommand { get; set; }
        public System.Windows.Input.ICommand ClearDatabaseCommand { get; set; }

        public SettingsViewModel(Page page)
        {
            this.page = page;

            CopyPathOfDatabaseToClipboardCommand = new Command(async () => await CopyPathOfDatabaseToClipboard());
            ImportCommand = new Command(async () => await ImportDatabase());
            ExportCommand = new Command(async () => await ExportDatabase());
            ClearDatabaseCommand = new Command(async () => await ClearDatabase());
        }

        private async Task CopyPathOfDatabaseToClipboard()
        {
            await Clipboard.SetTextAsync(PathOfDatabase);
            await Toast.Make("Path of database has copied successfull!").Show();
        }

        private async Task ImportDatabase()
        {
            if (!await page.DisplayAlert("Do you want to make import of data to database?", "The operation cannot be undone.", "Yes", "No"))
                return;

            var result = await FilePicker.Default.PickAsync();
            if (result == null)
            {
                await page.DisplayAlert("Import has failed!.", "Please try again.", "OK");
                return;
            }
            var tab = File.ReadAllLines(result.FullPath);
            await Constants.DB.ImportDataFromQuery(tab);
            await page.DisplayAlert("Import has been successfull!.", "The items has just apeared in database.", "OK");
            //todo do zrobienia kopiowanie do schowka
        }

        private async Task ExportDatabase()
        {
            if (!await page.DisplayAlert("Do you want to make export of data from database?", "Select yes to make export.", "Yes", "No"))
                return;

            //#if ANDROID
            //            var statusread = PermissionStatus.Unknown;
            //            statusread = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            //            while (statusread != PermissionStatus.Granted)
            //            {
            //                statusread = await Permissions.RequestAsync<Permissions.StorageRead>();
            //            };

            //            var statuswrite = PermissionStatus.Unknown;
            //            statuswrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            //            while (statuswrite != PermissionStatus.Granted)
            //            {
            //                statuswrite = await Permissions.RequestAsync<Permissions.StorageRead>();
            //            };
            //#endif
            //if (!FileSystem.Current.AppPackageFileExistsAsync(Constants.DatabaseFilename).Result)
            //{
            //    //todo wait DisplayAlert("Can not export.", "Can not find database!", "OK");
            //    return;
            //}
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
            //todo do zrobienia kopiowanie do schowka
        }

        private async Task ClearDatabase()
        {
            if (!await page.DisplayAlert("Do you want to clear all data of database?", "The operation cannot be undone.", "Yes", "No"))
            {
                await Toast.Make($"Database hasn't cleared.").Show();
                return;
            }

            await Constants.DB.ClearDatabase();
            await Toast.Make($"Database has cleared successfull!").Show();
            return;
        }
    }
}
