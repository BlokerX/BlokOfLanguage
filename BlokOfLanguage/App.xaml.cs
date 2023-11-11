namespace BlokOfLanguage;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		DataBase.Constants.LoadDataBase();
        MainPage = new AppShell();
	}
}
