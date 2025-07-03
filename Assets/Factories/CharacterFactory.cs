using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
public class CharacterFactory
{
	private readonly CharacterFactoryData _data;
	private CharacterBase _character;
	
	public CharacterFactory(CharacterFactoryData data)
	{
		_data = data;
	}
	public CharacterBase Create() 
	{
		Vector3 worldPosition = Vector3.zero; 
		Quaternion worldRotation = Quaternion.identity;
		
		if (_character == null) 
		{
			_character = PhotonNetwork.Instantiate(_data.Prefab.name, worldPosition, worldRotation).GetComponent<CharacterBase>();
		}
		
		return _character;
	}

}
