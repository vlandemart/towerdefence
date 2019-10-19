using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool Dead;
	private float health;
	private float speed;
	private int damage;
	private int reward;
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	private Waypoint currentWaypoint;
	private Vector2 movePos;

	public void SetUp(Vector2 startPos, EnemyData data)
	{
		Dead = false;
		health = data.Health;
		speed = data.Speed;
		damage = data.Damage;
		reward = Random.Range(data.MinReward, data.MaxReward + 1);
		spriteRenderer.sprite = data.EnemySprite;
		transform.position = startPos;
		GetMovePos();
	}

	private void Update()
	{
		if (Dead)
			return;

		Vector2 pos = transform.position;
		Move();
		if (Vector2.Distance(pos, movePos) < .5f)
			GetMovePos();
	}

	private void GetMovePos()
	{
		currentWaypoint = EnemyController.Instance.GetNextWaypoint(this, currentWaypoint);
		if (currentWaypoint == null)
			return;

		Vector2 pos = currentWaypoint.transform.position;
		float scatterRadius = currentWaypoint.ScatterRadius;
		pos += new Vector2(Random.Range(-scatterRadius, scatterRadius), Random.Range(-scatterRadius, scatterRadius));

		movePos = pos;
	}

	private void Move()
	{
		Vector2 pos = transform.position;
		Vector2 dir = (movePos - pos).normalized;
		transform.position = pos + dir * (speed * Time.deltaTime);
	}

	public void TakeDamage(float damageToTake)
	{
		health -= damageToTake;
		//Spawn some particle effects or something
		if (health <= 0)
			Die();
	}
	
	private void Die()
	{
		Dead = true;
		//Play some particle effects
		//Play death animation
		GameController.Instance.AddReward(reward);
		EnemyController.Instance.ReturnEnemyToPool(this);
	}

	public void DealDamage()
	{
		GameController.Instance.TakeDamage(damage);
		//Play some particle effect or animation
		EnemyController.Instance.ReturnEnemyToPool(this);
	}
}
