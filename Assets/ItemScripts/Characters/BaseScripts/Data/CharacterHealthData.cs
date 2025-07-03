using UnityEngine;

[System.Serializable]
public class CharacterHealthData
{
	[SerializeField] private int _maxHealth;
	
	public int MaxHealth 
	{
		get 
		{
			return Mathf.Max(_maxHealth, 0);
		}
	}
}
