using System.Collections.Generic;
public class MenuController
{
	private readonly List<Menu> _menus;
	
	public MenuController(List<Menu> menus)
	{
		_menus = menus;
	}
	
	public void OpenMenu(string name) 
	{
		foreach (Menu menu in _menus) 
		{
			if (menu.Name == name) 
			{
				menu.Open();
			}
			else 
			{
				menu.Close();
			}
		}
	}
	
	public void CloseMenu(string name) 
	{
		foreach (Menu menu in _menus) 
		{
			if (menu.Name == name) 
			{
				menu.Close();
				break;
			}
		}
	}
}
