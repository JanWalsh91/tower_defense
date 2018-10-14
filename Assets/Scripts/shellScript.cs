using UnityEngine;
using System.Collections;

public class shellScript : MonoBehaviour {

	[HideInInspector]public Vector2 targetPosition;
	public float speed;
	public int damage;
	private Vector3 lastPos;
	public GameObject shellExplosion;

	public void getTarget(GameObject bot) {
		targetPosition = bot.transform.position;
	}

	void Update () {
		lookForward();
		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, targetPosition, step);
		if ((Vector2)transform.position == (Vector2)targetPosition) {
			GameObject explo = (GameObject)Instantiate (shellExplosion, gameObject.transform.position, Quaternion.identity);
			explo.GetComponent<shellExplosion>().damage = damage;
			Destroy (gameObject);
		}
	}

	void lookForward() {
		Vector3 moveDirection = gameObject.transform.position - lastPos; 
		if (moveDirection != Vector3.zero) 
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		lastPos = gameObject.transform.position;
	}
}
