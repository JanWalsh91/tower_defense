using UnityEngine;
using System.Collections;

public class gatlingImpactScript : MonoBehaviour {

	public float lifeTime;
	private float startLife;

	void Start() {
		startLife = Time.time;
	}

	void Update() {
		if (Time.time > lifeTime + startLife)
			Destroy (gameObject);
	}
}
