using System;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovement
{
	private const float DistanceGroundCheck = 0.15f;
	
	private readonly CharacterMovementData _data;
	private readonly CharacterController _characterController;
	private readonly Animator _animator;
	private readonly CharacterStaminaHandler _characterStaminaHandler;
	
	private float _currentHorizontalSpeedX = 0;
	private float _currentHorizontalSpeedZ = 0;
	private float _currentVerticalSpeed = 0;
	
	private bool _runKeyPressed;
	private bool _jumpKeyPressed;
	private bool _canStopInertia;
	
	private Vector3 _movementDirectionX;
	private Vector3 _movementDirectionZ;
	private Vector3 _lastMovementBeforeJump = Vector3.zero;
	private Vector3 _lastMovementBeforeStopX = Vector3.zero;
	private Vector3 _lastMovementBeforeStopZ = Vector3.zero;
	
	public CharacterStaminaHandler CharacterStaminaHandler => _characterStaminaHandler;
	public bool CanMovement { get; set; }
	public CharacterMovement(CharacterMovementData data, CharacterController characterController, Animator animator)
	{
		_data = data;
		_characterController = characterController;
		_animator = animator;
		_characterStaminaHandler = new CharacterStaminaHandler(_data.CharacterStaminaData);
		CanMovement = true;
	}

	public void MoveInput(Vector3 forward, Vector3 right)
	{
		if (!CanMovement) return;

		float dirZ = Input.GetAxisRaw("Vertical"); 
		float dirX = Input.GetAxisRaw("Horizontal"); 

		_movementDirectionX = dirX * right;
		_movementDirectionZ = dirZ * forward;
		
		_runKeyPressed = Input.GetKey(_data.RunButton);
		_jumpKeyPressed = Input.GetKeyDown(_data.JumpButton);
	}

	public void Move()
	{
		Vector3 finalMovement = Vector3.zero;
		
		if (CanMovement) 
		{
			Vector3 movement = (_movementDirectionX * _currentHorizontalSpeedX + _movementDirectionZ * _currentHorizontalSpeedZ) * Time.fixedDeltaTime;
			
			if(!IsGrounded()) 
			{
				finalMovement = movement * _data.AboveGroundSpeedModifier;
				_animator.SetFloat("Speed", 0);
			}
			else 
			{
				finalMovement = movement;
				_animator.SetFloat("Speed", _characterController.velocity.magnitude);
			}
			
			UpdateJump(finalMovement);
			
			UpdateAcceleration(finalMovement);
		}
		
		
		UpdateGravity(ref finalMovement);
		
		_characterController.Move(finalMovement + _lastMovementBeforeJump);
	}

	private void UpdateAcceleration(Vector3 movement)
	{
		float targetSpeedX = GetCurrentSpeed(_currentHorizontalSpeedX) * _data.LateralSpeedModifier; // Целевая скорость в зависимости от режима (бег/ходьба) в бок
		float targetSpeedZ = GetCurrentSpeed(_currentHorizontalSpeedZ); // Целевая скорость в зависимости от режима (бег/ходьба) вперед
		
		AccelerationXHandler(movement, targetSpeedX);
		AccelerationZHandler(movement, targetSpeedZ);		
	}

	private void AccelerationXHandler(Vector3 movement, float targetSpeedX) 
	{
		if (_movementDirectionX.magnitude > 0.1f) // Если есть ввод от пользователя в бок
		{
			_currentHorizontalSpeedX = Mathf.Lerp(_currentHorizontalSpeedX, targetSpeedX, _data.Acceleration * Time.fixedDeltaTime);
			_lastMovementBeforeStopX = movement;
		}
		else 
		{
			DecelerationXHandler();
		}
	}
	private void AccelerationZHandler(Vector3 movement, float targetSpeedZ) 
	{
		if (_movementDirectionZ.magnitude > 0.1f) 
		{
			_currentHorizontalSpeedZ = Mathf.Lerp(_currentHorizontalSpeedZ, targetSpeedZ, _data.Acceleration * Time.fixedDeltaTime);
			_lastMovementBeforeStopZ = movement;
		}
		else 
		{
			DecelerationZHandler();
		}
	}
	
	private void DecelerationXHandler() 
	{
		if (_currentHorizontalSpeedX >= 0.1f) 
		{
			_currentHorizontalSpeedX = Mathf.Lerp(_currentHorizontalSpeedX, 0f, 1f / _data.Deceleration * Time.fixedDeltaTime);
				
			if (IsGrounded())
				_characterController.Move(_lastMovementBeforeStopX.normalized * _currentHorizontalSpeedX * Time.fixedDeltaTime);
	
			else 
				_currentHorizontalSpeedX = 0;
			
			//Debug.Log("Текущая скорость по X: " + _currentHorizontalSpeedX);
		}
	}
	
	private void DecelerationZHandler() 
	{
		if (_currentHorizontalSpeedZ >= 0.1f) 
		{
			_currentHorizontalSpeedZ = Mathf.Lerp(_currentHorizontalSpeedZ, 0f, 1f / _data.Deceleration * Time.fixedDeltaTime);
				
			if (IsGrounded())
				_characterController.Move(_lastMovementBeforeStopZ.normalized * _currentHorizontalSpeedZ * Time.fixedDeltaTime);
	
			else 
				_currentHorizontalSpeedZ = 0;
			
			//Debug.Log("Текущая скорость по Z: " + _currentHorizontalSpeedZ);
		}
	}
	private float GetCurrentSpeed(float speed)
	{
		if (_runKeyPressed && _characterStaminaHandler.CurrentAmountStamina > 0 && speed > 0.1f)
		{
			_characterStaminaHandler.ReductionUpdate();
			return _data.RunSpeed;
		}

		_characterStaminaHandler.RecoveryUpdate();
		return _data.WalkSpeed;
	}

	private void UpdateJump(Vector3 movement)
	{
		if (IsGrounded())
		{
			//_tempMovement = Vector3.zero;

			if (_jumpKeyPressed)
			{
				StopInertiaAsync(DistanceGroundCheck);
				_currentVerticalSpeed = _data.JumpHeiht;
				_animator.SetTrigger("Jump");
				
				// Сохраняем инерцию только по горизонтали
				_lastMovementBeforeJump = movement;
				_lastMovementBeforeJump.y = 0;

				_jumpKeyPressed = false;
			}
		}
		if (IsGrounded() && _canStopInertia) 
		{
			_lastMovementBeforeJump = Vector3.zero;
			_canStopInertia = false;
		}
	}

	private async void StopInertiaAsync(float delay) 
	{
		await Task.Delay(TimeSpan.FromSeconds(delay));
		_canStopInertia = true;
	}
	private bool IsGrounded()
	{
		return Physics.Raycast(_characterController.transform.position, Vector3.down, DistanceGroundCheck);
	}

	private void UpdateGravity(ref Vector3 movement)
	{
		if (!_characterController.isGrounded)
		{
			_currentVerticalSpeed += _data.Gravity * Time.fixedDeltaTime;
			
			_currentVerticalSpeed = Mathf.Lerp(_currentVerticalSpeed, _data.Gravity, _data.Drag * Time.fixedDeltaTime);
				
			movement.y = _currentVerticalSpeed * Time.fixedDeltaTime;
			
		}
		else
		{
			if (_currentVerticalSpeed < 0)
				_currentVerticalSpeed = 0;
			
			movement.y = 0;
		}
		//Debug.Log("Текущая вертикальная скорость: " + _currentVerticalSpeed);
	}
}
