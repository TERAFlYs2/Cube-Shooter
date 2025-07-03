using System;
using UnityEngine;

public class CharacterStaminaHandler : IStaminaNotifier
{
	private readonly CharacterStaminaData _data;
	private float _lastTimeStaminaReduction;

	public float CurrentAmountStamina { get; private set; }
	public event Action<float> OnStaminaChanged;

	public CharacterStaminaHandler(CharacterStaminaData data)
	{
		_data = data;
		CurrentAmountStamina = data.MaxStamina;
	}

	public void RecoveryUpdate()
	{
		if (Time.time > _lastTimeStaminaReduction + _data.DelayBeforeRecovery)
		{
			UpdateStamina(_data.AmountStaminaRecovery * _data.StaminaRecoveryRate, maxStamina: _data.MaxStamina);
		}
	}

	public void ReductionUpdate()
	{
		if (CurrentAmountStamina > 0)
		{
			UpdateStamina(-_data.AmountStaminaReduction * _data.StaminaReductionRate, maxStamina: _data.MaxStamina);
			_lastTimeStaminaReduction = Time.time;
		}
	}

	private void UpdateStamina(float staminaChange, float maxStamina)
	{
		float value = CurrentAmountStamina + staminaChange * Time.fixedDeltaTime;
		CurrentAmountStamina = Mathf.Clamp(value, 0, maxStamina);

		if (CurrentAmountStamina >= maxStamina)
		{
			CurrentAmountStamina = maxStamina;
			//Debug.Log("Персонаж восстановил выносливость полностью");
			return;
		}
		else if (CurrentAmountStamina <= 0)
		{
			CurrentAmountStamina = 0;
			//Debug.Log("Персонаж устал, его выносливость 0");
			return;
		}

		OnStaminaChanged?.Invoke(CurrentAmountStamina);
	}
}
