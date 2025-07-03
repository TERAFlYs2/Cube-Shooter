using UnityEngine;

[System.Serializable]
public class CharacterCameraData
{
	[SerializeField] private float _sensitivityX = 5;
	[SerializeField] private float _sensitivityY = 5;
	
	[SerializeField] private float _minAngleY = -45f;
	[SerializeField] private float _maxAngleY = 45f;
	
	public float SensitivityX 
	{
		get 
		{
			return Mathf.Max(_sensitivityX, 0);
		}
	}
	
	public float SensitivityY 
	{
		get 
		{
			return Mathf.Max(_sensitivityY, 0);
		}
	}
	
	public float MinAngleY 
	{
		get 
		{
			return Mathf.Min(_minAngleY, 0);
		}
	}
	
	public float MaxAngleY 
	{
		get 
		{
			return Mathf.Max(_maxAngleY, 0);
		}
	}
}
