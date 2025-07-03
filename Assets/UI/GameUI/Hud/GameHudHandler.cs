using UnityEngine;

public class GameHudHandler
{
	private readonly GameHudData _data;
	
	private IStaminaNotifier _staminaNotifier;
	private IHealthNotifier _healthNotifier;
	private IWeaponNotifier _weaponNotifier;
	private IAppStatsNotifier _appStatsNotifier;
	
	public GameHudHandler(GameHudData gameHudData)
	{
		_data = gameHudData;
	}

	public IStaminaNotifier StaminaNotifier
	{
		get => _staminaNotifier;
		set
		{
			if (_staminaNotifier != null)
				_staminaNotifier.OnStaminaChanged -= UpdateStaminaBar;
			
			_staminaNotifier = value;

			_staminaNotifier.OnStaminaChanged += UpdateStaminaBar;
				
			UpdateStaminaBar(_staminaNotifier.CurrentAmountStamina);			
		}
	}
	
	public IHealthNotifier HealthNotifier
	{
		get => _healthNotifier;
		set
		{
			if (_healthNotifier != null)
				_healthNotifier.OnHealthChanged -= UpdateHealthBar;
			
			_healthNotifier = value;

			_healthNotifier.OnHealthChanged += UpdateHealthBar;
				
			UpdateHealthBar(_healthNotifier.CurrentAmountHealth);		
		}
	}
	
	public IWeaponNotifier WeaponNotifier
	{
		get => _weaponNotifier;
		set
		{
			if (_weaponNotifier != null) 
			{
				_weaponNotifier.CurrentAmmoChanged -= UpdateCurrentAmmoText;
				_weaponNotifier.CurrentMaxAmmoChanged -= UpdateCurrentMaxAmmoText;	
			}
			
			_weaponNotifier = value;

			_weaponNotifier.CurrentAmmoChanged += UpdateCurrentAmmoText;
			_weaponNotifier.CurrentMaxAmmoChanged += UpdateCurrentMaxAmmoText;
				
			UpdateCurrentAmmoText(_weaponNotifier.CurrentAmmo);
			UpdateCurrentMaxAmmoText(_weaponNotifier.CurrentMaxAmmo);
		}
	}
	
	public IAppStatsNotifier AppStatsNotifier 
	{
		get => _appStatsNotifier;
		set
		{
			if (_appStatsNotifier != null) 
			{
				_appStatsNotifier.OnUpdateFps -= UpdateFpsText;
				_appStatsNotifier.OnUpdatePing -= UpdatePingText;
			}				
			
			_appStatsNotifier = value;

			_appStatsNotifier.OnUpdateFps += UpdateFpsText;	
			_appStatsNotifier.OnUpdatePing += UpdatePingText;
		}
	}

	public void UnSubscribeListeners()
	{
		if (_staminaNotifier != null) 
			_staminaNotifier.OnStaminaChanged -= UpdateStaminaBar;
				
		if (_healthNotifier != null) 
			_healthNotifier.OnHealthChanged -= UpdateHealthBar;
		
		if(_weaponNotifier != null) 
		{
			_weaponNotifier.CurrentAmmoChanged -= UpdateCurrentAmmoText;
			_weaponNotifier.CurrentMaxAmmoChanged -= UpdateCurrentMaxAmmoText;	
		}
	}

	private void UpdateHealthBar(int currentHealth) 
	{
		if (_data.HealthBar != null) 
			_data.HealthBar.value = Mathf.Clamp(currentHealth, 0, _data.HealthBar.maxValue);
	}
	private void UpdateStaminaBar(float currentStamina)
	{
		if (_data.StaminaBar != null)
			_data.StaminaBar.value = Mathf.Clamp(currentStamina, 0, _data.StaminaBar.maxValue);
	}
	
	private void UpdateCurrentAmmoText(int currentAmmo) 
	{
		if (_data.CurrentAmmoText != null) 
			_data.CurrentAmmoText.text = currentAmmo.ToString();
	}
	
	private void UpdateCurrentMaxAmmoText(int currentMaxAmmo) 
	{
		if (_data.CurrentMaxAmmoText != null) 
			_data.CurrentMaxAmmoText.text = currentMaxAmmo.ToString();
	}
	
	private void UpdateFpsText(int fps) 
	{
		if(_data.FpsText != null) 
			_data.FpsText.text = fps.ToString() + " Fps";
	}
	
	private void UpdatePingText(int ping) 
	{
		if(_data.PingText != null) 
			_data.PingText.text = ping.ToString() + " Ping";
	}
}
