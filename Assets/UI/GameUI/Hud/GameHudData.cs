using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameHudData
{
	[field: SerializeField] public Slider HealthBar { get; private set; }
	[field: SerializeField] public Slider StaminaBar { get; private set; }
	[field: SerializeField] public TMP_Text CurrentAmmoText { get; private set; }
	[field: SerializeField] public TMP_Text CurrentMaxAmmoText { get; private set; }
	[field: SerializeField] public TMP_Text PingText { get; private set; }
	[field: SerializeField] public TMP_Text FpsText { get; private set; }
}
