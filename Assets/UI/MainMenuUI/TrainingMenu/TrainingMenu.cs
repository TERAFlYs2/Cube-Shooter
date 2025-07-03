using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingMenu : Menu
{
	[SerializeField] private Button _backButton;
	[SerializeField] private Transform _content;
	
	[SerializeField] private Button _mapPrefab;
	
	private void Start() 
	{
		Button obj = Instantiate(_mapPrefab, _content);
		obj.onClick.AddListener(OnMapClicked);
	}

	public override void SubscribeListeners()
	{
	   _backButton.onClick.AddListener(OnBackButtonClicked);
	}

	public override void UnSubscribeListeners()
	{
		_backButton.onClick.RemoveListener(OnBackButtonClicked);
		_mapPrefab.onClick.RemoveListener(OnMapClicked);
	}
	
	private void OnBackButtonClicked() 
	{
		MenuController.OpenMenu("Main");
	}
	
	private void OnMapClicked() 
	{
		SceneManager.LoadScene("GameScene");
		Debug.Log("Кнопка нажата");
	}
	
}
