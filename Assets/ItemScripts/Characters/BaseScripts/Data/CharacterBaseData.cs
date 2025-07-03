using UnityEngine;

[System.Serializable]
public class CharacterBaseData
{
	[Header("General")]
	[field: SerializeField] public string Name;
	[field: SerializeField] public Team CurrentTeam;
	
	[Header("Custom components")]
	[field: SerializeField] public CharacterMovementData CharacterMovementData;
	[field: SerializeField] public CharacterCameraData CharacterCameraData;
	[field: SerializeField] public CharacterHealthData CharacterHealthData;
	
	
	[Header("Components")]
	[field: SerializeField] public Animator Animator;
}
