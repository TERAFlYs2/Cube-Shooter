using System;
using Photon.Pun;
using UnityEngine;

public class CharacterHealthHandler : IDamageble
{
	private readonly CharacterHealthData _data;
	private readonly PhotonView _photonView;
	private readonly Animator _animator;
	
	private int _currentAmountHealth;

	private bool _canTakeDamage;
	public int CurrentAmountHealth 
	{
		get => _currentAmountHealth;
		private set
		{
			_currentAmountHealth = value;
			OnHealthChanged?.Invoke(_currentAmountHealth); 
		}
	}

	public event Action<int> OnHealthChanged; 
	public event Action OnDie;        

	public CharacterHealthHandler(CharacterHealthData data, PhotonView photonView, Animator animator)
	{
		_data = data ?? throw new ArgumentNullException(nameof(data)); 
		_photonView = photonView ?? throw new ArgumentNullException(nameof(photonView));
		_animator = animator ?? throw new ArgumentNullException(nameof(animator));
		
		CurrentAmountHealth = _data.MaxHealth;
		_canTakeDamage = true;
	}
	
	[PunRPC]
	private void RPC_TakeDamage(int amount) 
	{
		CurrentAmountHealth -= amount;
	}
	
	public virtual void TakeDamage(int amount) 
	{
		if (amount < 0)
			throw new ArgumentException("The damage value cannot be negative.", nameof(amount));
		
		if (_canTakeDamage && _photonView.IsMine)	
		{
			//_photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, amount);
			
			CurrentAmountHealth -= amount;

			if (CurrentAmountHealth <= 0) 
			{
				CurrentAmountHealth = 0;
				Die();
				
			}
		}
	}
	
	public virtual void Heal(int amount) 
	{
		if (amount < 0)
			throw new ArgumentException("The heal value cannot be negative.", nameof(amount));
		
		if (_photonView.IsMine) 
		{
			CurrentAmountHealth += amount;

			if (CurrentAmountHealth > _data.MaxHealth) 
			{
				CurrentAmountHealth = _data.MaxHealth;
			}
		}
	}
	
	public void SetMaxHealth() 
	{
		CurrentAmountHealth = _data.MaxHealth;
	}
	
	public void Die() 
	{
		Debug.Log("Ваш игрок погиб");
		_animator.SetTrigger("Dead");
		_animator.SetBool("IsDead", true);
		CurrentAmountHealth = 0;
		OnDie?.Invoke(); 
	}

	public void SetCanTakeDamage(bool canTakeDamage)
	{
		_canTakeDamage = canTakeDamage;
	}
}
