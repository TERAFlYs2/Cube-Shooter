using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterFactoryData 
{
	[field: SerializeField] public CharacterBase Prefab { get; private set; }
	
}
