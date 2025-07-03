using Photon.Pun;
using UnityEngine;

public class RocketLauncher : Weapon
{
	[Header("RocketLauncher components")]
	[SerializeField] private Transform _rocketSpawnTransform;

	private RocketProjectile _currentRocketProjectile;
	
	private void Start() 
	{		
		CreateRocket();
	}
	protected override void Shoot()
	{		
	  	_currentRocketProjectile?.Launch(_rocketSpawnTransform.right);
	}

	protected override void EndReload()
	{
		CreateRocket();
	}
	private void CreateRocket() 
	{	
		if (!photonView.IsMine) return;
		
		var rocket = PhotonNetwork.Instantiate(
			"Rocket_Launcher_Missile",
			_rocketSpawnTransform.position,
			_rocketSpawnTransform.rotation
		);

		_currentRocketProjectile = rocket.GetComponent<RocketProjectile>();
		
		if (_currentRocketProjectile != null) 
		{
			photonView.RPC(nameof(RPC_SetParentRocket), RpcTarget.AllBuffered, rocket.GetPhotonView().ViewID);
			
			_currentRocketProjectile.Collider.isTrigger = true;

			_currentRocketProjectile.Initialize(Damage, Distance);
		}
	}

	[PunRPC]
	private void RPC_SetParentRocket(int rocketViewID) 
	{
		var rocketView = PhotonView.Find(rocketViewID);
		
		if (rocketView != null)
			rocketView.transform.SetParent(_rocketSpawnTransform);
	}

}
