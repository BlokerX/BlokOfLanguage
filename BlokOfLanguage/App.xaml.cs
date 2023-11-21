namespace BlokOfLanguage;

public partial class App : Application
{
	public App()
	{
		DataBase.Constants.LoadDataBase();
		InitializeComponent();
		MainPage = new AppShell();
	}
}
