using UnityEngine;
using System.Collections;

public class visualPathCheckpoint : MonoBehaviour {

	private Transform target;

	void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "map.png", false);
		if (this.gameObject.GetComponent<checkpoint>().nextCheckpoint) {
			target = this.gameObject.GetComponent<checkpoint>().nextCheckpoint.transform;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, target.position);
		}
	}
}
