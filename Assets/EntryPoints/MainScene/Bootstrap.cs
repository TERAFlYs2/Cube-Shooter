using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] private PhotonManager _photonManager;
	[SerializeField] private MainMenuUIData _mainMenuUIData;
	
	private MainMenuUIHandler _mainMenuUIHandler;
	private void Awake() 
	{
		InitializeServices();
		InitializeMainUI();
	}
	
	private void InitializeServices() 
	{
		var mapsManager = new MapsManager();
		
		_photonManager.Initialize(mapsManager);
	}
	private void InitializeMainUI() 
	{
		_mainMenuUIHandler = new MainMenuUIHandler(_mainMenuUIData);
		
		LobbyMenu lobbyMenu = _mainMenuUIHandler.Data.LobbyMenu;
		MainMenu mainMenu = _mainMenuUIHandler.Data.MainMenu;
		
		
		lobbyMenu.RoomService = _photonManager;
		mainMenu.VersionText = _photonManager.Version;
	}
	private void OnEnable() 
	{
		_mainMenuUIHandler.OnEnable();
	}
	
	private void OnDisable() 
	{
		_mainMenuUIHandler.OnDisable();
	}
}
