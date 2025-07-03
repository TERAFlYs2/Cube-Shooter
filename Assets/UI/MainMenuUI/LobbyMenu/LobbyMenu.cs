using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : Menu
{
	[SerializeField] private Button _backButton;
	[SerializeField] private Button _createRoomButton;
	[SerializeField] private Button _joinRoomButton;
	
	[SerializeField] private TMP_InputField _joinOrCreateRoomField;
	[SerializeField] private TMP_Text _statusText;

	private IRoomService _roomService;
	
	public IRoomService RoomService 
	{
		get => _roomService;
		
		set 
		{
			if (_roomService != null) 
			{
				_roomService.OnRoomJoined -= OnRoomJoinedText;
				_roomService.OnRoomJoinedFailed -= OnRoomJoinedFailedText;
			}
			_roomService = value;
			
			_roomService.OnRoomJoined += OnRoomJoinedText;
			_roomService.OnRoomJoinedFailed += OnRoomJoinedFailedText;
			
			
		}
	}
	public override void SubscribeListeners()
	{
	   _joinRoomButton.onClick.AddListener(OnJoinRoomButtonClicked);
	   _createRoomButton.onClick.AddListener(OnCreateRoomButtonClicked);
	   _backButton.onClick.AddListener(OnBackButtonClicked);
	}

	public override void UnSubscribeListeners()
	{
		_joinRoomButton.onClick.RemoveListener(OnJoinRoomButtonClicked);
		_createRoomButton.onClick.RemoveListener(OnCreateRoomButtonClicked);
		_backButton.onClick.RemoveListener(OnBackButtonClicked);
	}
	
	private void OnCreateRoomButtonClicked() 
	{
		string roomName = _joinOrCreateRoomField.text;
		
		if (string.IsNullOrEmpty(roomName)) 
		{
			return;
		}
		
		_roomService.CreateRoom(roomName, new Photon.Realtime.RoomOptions {MaxPlayers = 4});
	}
	
	private void OnJoinRoomButtonClicked() 
	{
		string roomName = _joinOrCreateRoomField.text;
		
		if (string.IsNullOrEmpty(roomName)) 
		{
			return;
		}
		
		_roomService.JoinRoom(roomName);
	}
	
	private void OnBackButtonClicked() 
	{
		MenuController.OpenMenu("Main");
	}
	
	private void OnRoomJoinedText(string message)
	{
		_statusText.text = message;
	}
	private void OnRoomJoinedFailedText(string message)
	{
		_statusText.text = message;
	}
}
