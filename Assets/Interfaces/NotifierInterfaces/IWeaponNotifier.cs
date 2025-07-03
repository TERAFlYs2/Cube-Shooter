using System;

public interface IWeaponNotifier
{
	event Action<int> CurrentAmmoChanged; 
	event Action<int> CurrentMaxAmmoChanged;
	
	int CurrentAmmo { get; }
	int CurrentMaxAmmo { get; }
	bool Reloading { get; }
}
