using UnityEngine;

public class DamageProcess : MonoBehaviour
{
	[SerializeField] private int _damageAmount;
	[SerializeField] private float _damageRate;
	
	private float _lastTimeTakeDamage;
	private void Start() 
	{
		_lastTimeTakeDamage = Time.time;
	}

	private void OnTriggerStay(Collider other) 
	{
		if (other.TryGetComponent<IDamagebleProvider>(out var damageble) && Time.time >= _lastTimeTakeDamage + _damageRate)
		{
			damageble.Damageble.TakeDamage(_damageAmount);
			
			_lastTimeTakeDamage = Time.time;
		}
	}
}
