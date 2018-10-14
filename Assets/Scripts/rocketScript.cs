using UnityEngine;
using System.Collections;

public class rocketScript : MonoBehaviour {

	[HideInInspector]public Vector2 targetPosition;
	private Vector3 lastPos;
	public float speed;
	public int damage;
	public GameObject explosion;
	private GameObject target;
	
	public void getTarget(GameObject bot) {
		target = bot;
		if (bot)
			targetPosition = bot.transform.position;
	}
	
	void Update () {
		lookForward();
		if (target)
			targetPosition = target.transform.position;
		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, targetPosition, step);
		if ((Vector2)transform.position == (Vector2)targetPosition) {
			if (target.gameObject != null)
				target.GetComponent<ennemyScript>().hp -= damage;
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
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
