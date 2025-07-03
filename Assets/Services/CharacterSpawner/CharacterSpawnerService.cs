using System;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterSpawnerService : IDisposable
{
	private readonly CharacterSpawnerData _data;

	private CharacterBase _character;
	private CharacterController _characterController;
	
	private float _currentRespawnDuration;
	
	public CharacterSpawnerService(CharacterSpawnerData data, CharacterBase character)
	{
		_data = data;
		_character = character;
		_characterController = _character.GetComponent<CharacterController>();
		
		_currentRespawnDuration = _data.RespawnDuration;
		_character.Damageble.OnDie += Respawn;
	}

	public void Dispose()
	{
		_character.Damageble.OnDie -= Respawn;
	}

	public void SpawnRandom() 
	{ 		
		if (_character.photonView != null && _character.photonView.IsMine)
		{
			int randIndexPoint = UnityEngine.Random.Range(0, _data.SpawnPoints.Count);
			float randAngleY = UnityEngine.Random.Range(0, 360);
			
			_characterController.enabled = false;
			
			_character.transform.position = _data.SpawnPoints[randIndexPoint].position;
			_character.transform.localRotation = Quaternion.Euler(0, randAngleY, 0);
			
			_characterController.enabled = true;
			Debug.Log($"Позиция сейчас {_character.transform.position}; Позиция которая выпала {_data.SpawnPoints[randIndexPoint].position}");
		}

	}
	private async void Respawn() 
	{	
		_character.CharacterMovement.CanMovement = false;
		_character.Damageble.SetCanTakeDamage(false);
		await Task.Delay(TimeSpan.FromSeconds(_currentRespawnDuration));
		
		SpawnRandom();
		
		_character.Data.Animator.SetBool("IsDead", false);
		_character.Damageble.SetMaxHealth();
		_character.CharacterMovement.CanMovement = true;
		_character.Damageble.SetCanTakeDamage(true);
	}
	

}
