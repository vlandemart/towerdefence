using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
	private int price;
	private float damage;
	private float range;
	private float attackCd;
	private float currentCd;
	public List<TowerData> PossibleUpgrades = new List<TowerData>();

	[SerializeField]
	private SpriteRenderer spriteRenderer;
	[SerializeField]
	private TowerData placeholderData;

	private Enemy currentTarget;

	public void SetUp(TowerData data)
	{
		price = data.Price;
		damage = data.Damage;
		range = data.Range;
		attackCd = data.ShootInterval;
		PossibleUpgrades = new List<TowerData>(data.PossibleUpgrades);
		spriteRenderer.sprite = data.TowerSprite;
	}

	private void Start()
	{
		SetUp(placeholderData);
	}

	private void Update()
	{
		if (currentCd > 0)
		{
			currentCd -= Time.deltaTime;
			return;
		}
		Behaviour();
	}

	private void Behaviour()
	{
		if (currentTarget == null)
			FindNewTarget();
		if (currentTarget == null)
			return;
		if (currentCd <= 0)
			ShootAt(currentTarget);
	}

	private void FindNewTarget()
	{
		Vector2 pos = transform.position;
		currentTarget = EnemyController.Instance.GetClosestEnemy(pos, range);
	}

	private void ShootAt(Enemy enemy)
	{
		if (Vector2.Distance(enemy.transform.position, transform.position) > range)
			return;
		enemy.TakeDamage(damage);
		currentCd += attackCd;
		if (currentTarget.Dead)
			currentTarget = null;
		//play animation
		//play particle effect
	}

	public void SellTower()
	{
		GameController.Instance.AddReward(price / 2);
		SetUp(placeholderData);
	}

	public void UpgradeTower(TowerData data)
	{
		SetUp(data);
	}

	#region Editor Only Methods

	[ContextMenu("Sell tower")]
	private void TestSellTower()
	{
		SellTower();
	}

	[ContextMenu("Upgrade tower")]
	private void TestUpgradeTower()
	{
		UpgradeTower(PossibleUpgrades[0]);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, range);
	}

	#endregion
}
