namespace BlokOfLanguage;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		// todo
		Constants.DB = new DataBase.BlokOfLanguageDatabase();
        MainPage = new AppShell();
	}
}
