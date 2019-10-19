using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower")]
public class TowerData : ScriptableObject
{
	public int Price;
	public float Range;
	public float Damage;
	public float ShootInterval;
	public Sprite TowerSprite;
	public List<TowerData> PossibleUpgrades;
}
