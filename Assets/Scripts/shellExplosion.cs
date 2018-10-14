using UnityEngine;
using System.Collections;

public class shellExplosion : MonoBehaviour {

	public int damage;

	void OnTriggerEnter2D(Collider2D bot) {
		if (bot.gameObject.tag == "bot" || bot.gameObject.tag == "boss") {
			bot.gameObject.GetComponent<ennemyScript>().hp -= damage;
		}
	}
}
