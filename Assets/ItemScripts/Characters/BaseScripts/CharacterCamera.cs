using UnityEngine;

public class CharacterCamera
{
	private readonly CharacterCameraData _data;
	
	private readonly CharacterController _characterController;
	
	private float _verticalRotation;
	
	public CharacterCamera(CharacterCameraData data, CharacterController characterController)
	{
		_data = data;
		_characterController = characterController;
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	public void RotateInput(Camera camera)
	{
		// Горизонтальный поворот (вокруг оси Y персонажа)
		float mouseX = Input.GetAxis("Mouse X") * _data.SensitivityX * Time.deltaTime;
		_characterController.transform.Rotate(0f, mouseX, 0f);

		// Вертикальный поворот (вокруг оси X камеры)
		float mouseY = Input.GetAxis("Mouse Y") * _data.SensitivityY * Time.deltaTime;

		// Накопление вертикального поворота
		_verticalRotation -= mouseY; // Уменьшаем, потому что "Mouse Y" обратное
		_verticalRotation = Mathf.Clamp(_verticalRotation, _data.MinAngleY, _data.MaxAngleY); // Ограничиваем угол поворота

		// Применяем поворот к камере
		camera.transform.localEulerAngles = new Vector3(_verticalRotation, 0f, 0f);
	}
}
