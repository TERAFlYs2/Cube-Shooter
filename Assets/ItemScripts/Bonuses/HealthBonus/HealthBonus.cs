using Photon.Pun;
using UnityEngine;

public class HealthBonus : Bonus
{
	[Header("Health bonus parametrs")]
	[SerializeField] private int _healAmount;

	protected override void Take(CharacterBase character, PhotonView otherView)
	{
		photonView.RPC(nameof(RPC_ApplyHeal), RpcTarget.All, otherView.ViewID);
		
		if (PhotonNetwork.IsMasterClient) 
		{
			PhotonNetwork.Destroy(photonView);
		}
	}
	
	[PunRPC] 
	private void RPC_ApplyHeal(int targetID) 
	{
		PhotonView targetView = PhotonView.Find(targetID);
		
		if (targetView != null) 
		{
			targetView.GetComponent<CharacterBase>().Damageble.Heal(_healAmount);
		}
	}
}
