using System.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent] 
[AddComponentMenu("")] 
public class EntryPoint : MonoBehaviour
{
	[SerializeField] private CharacterFactoryData _characterFactoryData;
	[SerializeField] private CharacterSpawnerData _characterSpawnerData;
	[SerializeField] private GameUIData _gameUIData;
	[SerializeField] private ApplicationStats _applicationStats;
	
	private CharacterBase _character;
	private GameUIHandler _gameUIHandler;
	private CharacterSpawnerService _characterSpawnerService;
	private void Awake()
	{
		Run();
	}
	private void Run() 
	{
		InitializeCharacter();
		InitializeServices(); 
		InitializeGameUI();
	}
	
	private void OnDestroy()
	{
		DisposeResources();
	}
	
	private void InitializeServices() 
	{
		_characterSpawnerService = new CharacterSpawnerService(_characterSpawnerData, _character);
		_characterSpawnerService.SpawnRandom();
	}
	private void InitializeCharacter() 
	{
		_character = new CharacterFactory(_characterFactoryData).Create();

		//RenderBody renderBody = new RenderBody(_renderBodyData);
		//renderBody.RenderOnlyInNetwork(_character.GetComponent<Photon.Pun.PhotonView>().IsMine);
	}

	/// <summary>
	/// Инициализация игрового интерфейса с привязкой к персонажу.
	/// </summary>
	/// <param name="character">Ссылка на персонажа.</param>
	private void InitializeGameUI()
	{
		_gameUIHandler = new GameUIHandler(_gameUIData);
		
		var healthHandler = _character.Damageble;
		var staminaHandler = _character.CharacterMovement.CharacterStaminaHandler;
		var weapon = _character.CurrentWeapon;
		
		_gameUIHandler.GameHudHandler.HealthNotifier = healthHandler;
		_gameUIHandler.GameHudHandler.StaminaNotifier = staminaHandler;
		_gameUIHandler.GameHudHandler.WeaponNotifier = weapon;
		_gameUIHandler.GameHudHandler.AppStatsNotifier = _applicationStats;
	}

	/// <summary>
	/// Утилизация ресурсов.
	/// </summary>
	private void DisposeResources()
	{	
		_gameUIHandler?.Dispose();
		_characterSpawnerService?.Dispose();	
	}
}
