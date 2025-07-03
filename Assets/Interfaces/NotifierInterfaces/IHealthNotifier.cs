using System;

public interface IHealthNotifier
{
	event Action<int> OnHealthChanged;
	event Action OnDie;
	int CurrentAmountHealth { get; }
}
