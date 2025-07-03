using UnityEngine;

public class RenderHeadBone : MonoBehaviour
{
	private void Start() 
	{
		var photonView = GetComponentInParent<Photon.Pun.PhotonView>();
		
		if (photonView != null) 
			transform.localScale = photonView.IsMine ? Vector3.zero : Vector3.one;		
	}
}
