using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSpawnerData
{
	[field: SerializeField] public List<Transform> SpawnPoints { get; private set; }
	[field: SerializeField] public int RespawnDuration { get; private set; }
}
