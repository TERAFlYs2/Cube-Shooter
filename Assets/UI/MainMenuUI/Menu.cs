using UnityEngine;

public abstract class Menu : MonoBehaviour
{
	[field: SerializeField] public string Name { get; private set; }
	
	public MenuController MenuController { get; set; }
	
	public abstract void SubscribeListeners();
	public abstract void UnSubscribeListeners();

	
	public void Open() => gameObject.SetActive(true);
	public void Close() => gameObject.SetActive(false);
	
 }
