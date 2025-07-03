using System;
using UnityEngine;

public class ApplicationStats : MonoBehaviour, IAppStatsNotifier
{
	private const float UpdateTime = 1f;
	
	private float _lastUpdateFpsTime = 0;
	private float _countFps;

    public event Action<int> OnUpdateFps;
	public event Action<int> OnUpdatePing;
	private void Update() 
	{
		FpsHandler();
		PingHandler();
	}
	
	private void FpsHandler() 
	{
		_countFps++;
		
		if (Time.time >= _lastUpdateFpsTime + UpdateTime) 
		{
			OnUpdateFps.Invoke((int)_countFps);
			
			_countFps = 0;
			_lastUpdateFpsTime = Time.time;
		}
	}
	
	private void PingHandler() 
	{
		var ping = Photon.Pun.PhotonNetwork.GetPing();
		
		OnUpdatePing.Invoke(ping);
	}
}
