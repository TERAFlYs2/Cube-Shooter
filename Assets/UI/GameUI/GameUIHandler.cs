using System;
using UnityEngine;

public class GameUIHandler : IDisposable
{
	private readonly GameUIData _data;

	public GameHudHandler GameHudHandler { get; private set; }
	public GameUIHandler(GameUIData data)
	{
		_data = data;
		
		GameHudHandler = new GameHudHandler(_data.GameHudData);
	}

	public void Dispose()
	{
		Debug.Log($"Объект типа {this} удален");
		GameHudHandler.UnSubscribeListeners();
	}

}
