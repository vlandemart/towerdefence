using UnityEngine;

public class Waypoint : MonoBehaviour
{
	public float ScatterRadius = 3f;
	[SerializeField]
	private bool drawGizmo = true;
	[SerializeField]
	private Color gizmoColor = Color.blue;

	private void OnDrawGizmos()
	{
		if (!drawGizmo)
			return;
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere(transform.position, ScatterRadius);
	}
}
