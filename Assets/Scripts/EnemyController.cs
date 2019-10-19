using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public static EnemyController Instance;

	[SerializeField]
	private Enemy enemyPrefab;
	private List<Enemy> enemeisActive = new List<Enemy>();
	private List<Enemy> enemiesDisabled = new List<Enemy>();

	//Waypoint 0 is spawn position, waypoint (Count - 1) is end position
	[SerializeField]
	private List<Waypoint> waypoints = new List<Waypoint>();

	[SerializeField]
	private EnemyData testEnemyData;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
			return;
		}
		Instance = this;
		if (waypoints.Count <= 0)
			Debug.LogError("No waypoints assigned to EnemyController!");
	}

	public Enemy GetClosestEnemy(Vector2 pos, float range)
	{
		Vector2 castlePos = waypoints[waypoints.Count - 1].transform.position;
		Vector2 enemyPos;
		Enemy bestEnemy = null;
		float bestDistance = 1000f;
		float distance;

		foreach (Enemy enemy in enemeisActive)
		{
			if (enemy.Dead)
				continue;
			enemyPos = enemy.transform.position;
			if (Vector2.Distance(enemyPos, pos) > range)
				continue;

			distance = Vector2.Distance(castlePos, enemyPos);
			if (distance <= bestDistance)
			{
				bestEnemy = enemy;
				bestDistance = distance;
			}
		}
		return (bestEnemy);
	}

	private void PopulateEnemiesArray()
	{
		Enemy enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
		enemiesDisabled.Add(enemy);
	}

	public void SpawnEnemy(EnemyData enemyData)
	{
		if (enemiesDisabled.Count <= 0)
			PopulateEnemiesArray();
		Enemy enemy = enemiesDisabled[0];
		enemeisActive.Add(enemy);
		enemiesDisabled.RemoveAt(0);
		enemy.gameObject.SetActive(true);
		enemy.SetUp(waypoints[0].transform.position, enemyData);
	}

	public void ReturnEnemyToPool(Enemy enemy)
	{
		enemy.gameObject.SetActive(false);
		enemeisActive.Remove(enemy);
		enemiesDisabled.Add(enemy);
	}

	public Waypoint GetNextWaypoint(Enemy enemy, Waypoint currWaypoint)
	{
		if (currWaypoint == null)
			return (waypoints[0]);

		int index = waypoints.IndexOf(currWaypoint);
		index++;

		if (index >= waypoints.Count)
		{
			enemy.DealDamage();
			return (null);
		}

		return waypoints[index];
	}

	#region Editor Only Methods
	
	//Important: this way gameobjects are added in inconsistent order
	[ContextMenu("Assign Waypoints")]
	private void AssignWaypoints()
	{
		waypoints = new List<Waypoint>(GameObject.FindObjectsOfType<Waypoint>());
	}

	[ContextMenu("Spawn Test Enemy")]
	private void SpawnTestEnemy()
	{
		SpawnEnemy(testEnemyData);
		Debug.Log("Test enemy spawned!");
	}

	#endregion
}
