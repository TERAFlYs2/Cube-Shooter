using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainMenuUIData
{
	[field: SerializeField] public MainMenu MainMenu { get; private set; }
	[field: SerializeField] public LobbyMenu LobbyMenu { get; private set; }
	[field: SerializeField] public SettingsMenu SettingsMenu { get; private set; }
	[field: SerializeField] public TrainingMenu TrainingMenu { get; private set; }
	
	//[field: SerializeField] public List<Menu> Menus { get; private set; }
}
