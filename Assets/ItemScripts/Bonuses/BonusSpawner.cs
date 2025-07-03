using System.Collections;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class BonusSpawner : MonoBehaviourPun
{
	[SerializeField] [Range(1f, 100f)] private float _chance; 
	[SerializeField] private Bonus[] _existingBonuses;
	[SerializeField] private float _timeSpawn;
	[SerializeField] private Transform _spawnPoint;
	
	private float _lastSpawnTime = Mathf.NegativeInfinity;
	private Bonus _spawnedBonus;
	
	private void Awake() 
	{
		photonView.ViewID = PhotonNetwork.AllocateViewID(PhotonNetwork.MasterClient.ActorNumber);
	}
	private void Start() 
	{
		CreateBonus();
	}
	
	private void FixedUpdate() 
	{
		if (_spawnedBonus == null) 
		{
			if (Time.time > _lastSpawnTime + _timeSpawn) 
			{
				CreateBonus();
				_lastSpawnTime = Time.time;
			}
		}
		else
			_spawnedBonus.transform.Rotate(Vector3.up * 45f * Time.fixedDeltaTime);
	}
	
	private void CreateBonus() 
	{
		if(PhotonNetwork.IsMasterClient) 
		{
			int index = Random.Range(0, _existingBonuses.Length);
		
			Bonus newBonus = _existingBonuses[index];
			
			_spawnedBonus = PhotonNetwork.Instantiate(newBonus.Prefab.name, _spawnPoint.position, Quaternion.identity).GetComponent<Bonus>();

			
			photonView.RPC(nameof(RPC_SetParent), RpcTarget.All, _spawnedBonus.SpecificPhotonView.ViewID);
		}
	}
	
	[PunRPC]
	private void RPC_SetParent(int viewID) 
	{
		PhotonView bonusSpawnerView = PhotonView.Find(viewID);
		
		if (bonusSpawnerView != null) 
		{
			bonusSpawnerView.transform.SetParent(_spawnPoint.transform);
			Debug.LogWarning("///////////////////////");
		}

	}
}
