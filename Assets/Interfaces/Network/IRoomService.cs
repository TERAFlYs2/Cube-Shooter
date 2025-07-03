using System;
public interface IRoomService
{
	void JoinRoom(string roomName);
	void CreateRoom(string roomName, Photon.Realtime.RoomOptions roomOptions);
	event Action<string> OnRoomJoined;
	event Action<string> OnRoomJoinedFailed;
}
