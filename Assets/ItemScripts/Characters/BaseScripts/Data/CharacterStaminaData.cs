using UnityEngine;

[System.Serializable]
public class CharacterStaminaData
{
	[SerializeField] private float _maxStamina = 100;
	[SerializeField] private float _amountStaminaRecovery = 2;
	[SerializeField] private float _amountStaminaReduction = 2;
	[SerializeField] private float _staminaRecoveryRate = 3f;
	[SerializeField] private float _staminaReductionRate = 3f;
	[SerializeField] private float _delayBeforeRecovery = 3f;
	
	public float MaxStamina  
	{
		get 
		{
			return Mathf.Max(0, _maxStamina);
		}
	}
	
	public float AmountStaminaRecovery 
	{
		get 
		{
			return Mathf.Max(_amountStaminaRecovery, 0);
		}
	}
	
	public float AmountStaminaReduction 
	{
		get 
		{
			return Mathf.Max(_amountStaminaReduction, 0);
		}
	}
	
	public float StaminaRecoveryRate 
	{
		get 
		{
			return Mathf.Max(0, _staminaRecoveryRate);
		}
	}
	
	public float StaminaReductionRate 
	{
		get 
		{
			return Mathf.Max(0, _staminaReductionRate);
		}
	}
	
	public float DelayBeforeRecovery 
	{
		get 
		{
			return Mathf.Max(_delayBeforeRecovery, 0);
		}
	}
}
