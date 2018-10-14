using UnityEngine;
using System.Collections;

public class visualPathSpawner : MonoBehaviour {

	private Transform target;

	void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "death.png", true);
		if (this.gameObject.GetComponent<ennemySpawner>().nextCheckpoint) {
			target = this.gameObject.GetComponent<ennemySpawner>().nextCheckpoint.transform;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, target.position);
		}
	}
}
