using Photon.Pun;
using UnityEngine;
public enum Team 
{
	Blue,
	Red
}

public abstract class CharacterBase : MonoBehaviourPun, IDamagebleProvider
{
	[field: SerializeField] public CharacterBaseData Data {get; private set; }
	[SerializeField] private Camera _camera;
	[field: SerializeField] public Weapon CurrentWeapon { get; private set; }
	
	public CharacterMovement CharacterMovement { get; protected set; }
	public CharacterCamera CharacterCamera { get; protected set; }
	public IDamageble Damageble { get; protected set; }
	
	private void Awake() 
	{		
		if (photonView.IsMine) 
		{
			_camera.enabled = true;
			Cursor.lockState = CursorLockMode.Locked;
		}
		else 
		{
			_camera.enabled = false;
		}
		
		var characterController = GetComponent<CharacterController>();		
		
		CharacterMovement = new CharacterMovement(Data.CharacterMovementData, characterController, Data.Animator);
		CharacterCamera = new CharacterCamera(Data.CharacterCameraData, characterController);
		Damageble = new CharacterHealthHandler(Data.CharacterHealthData, photonView, Data.Animator);
	}
	
	private void Update() 
	{
		if (photonView.IsMine) 
		{
			UpdateMovementInput();
			UpdateRotateInput();

			if (Input.GetMouseButtonDown(1))
				CurrentWeapon?.Attack();

			if (Input.GetKeyDown(KeyCode.R)) 
			{
				CurrentWeapon?.Reload();
			}
		}
		
	}
	
	private void FixedUpdate() 
	{
		if (photonView.IsMine)
			UpdateMovement();
	}
	
	protected virtual void UpdateRotateInput() 
	{
		CharacterCamera.RotateInput(_camera);
	}
	
	protected virtual void UpdateMovementInput()
	{
		CharacterMovement.MoveInput(transform.forward, transform.right);
		
	}
	
	protected virtual void UpdateMovement() 
	{
		CharacterMovement.Move();
	}
	
	
}
