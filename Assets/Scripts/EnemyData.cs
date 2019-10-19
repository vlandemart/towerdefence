using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
	public float Health;
	public float Speed;
	public int Damage;
	public int MinReward;
	public int MaxReward;
	public Sprite EnemySprite;
}
