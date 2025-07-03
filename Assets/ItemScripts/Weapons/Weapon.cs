using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public abstract class Weapon : Item, IWeaponNotifier
{
	[Header("Weapon parametrs")]
	[SerializeField] protected int Damage;
	[SerializeField] protected float ReloadTime;
	[SerializeField] protected float Rate;
	[SerializeField] protected float Distance;
	[SerializeField] protected int NumberAmmoInClip;
	[SerializeField] protected int MaxAmmo;
	
	[Header("Weapon effects")]
	[SerializeField] protected GameObject Muzzle;
	[SerializeField] protected AudioClip ShotClip;
	[SerializeField] protected AudioClip ReloadClip;
	
	[Header("Weapon other components")]
	[SerializeField] protected AudioSource SourceShot;
	[SerializeField] protected AudioSource SourceReload;
	[field: SerializeField] public Transform MuzzlePosition {get; private set; } 
	
	private float _lastTimeShot = Mathf.NegativeInfinity;
	private float _lastReloadTime = Mathf.NegativeInfinity;
	
	private int _currentAmmo;
	private int _currentMaxAmmo;
	
	private Coroutine _currentReloadCoroutine;
	private PhotonView _muzzleView;
	
	public bool Reloading { get; private set; } = false;
	
	public int CurrentAmmo 
	{ 
		get => _currentAmmo; 
		
		private set 
		{
			_currentAmmo = value;
			CurrentAmmoChanged?.Invoke(_currentAmmo);
		}
	}
	
	public int CurrentMaxAmmo 
	{
		get => _currentMaxAmmo;
		
		private set 
		{
			_currentMaxAmmo = value;
			CurrentMaxAmmoChanged?.Invoke(_currentMaxAmmo);
		}
	}
	public event Action<int> CurrentAmmoChanged;
	public event Action<int> CurrentMaxAmmoChanged;
	private void Awake() 
	{	
		_muzzleView = Muzzle.GetComponent<PhotonView>();
		if (MaxAmmo >= NumberAmmoInClip) 
		{
			CurrentAmmo = NumberAmmoInClip;
			CurrentMaxAmmo = MaxAmmo - NumberAmmoInClip;
		}
			
		SourceShot.clip = ShotClip;
		SourceReload.clip = ReloadClip;
		
	}
	public void Attack() 
	{
		if (NextShot() && !Reloading && CurrentAmmo > 0) 
		{
			Shoot();
			
			_lastTimeShot = Time.time;
			CurrentAmmo--;

			SpecificPhotonView?.RPC(nameof(RPC_Shot), RpcTarget.All, SpecificPhotonView.ViewID);	
		}
	}
	public virtual void Reload() 
	{	
		if (CurrentAmmo < NumberAmmoInClip && CurrentMaxAmmo > 0 && !Reloading && Time.time >= _lastReloadTime + ReloadTime) 
		{			
			if (_currentReloadCoroutine == null)
				_currentReloadCoroutine = StartCoroutine(ReloadRoutine());
		}
	}
	protected virtual void BeginReload() 
	{
		
	}
	protected virtual void EndReload() 
	{
		
	}
	
	protected abstract void Shoot();
	
	protected virtual bool NextShot()
	{
		return Time.time >= _lastTimeShot + 1f / Rate;
	}
	private IEnumerator ReloadRoutine() 
	{
		Reloading = true;
		
		BeginReload();
		
		SpecificPhotonView?.RPC(nameof(RPC_PlayAudioReload), RpcTarget.All);
		
		yield return new WaitForSeconds(ReloadTime);
		
		int remainingAmmo = NumberAmmoInClip - CurrentAmmo;
		
		int finalAmountAddedAmmo;
		
		if (remainingAmmo > CurrentMaxAmmo) 
		{
			finalAmountAddedAmmo = CurrentMaxAmmo;
			CurrentMaxAmmo = 0;
		}
		else 
		{
			finalAmountAddedAmmo = remainingAmmo;
			CurrentMaxAmmo -= remainingAmmo;
		}
		
		
		CurrentAmmo += finalAmountAddedAmmo;
		
		EndReload();
		
		Reloading = false;
		_lastReloadTime = Time.time;
		_currentReloadCoroutine = null;
	}
	
	[PunRPC]
	protected void RPC_Shot(int viewID) 
	{
		PhotonView photonView = PhotonView.Find(viewID);
		
		if (photonView != null) 
		{
			var obj = PhotonNetwork.Instantiate(Muzzle.name, MuzzlePosition.position, MuzzlePosition.rotation);
			
			Transform networkMuzzleTransform = photonView.GetComponent<RocketLauncher>().MuzzlePosition;
			
			obj.transform.SetParent(networkMuzzleTransform);
			
			SourceShot?.Play();
		}
	}
	
	[PunRPC]
	protected void RPC_PlayAudioReload() 
	{
		SourceReload?.Play();
	}
}
