using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks, IRoomService
{
	private const string OnConnectedToMasterMessage = "You have connected to the server";
	private const string OnJoinedLobbyMessage = "You have connected to the lobby";
	private const string OnJoinedRoomMessage = "You have connected to the room";
	
	private MapsManager _mapsManager;
	
	[field: SerializeField] public string Version { get; private set; }
	
	public new event Action<string> OnConnected;
	public event Action<string> OnLobbyConnected;
	public event Action<string> OnRoomJoined;
	public event Action<string> OnRoomJoinedFailed;
	
	
	
	private void Awake() 
	{
		Debug.Log(Version);
		PhotonNetwork.GameVersion = Version;
		PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(1000, 9999);
		PhotonNetwork.SendRate = 30;
		PhotonNetwork.SerializationRate = 15;
		
		if (!PhotonNetwork.IsConnected) 
		{
			PhotonNetwork.ConnectUsingSettings();
		}
	}
	public void Initialize(MapsManager mapsManager) 
	{
		_mapsManager = mapsManager;
	}
	public override void OnConnectedToMaster()
	{
		OnConnected?.Invoke(OnConnectedToMasterMessage);
		PhotonNetwork.JoinLobby();
		
		Debug.Log(OnConnectedToMasterMessage);
	}

	public override void OnJoinedLobby()
	{
		OnLobbyConnected?.Invoke(OnJoinedLobbyMessage);
		
		Debug.Log(OnJoinedLobbyMessage);
	}
	public override void OnJoinedRoom()
	{
		OnRoomJoined?.Invoke(OnJoinedRoomMessage);
		
		PhotonNetwork.LoadLevel("GameScene");

		Debug.Log(OnJoinedRoomMessage);
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		OnRoomJoinedFailed?.Invoke(message);
		
		Debug.Log(message);
	}
	
	public void CreateRoom(string roomName, RoomOptions roomOptions) 
	{
		if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby) 
		{
			PhotonNetwork.CreateRoom(roomName, roomOptions);
		}
	}
	public void JoinRoom(string roomName)
	{
		if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby) 
		{
			PhotonNetwork.JoinRoom(roomName);
		}
	}
	
}