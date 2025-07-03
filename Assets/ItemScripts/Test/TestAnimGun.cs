using UnityEngine;

public class TestAnimGun : MonoBehaviour
{	
	[SerializeField] private Animator _animator;
	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.G)) 
		{
			_animator.SetBool("HasWeapon", true);
		}
		else if (Input.GetKeyDown(KeyCode.H)) 
		{
			_animator.SetBool("HasWeapon", false);
		}
	}
}
