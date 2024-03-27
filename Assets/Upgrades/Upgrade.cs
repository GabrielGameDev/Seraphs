using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
	Catalyst,
	Resonance,
	Swift, Thunderbolt
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
	public string title;
	public string description;
	public Sprite image;
	public UpgradeType type;

}
