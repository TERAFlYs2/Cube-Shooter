using System.Collections.Generic;

public class MainMenuUIHandler
{
	public MainMenuUIData Data { get; private set; }
	
	public MenuController MenuController { get; private set; }
	public List<Menu> Menus { get; private set; }
	
	public MainMenuUIHandler(MainMenuUIData data)
	{
		Data = data;
		
		Menus = new List<Menu>() 
		{
			Data.MainMenu,
			Data.LobbyMenu,
			Data.SettingsMenu,
			Data.TrainingMenu
		};
	

		MenuController = new MenuController(Menus);
		foreach (var menu in Menus) 
		{
			menu.MenuController = MenuController;
		}
		
		MenuController.OpenMenu(Data.MainMenu.Name);
	}

	
	public void OnEnable() 
	{
		foreach (var menu in Menus) 
		{
			menu.SubscribeListeners();
		}
	}
	
	public void OnDisable() 
	{
		foreach (var menu in Menus) 
		{
			menu.UnSubscribeListeners();
		}
	}
}
