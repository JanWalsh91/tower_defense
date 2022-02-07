using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireScript : MonoBehaviour {

	public float radius;
	public int energy;
	public float cooldown;
	public bool canUse;
	public GameObject boom;

	float timeSinceLastUse;


	public static fireScript fm;
	void Awake() {
		if (fm == null)
			fm = this;
	}

	// Use this for initialization
	void Start () {
		canUse = false;
		timeSinceLastUse = Time.time;
	}

	void Update() {
		if (!canUse && Time.time - timeSinceLastUse > cooldown) {
			canUse = true;
		}
	}

	public void Explode(Vector2 position) {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
		foreach (Collider2D collider in colliders) {
			if (collider.gameObject.CompareTag("EnemyMinion")){
				Destroy(collider.gameObject);
			}
		}
		Instantiate(boom, position, Quaternion.identity);
		canUse = false;
		timeSinceLastUse = Time.time;
	}
}
