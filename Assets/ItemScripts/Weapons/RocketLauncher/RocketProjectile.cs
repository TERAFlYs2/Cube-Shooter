using System.Collections;
using Photon.Pun;
using UnityEngine;

public class RocketProjectile : MonoBehaviourPun
{
	[Header("Parametrs")]
	[SerializeField] private int _explosionDamage;
	[SerializeField] private float _explosionRange;
	
	[Header("Effects")]
	[SerializeField] private GameObject _explosionEffect;
	[SerializeField] private AudioClip _explosionSound;
	
	[Header("Other components")]
	[SerializeField] private AudioSource _source;
	
	private float _force;
	private int _directDamage;
	
	private Rigidbody _rigidbody;
	private Coroutine _destroyRoutine;
	private int _currentCountExplosions = 0;
	
	public BoxCollider Collider { get; private set; }
	public const int CountExplode = 1;
	private void Awake() 
	{
		_source = GetComponent<AudioSource>();
		_rigidbody = GetComponent<Rigidbody>();
		Collider = GetComponent<BoxCollider>();
		
		_rigidbody.useGravity = false;
		
		_source.clip = _explosionSound;
	}
	public void Initialize(int directDamage, float distance) 
	{
		_directDamage = directDamage;
		_force = distance;
	}
	public void Launch(Vector3 direction) 
	{
		if (Collider != null) 
		{
			Collider.isTrigger = false;
		}
		
		photonView.RPC(nameof(RPC_ResetParent), RpcTarget.All);
		_rigidbody.useGravity = true;
		_rigidbody.AddForce(direction * _force, ForceMode.Impulse);	
	}
	
	private void OnCollisionEnter(Collision other) 
	{	
		if (_currentCountExplosions <= CountExplode && transform.parent != null) return;
		
		Explode(other);	
	}
	
	private void Explode(Collision other) 
	{
		_currentCountExplosions++;
		
		photonView.RPC(nameof(RPC_PlayExplosionEffects), RpcTarget.All);
		
		PhotonNetwork.Instantiate(_explosionEffect.name, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));

		if (other.gameObject.TryGetComponent<PhotonView>(out var otherView)) 
		{
			photonView.RPC(nameof(RPC_ApplyDamage), RpcTarget.All, otherView.ViewID, _directDamage);
		}
		
		Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRange);
			
		foreach (var collider in colliders) 
		{
			if (collider.TryGetComponent<PhotonView>(out var colliderView)) 
			{
				photonView.RPC(nameof(RPC_ApplyDamage), RpcTarget.All, colliderView.ViewID, _explosionDamage);
			}
		}
		
		_rigidbody.isKinematic = true;
	}
 	
	[PunRPC]
	private void RPC_ResetParent() 
	{
		transform.SetParent(null);
	}
	[PunRPC]
	private void RPC_ApplyDamage(int targetObjId, int finalDamage) 
	{
		PhotonView targetView = PhotonView.Find(targetObjId);
		
		if (targetView != null) 
		{
			targetView.GetComponent<CharacterBase>()?.Damageble.TakeDamage(finalDamage);
		}
	}
	
	[PunRPC]
	private void RPC_PlayExplosionEffects() 
	{
		_source.Play();
		
		if (_destroyRoutine == null && photonView.IsMine) 
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			_destroyRoutine = StartCoroutine(DestroyRoutine());
		}
			
	}
	
	private IEnumerator DestroyRoutine() 
	{
		yield return new WaitForSeconds(2f);
		Debug.Log("Corrrrrr");
		PhotonNetwork.Destroy(photonView);
	}
}
