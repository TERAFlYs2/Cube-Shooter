using System;

public interface IStaminaNotifier 
{
	event Action<float> OnStaminaChanged;
	float CurrentAmountStamina { get; }
}
