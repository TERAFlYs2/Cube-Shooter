using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu 
{
	[Header("Buttons")]
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _trainingButton;
	[SerializeField] private Button _searchGameButton;
	
	[SerializeField] private TMP_Text _versionText;
	
	public string VersionText 
	{ 	
		get => _versionText.text;
	  	set  
		{
			_versionText.text += $" {value}";
		}
	}

	public override void SubscribeListeners() 
	{
		_exitButton.onClick.AddListener(OnExitButtonClicked);
		_trainingButton.onClick.AddListener(OnTrainingButtonClicked);
		_searchGameButton.onClick.AddListener(OnSearchGameButtonClickd);
	}
	
	public override void UnSubscribeListeners() 
	{
		_exitButton.onClick.RemoveListener(OnExitButtonClicked);
		_trainingButton.onClick.RemoveListener(OnTrainingButtonClicked);
		_searchGameButton.onClick.RemoveListener(OnSearchGameButtonClickd);
	}
	
	public void OnTrainingButtonClicked() 
	{
		MenuController.OpenMenu("Training");
	}
	public void OnSearchGameButtonClickd() 
	{
		MenuController.OpenMenu("Lobby");
	}
	public void OnExitButtonClicked() 
	{
		Application.Quit();
		Debug.Log("Мы выйшли");
	}
}
