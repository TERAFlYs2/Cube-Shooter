using Photon.Pun;
using UnityEngine;

public abstract class Item : MonoBehaviourPun
{
	[field: SerializeField] public PhotonView SpecificPhotonView  {get; private set; }
	
	private void OnTriggerEnter(Collider other) 
	{
		Interaction(other);
	}
	public void Interaction(Collider other) 
	{
		CharacterBase character = other.GetComponent<CharacterBase>();
		PhotonView otherView = other.GetComponent<PhotonView>();
		
		if (character != null && otherView != null && otherView.IsMine) 
		{
			Take(character, otherView);
		}
	}
	
	protected virtual void Take(CharacterBase character, PhotonView otherView) 
	{
		
	}
}
