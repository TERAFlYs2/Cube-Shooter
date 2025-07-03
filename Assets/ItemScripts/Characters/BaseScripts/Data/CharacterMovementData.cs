using UnityEngine;

[System.Serializable]
public class CharacterMovementData
{
	#region Values
	
	[Header("Values")]
	
	[Tooltip("Задает скорость ходьбы персонажу.")]
	[SerializeField] private float _walkSpeed = 5;
	
	
	[Tooltip("Задает скорость бега персонажу.")]
	[SerializeField] private float _runSpeed = 9;
	
	
	[Tooltip("Задает высоту прыжка персонажа.")]
	[SerializeField] private float _jumpHeiht = 2;
	
	#endregion
	
	#region Modifiers
	
	[Header("Modifiers")]
	
	[Tooltip("Вычисляет процент боковой скорости персонажа.")]
	[SerializeField][Range(0, 1)] private float _lateralSpeedModifier = 0.65f;
	
	[Tooltip("Вычисляет процент скорости персонажа над землей, где 1 - не меняет скорость, а 0 - обнуляет.")]
	[SerializeField][Range(0, 1)] private float _aboveGroundSpeedModifier = 0.4f;
	
	#endregion
	
	#region Physics
	
	[Header("Physics")]
	[SerializeField] private float _acceleration = 2;
	[SerializeField] private float _deceleration = 2;
	[SerializeField] private float _gravity = -9.8f;
	[SerializeField] private float _drag = 0;
	
	#endregion
	
	#region Inputs
	
	[Header("Inputs")]
	[SerializeField] private KeyCode _runButton = KeyCode.LeftShift;
	[SerializeField] private KeyCode _jumpButton = KeyCode.Space;
	
	#endregion
	
	#region Other
	
	[Header("Other")] 
	[SerializeField] private CharacterStaminaData _characterStaminaData;
	
	#endregion

	
	#region ValuesProperties
	public float WalkSpeed 
	{
		get 
		{
			return Mathf.Max(_walkSpeed, 0f);
		}
	}
	
	public float RunSpeed 
	{
		get 
		{
			return Mathf.Max(_runSpeed, _walkSpeed);
		}
	}
	
	public float JumpHeiht 
	{
		get 
		{
			return Mathf.Max(_jumpHeiht, 0);
		}
	}
	
	#endregion
	
	#region ModifiersProperties
	
	public float LateralSpeedModifier => _lateralSpeedModifier;
	public float AboveGroundSpeedModifier => _aboveGroundSpeedModifier;
	
	#endregion
	
	#region PhysicsProperties
	public float Acceleration 
	{
		get 
		{
			return Mathf.Max(_acceleration, 0f);
		}
	}
	
	public float Deceleration 
	{
		get 
		{
			return Mathf.Max(_deceleration, 0f);
		}
	}
	
	public float Gravity => _gravity;
	public float Drag 
	{
		get 
		{
			return Mathf.Max(_drag, 0);
		}
	}
	
	#endregion 
	
	#region InputsProperties
	
	public KeyCode RunButton => _runButton;
	public KeyCode JumpButton => _jumpButton;
	
	#endregion 
	
	#region OtherProperties
	public CharacterStaminaData CharacterStaminaData => _characterStaminaData;
	
	#endregion
}
